﻿using System.Collections.Generic;
using DarkTonic.PoolBoss;
using TimelineUp.SO;
using UnityEngine;

namespace TimelineUp.Obstacle
{
    public class ObstacleManager : MonoBehaviour
    {
        [SerializeField] Transform container;
        [SerializeField] ObstacleSO obstacleSO;

        private List<BaseObstacle> listObstacles;

        private void Awake()
        {
            listObstacles = new List<BaseObstacle>();
        }

        public void LoadObstacle(DataInMatch data)
        {
            if (data.ListMainObstacles.Count == 0) return;
            
            // Sinh các obstacle chính
            foreach (var mainObstacleData in data.ListMainObstacles)
            {
                var obs = Spawn(mainObstacleData.Type);
                obs.Initialize(mainObstacleData.Locked);

                int x = mainObstacleData.x, z = mainObstacleData.z;
                obs.transform.position = new Vector3(x, 0, z);
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
                    var spawned = Spawn(ObstacleType.EndBlock);
                    spawned.transform.position = positions[i];
                    spawned.Initialize(false);

                    var endBlockEffect = spawned.GetComponent<EndBlockEffect>();
                    endBlockEffect.SetInfo(order);

                    listObstacles.Add(spawned);
                }
            }

            // Sinh cổng về đích
            positionZ += deltaZ;
            var gateFinish = Spawn(ObstacleType.GateFinish);
            gateFinish.Initialize(false);
            gateFinish.transform.position = new Vector3(0, 0, positionZ);
            listObstacles.Add(gateFinish);
        }

        public void Unload()
        {
            foreach (var obstacle in listObstacles)
            {
                PoolBoss.Despawn(obstacle.transform);
            }

            listObstacles.Clear();
        }

        public BaseObstacle Spawn(ObstacleType type)
        {
            Transform spawned = null;

            var prefab = obstacleSO.GetPrefabs(type);
            spawned = PoolBoss.Spawn(prefab, Vector3.zero, Quaternion.identity, container);
            return spawned.GetComponent<BaseObstacle>();
        }

        public GateSpawnEffect GetNextGateSpawn()
        {
            foreach (var obs in listObstacles)
            {
                if (obs.Type == ObstacleType.GateSpawn)
                {
                    return obs.GetComponent<GateSpawnEffect>();
                }
            }

            return null;
        }

        public void Remove(BaseObstacle obs)
        {
            listObstacles.Remove(obs);
            PoolBoss.Despawn(obs.transform);
        }
    }

    [System.Serializable]
    public enum ObstacleType
    {
        ExpBlock,
        WarriorCollector,
        GateSpawn,
        GateProjectileRange,
        GateProjectileRate,
        Enermy,

        EndBlock,
        GateFinish
    }

}
