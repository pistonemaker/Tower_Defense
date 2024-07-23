using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Database", menuName = "Data/Database")]
public class Database : ScriptableObject
{
    public Tower towerPrefab;
    public PrefabData prefabData;
    public List<MonsterData> listMonstersData;
    public List<TowerData> listTowersData;
}

[Serializable]
public class TowerData
{
    public string towerName;
    public int towerID;
    public bool canAirShoot;
    public List<TurretSpecification> listSpecifications;
}

[Serializable]
public class TurretSpecification
{
    public string name;
    public int spiritStoneToBuy;
    public int spiritStoneGetWhenSale;
    public float damage;
    public Sprite caseSprite;
    public Sprite towerSprite;
    public Bullet bulletPrefab;
    public float cooldown;
    public float shootingRange;
}
    
    
[Serializable]
public class MonsterData 
{
    public string monsterName;
    public int monsterID;
    public int spiritStoneAmount;
    public int damage;
    public Monster monsterPrefab;
    public float maxHP;
    public float speed = 2f;
}

[Serializable]
public class PrefabData
{
    public Spawner spawnerPrefab;
    public Pathway pathwayPrefab;
    public Wave wavePrefab;
    public MiniWave miniWavePrefab;
    public TowerPosition towerPosition;
}