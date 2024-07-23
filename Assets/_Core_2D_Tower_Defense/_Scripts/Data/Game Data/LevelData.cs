using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data_Level ", menuName = "Data/Level Data")]
public class LevelData : ScriptableObject
{
    public bool isPass;
    public int spiritStoneStart;
    public int liveStart;
    public int coinWinGet;
    public int coinLoseGet;
    public MapData mapData;
    public List<TowerInLevel> towersInLevel;
    public LevelLayoutData layoutData;
    public List<WaveData> listWavesData;
}

[Serializable]
public class WaveData
{
    public string name;
    public List<MiniWaveData> listMiniWaveData;
}

[Serializable]
public class MiniWaveData
{
    public string name;
    public int spawnerID;
    public int pathwayID;
    public float spawnCooldown;
    public List<int> listMonstersID;
}

[Serializable]  
public class LevelLayoutData
{
    public List<SpawnerData> spawnersData;
    public List<PathwayData> pathwaysData;
}

[Serializable]
public class SpawnerData
{
    public int spawnerID;
    public Vector2 position;
}

[Serializable]
public class PathwayData
{
    public int pathwayID;
    public Vector2 startPoint;
    public List<Vector2> waypoints;
}

[Serializable]
public class TowerInLevel
{
    public List<int> towerAllowed;
}

[Serializable]
public class MapData
{
    public List<Vector2> listTowerPositions;
}

