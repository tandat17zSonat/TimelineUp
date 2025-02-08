using System;
using System.Collections;
using System.Collections.Generic;
using DarkTonic.PoolBoss;
using TimelineUp.Obstacle;
using TimelineUp.ScriptableObjects;
using UnityEngine;

public class ObstacleManager : MonoBehaviour
{
    [SerializeField] ObstaclePool obstaclePool;

    [SerializeField] Transform container;
    [SerializeField] Transform gateSpawnPrefab;
    [SerializeField] Transform expBlockPrefab;
    [SerializeField] Transform warriorCollectorPrefab;
    [SerializeField] Transform gateFinishPrefab;

    private List<ObstacleBase> listObstacles;

    public void Awake()
    {
        listObstacles = new List<ObstacleBase>();
    }

    public void LoadObstacle(ObstacleData data)
    {
        foreach (var (type, pos) in data.ListObstacles)
        {
            var obs = Spawn(type);
            obs.transform.position = pos;
            listObstacles.Add(obs);
        }
    }

    public void Unload()
    {
        foreach (var obstacle in listObstacles)
        {
            PoolBoss.Despawn(obstacle.transform);
        }

        listObstacles.Clear();
    }

    public ObstacleBase Spawn(ObstacleType type)
    {
        Transform spawned = null;

        switch (type)
        {
            case ObstacleType.WarriorCollector:
                {
                    spawned = PoolBoss.Spawn(warriorCollectorPrefab, Vector3.zero, Quaternion.identity, container);
                    var warriorCollectorEffect = spawned.GetComponent<WarriorCollectorEffect>();
                    warriorCollectorEffect.Initialize();
                    break;
                }
            case ObstacleType.GateSpawn:
                {
                    spawned = PoolBoss.Spawn(gateSpawnPrefab, Vector3.zero, Quaternion.identity, container);
                    var gateSpawn = spawned.GetComponent<GateSpawnEffect>();
                    gateSpawn.Initialize();
                    break;
                }
            case ObstacleType.ExpBlock:
                {
                    spawned = PoolBoss.Spawn(expBlockPrefab, Vector3.zero, Quaternion.identity, container);
                    break;
                }
            case ObstacleType.GateFinish:
                {
                    spawned = PoolBoss.Spawn(gateFinishPrefab, Vector3.zero, Quaternion.identity, container);
                    break;
                }
        }
        return spawned.GetComponent<ObstacleBase>();
    }
}

public class ObstacleData
{
    public List<(ObstacleType, Vector3)> ListObstacles;

    public ObstacleData()
    {
        ListObstacles = new();
        ListObstacles.Add((ObstacleType.ExpBlock, new Vector3(2, 0, 30)));
        ListObstacles.Add((ObstacleType.WarriorCollector, new Vector3(-2, 0, 50)));
        ListObstacles.Add((ObstacleType.GateSpawn, new Vector3(0, 0, 100)));
        ListObstacles.Add((ObstacleType.GateFinish, new Vector3(0, 0, 150)));
    }

}

public enum ObstacleType
{
    ExpBlock,

    WarriorCollector,
    GateSpawn,

    GateFinish
}
