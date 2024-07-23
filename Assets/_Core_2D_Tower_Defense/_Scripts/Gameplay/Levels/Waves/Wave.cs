using System.Collections.Generic;
using UnityEngine;

public class Wave : MonoBehaviour
{
    public int waveID;
    public WaveData waveData;
    public List<MiniWave> listMiniWaves;
    public List<MiniWaveData> listMiniWavesData;

    public void InitWave(WaveData data)
    {
        waveData = data;
        listMiniWavesData = waveData.listMiniWaveData;
        
        CreateMiniWaves();
    }

    private void CreateMiniWaves()
    {
        for (int i = 0; i < listMiniWavesData.Count; i++)
        {
            SpawnMiniWave(i);
        }
    }

    private void SpawnMiniWave(int id)
    {
        var miniWave = Instantiate(LevelManager.Instance.database.prefabData.miniWavePrefab, transform);
        miniWave.miniWaveID = id;
        miniWave.wave = this;
        miniWave.Init(listMiniWavesData[id]);
        listMiniWaves.Add(miniWave);
    }
    
    public void CheckIfAllMiniWaveClear()
    {
        if (listMiniWaves.Count == 0)
        {
            this.PostEvent(EventID.On_Spawn_Next_Wave, waveID);
            LevelManager.Instance.listWaves.Remove(this);
            Destroy(gameObject);
        }
    }
}
