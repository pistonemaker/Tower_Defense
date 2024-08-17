using System.Collections.Generic;
using UnityEngine;

public class LevelManager : Singleton<LevelManager>
{
    public GameData gameData;
    public LevelData levelData;
    public Database database;

    public Transform spawnersTrf;
    public Transform pathWaysTrf;
    public Transform plotTrf;

    public List<Wave> listWaves = new List<Wave>();
    public List<Spawner> listSpawners = new List<Spawner>();
    public List<Pathway> listPathways = new List<Pathway>();

    private int spiritStone = 0;
    private int lives = 0;

    public int SpiritStone
    {
        get => spiritStone;
        set
        {
            spiritStone = value;
            this.PostEvent(EventID.On_Spirit_Stone_Change, value);
        }
    }

    public int Lives
    {
        get => lives;
        set
        {
            lives = value;
            this.PostEvent(EventID.On_Lives_Change, value);
        }
    }

    protected override void Awake()
    {
        base.Awake();
        
        AudioManager.Instance.PlayMusic("Game_Play", 0.5f);
        InitMap();
        InitLevel();
    }

    private void OnEnable()
    {
        this.RegisterListener(EventID.On_Spawn_Next_Wave,
            param => OnCreateNextWave((int)param));
        this.RegisterListener(EventID.On_Monster_Killed,
            param => OnMonsterKilled((int)param));
        this.RegisterListener(EventID.On_Monster_Escaped,
            param => OnMonsterEscaped((int)param));
        EventDispatcher.Instance.RegisterListener(EventID.On_Player_Win, HandlePlayerWin);
    }

    private void OnDisable()
    {
        this.RemoveListener(EventID.On_Spawn_Next_Wave,
            param => OnCreateNextWave((int)param));
        this.RemoveListener(EventID.On_Monster_Killed,
            param => OnMonsterKilled((int)param));
        this.RemoveListener(EventID.On_Monster_Escaped,
            param => OnMonsterEscaped((int)param));
        EventDispatcher.Instance.RemoveListener(EventID.On_Player_Win, HandlePlayerWin);
    }

    private void InitMap()
    {
        int seasonID = PlayerPrefs.GetInt(DataKey.Cur_Season);
        int levelID = PlayerPrefs.GetInt(DataKey.Cur_Level);
        levelData = gameData.listSeasonData[seasonID].listLevelData[levelID];
        var listTowerPositions = levelData.mapData.listTowerPositions;

        for (int i = 0; i < listTowerPositions.Count; i++)
        {
            var plot = PoolingManager.Spawn(database.prefabData.towerPosition);
            plot.transform.SetParent(plotTrf);
            plot.transform.localScale = 0.8f * Vector3.one;
            plot.transform.position = listTowerPositions[i];
        }
    }

    public void InitLevel()
    {
        SpiritStone = levelData.spiritStoneStart;
        Lives = levelData.liveStart;
        TowerBuildManager.Instance.Init();
        CreateSpawnersAndPathWays();
        StartCoroutine(UIManager.Instance.ShowWaveName(0));
    }

    public void CreateSpawnersAndPathWays()
    {
        for (int i = 0; i < levelData.layoutData.spawnersData.Count; i++)
        {
            var spawner = Instantiate(database.prefabData.spawnerPrefab, spawnersTrf);
            spawner.spawnerID = i;
            spawner.InitSpawner(levelData.layoutData.spawnersData[i]);
            listSpawners.Add(spawner);
        }

        for (int i = 0; i < levelData.layoutData.pathwaysData.Count; i++)
        {
            var pathway = Instantiate(database.prefabData.pathwayPrefab, pathWaysTrf);
            pathway.pathwayID = i;
            pathway.InitPathway(levelData.layoutData.pathwaysData[i]);
            listPathways.Add(pathway);
        }
    }

    // Bắt sự kiện tạo Wave mới
    public void OnCreateNextWave(int waveID)
    {
        if (IsPlayerWin(waveID + 1))
        {
            return;
        }

        waveID++;
        StartCoroutine(UIManager.Instance.ShowWaveName(waveID));
    }

    // Check xem win chưa
    private bool IsPlayerWin(int waveID)
    {
        if (waveID >= levelData.listWavesData.Count)
        {
            Debug.Log("Win :v");
            this.PostEvent(EventID.On_Player_Win, levelData.coinWinGet);
            return true;
        }

        return false;
    }

    // Sinh quái cho Wave 
    public void CreateWave(int waveID)
    {
        var wave = Instantiate(database.prefabData.wavePrefab);
        wave.waveID = waveID;
        wave.name = "Wave " + (waveID + 1);
        wave.InitWave(levelData.listWavesData[waveID]);
        listWaves.Add(wave);
    }

    private void OnMonsterKilled(int spiritStoneGet)
    {
        SpiritStone += spiritStoneGet;
    }

    private void OnMonsterEscaped(int livesAmount)
    {
        Lives -= livesAmount;
        if (Lives <= 0)
        {
            Lives = 0;
            Debug.Log("Lost Game");
            this.PostEvent(EventID.On_Player_Lose, levelData.coinLoseGet);
        }
    }

    public int CountStarGet()
    {
        if (Lives == levelData.liveStart)
        {
            return 3;
        }

        if ((float)Lives / levelData.liveStart > 1 / 2f)
        {
            return 2;
        }

        return 1;
    }

    private void HandlePlayerWin(object param)
    {
        if (!levelData.isPass)
        {
            levelData.isPass = true;
        }
        
        var maxLevel = PlayerPrefs.GetInt(DataKey.Max_Level);
        var curLevel = PlayerPrefs.GetInt(DataKey.Cur_Level);
        var maxSeason = PlayerPrefs.GetInt(DataKey.Max_Season);
        var curSeason = PlayerPrefs.GetInt(DataKey.Cur_Season);

        if (curSeason < maxSeason || maxSeason == gameData.listSeasonData.Count - 1)
        {
            return;
        }
        
        if (curSeason == maxSeason)
        {
            if (maxLevel == gameData.listSeasonData[curSeason].listLevelData.Count - 1)
            {
                if (curLevel == maxLevel)
                {
                    maxLevel = 0;
                    curLevel = 0;
                    maxSeason++;
                    curSeason++;
                    PlayerPrefs.SetInt(DataKey.Max_Level, maxLevel);
                    PlayerPrefs.SetInt(DataKey.Cur_Level, curLevel);
                    PlayerPrefs.SetInt(DataKey.Max_Season, maxSeason);
                    PlayerPrefs.SetInt(DataKey.Cur_Season, curSeason);
                }
            }
        }
    }
}