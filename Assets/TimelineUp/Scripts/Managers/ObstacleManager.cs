using System.Collections.Generic;
using DarkTonic.PoolBoss;
using HyperCasualRunner;
using HyperCasualRunner.CollectableEffects;
using UnityEngine;

public class ObstacleManager : MonoBehaviour
{
    [SerializeField] Transform container;
    [SerializeField] Transform gateSpawnPrefab;
    [SerializeField] Transform expBlockPrefab;
    [SerializeField] Transform warriorCollectorPrefab;
    [SerializeField] Transform gateFinishPrefab;
    [SerializeField] Transform gateProjectileRangePrefab;
    [SerializeField] Transform gateProjectileRatePrefab;
    [SerializeField] Transform endBlockPrefab;

    private List<CollectableEffectBase> listObstacles;

    public void Awake()
    {
        listObstacles = new List<CollectableEffectBase>();
    }

    public void LoadObstacle(ObstacleData data)
    {

        if (data.ListObstacles.Count == 0) return;
        foreach (var (type, pos) in data.ListObstacles)
        {
            var obs = Spawn(type);
            obs.transform.position = pos;
            listObstacles.Add(obs);
        }

        // Sinh các endblock 
        var deltaZ = 4;
        var positionZ = listObstacles[listObstacles.Count - 1].transform.position.z + 20; // khoảng cách từ obstacle cuối tới endblock
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

    public CollectableEffectBase Spawn(ObstacleType type)
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
                    var expBlock = spawned.GetComponent<ExpBlockEffect>();
                    expBlock.Initialize();
                    break;
                }
            case ObstacleType.GateFinish:
                {
                    spawned = PoolBoss.Spawn(gateFinishPrefab, Vector3.zero, Quaternion.identity, container);
                    break;
                }
            case ObstacleType.GateProjectileRange:
                {
                    spawned = PoolBoss.Spawn(gateProjectileRangePrefab, Vector3.zero, Quaternion.identity, container);
                    var effect = spawned.GetComponent<GateProjectileRangeEffect>();
                    effect.Initialize();
                    break;
                }
            case ObstacleType.GateProjectileRate:
                {
                    spawned = PoolBoss.Spawn(gateProjectileRatePrefab, Vector3.zero, Quaternion.identity, container);
                    var effect = spawned.GetComponent<GateProjectileRateEffect>();
                    effect.Initialize();
                    break;
                }
        }

        spawned.GetComponent<Collectable>().Init();
        return spawned.GetComponent<CollectableEffectBase>();
    }

    public GateSpawnEffect GetNextGateSpawn()
    {
        foreach (var obs in listObstacles)
        {
            if (obs.Type == ObstacleType.GateSpawn)
            {
                return obs as GateSpawnEffect;
            }
        }

        return null;
    }

    public void Remove(CollectableEffectBase obs)
    {
        listObstacles.Remove(obs);
        Debug.LogWarning($"{obs.gameObject.name}");
        PoolBoss.Despawn(obs.transform);
    }
}


public enum ObstacleType
{
    ExpBlock,

    WarriorCollector,
    GateSpawn,
    GateProjectileRange,
    GateProjectileRate,

    EndBlock,

    GateFinish,

}
