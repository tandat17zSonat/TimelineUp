using System;
using System.Collections;
using System.Collections.Generic;
using DarkTonic.PoolBoss;
using HyperCasualRunner.CollectableEffects;
using Sirenix.Serialization.Internal;
using TimelineUp.Obstacle;
using TimelineUp.ScriptableObjects;
using UnityEngine;

public class ObstacleManager : MonoBehaviour
{
    [SerializeField] Transform container;
    [SerializeField] Transform gateSpawnPrefab;
    [SerializeField] Transform expBlockPrefab;
    [SerializeField] Transform warriorCollectorPrefab;
    [SerializeField] Transform gateFinishPrefab;
    [SerializeField] Transform endBlockPrefab;

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

        // Sinh các endblock
        var deltaZ = 3;
        var positionZ = listObstacles[listObstacles.Count - 1].transform.position.z + 5;
        var gameConfigData = GameManager.Instance.GameConfigData;
        for (int order = 0; order < gameConfigData.ListEndBlockDatas.Count; order++)
        {
            positionZ += deltaZ;
            var positions = new List<Vector3>();
            positions.Add(new Vector3(-2, 0, positionZ));
            positions.Add(new Vector3(0, 0, positionZ));
            positions.Add(new Vector3(2, 0, positionZ));

            for (int i = 0; i < 3; i++)
            {
                var spawned = PoolBoss.Spawn(endBlockPrefab, Vector3.zero, Quaternion.identity, container);
                spawned.transform.position = positions[i];

                var endBlockEffect = spawned.GetComponent<EndBlockEffect>();
                endBlockEffect.Initialize(order);
            }
        }

        // Sinh cổng về đích
        positionZ += deltaZ;
        var gateFinish = PoolBoss.Spawn(gateFinishPrefab, Vector3.zero, Quaternion.identity, container);
        gateFinish.transform.position = new Vector3(0, 0, positionZ);
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
        ListObstacles.Add((ObstacleType.WarriorCollector, new Vector3(-2, 0, 40)));
        ListObstacles.Add((ObstacleType.GateSpawn, new Vector3(0, 0, 60)));
    }

}

public enum ObstacleType
{
    ExpBlock,

    WarriorCollector,
    GateSpawn,

    EndBlock,

    GateFinish,
}
