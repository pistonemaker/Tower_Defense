using System.Collections.Generic;
using UnityEngine;


public class MapToData : MonoBehaviour
{
    [SerializeField] private LevelData levelData;
    [SerializeField] private List<Transform> plots;

    private void Start()
    {
        if (levelData.mapData.listTowerPositions.Count > 0)
        {
            return;
        }
        
        var listTowerPositions = levelData.mapData.listTowerPositions;
        
        for (int i = 0; i < plots.Count; i++)
        {
            listTowerPositions.Add(plots[i].position);
        }
    }
}
