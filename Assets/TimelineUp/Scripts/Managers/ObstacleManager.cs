using System.Collections.Generic;
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

        private float _changeCameraPointZ;

        private void Awake()
        {
            listObstacles = new List<BaseObstacle>();
        }

        public void LoadObstacle(MapData data)
        {
            if (data.ListMainObstacles.Count == 0) return;

            // Sinh các obstacle chính
            foreach (var mainObstacleData in data.ListMainObstacles)
            {
                var obs = Spawn(mainObstacleData.Type);

                float x = mainObstacleData.x, z = mainObstacleData.z;
                obs.transform.position = new Vector3(x, 0, z);

                obs.Initialize();

                var properties = mainObstacleData.Properties;
                // check lock
                if (mainObstacleData.Locked == true)
                {
                    obs.SetLock(mainObstacleData.GetLockNumber());
                }

                // check move
                if (mainObstacleData.Move == true)
                {
                    obs.SetRun();
                }

                obs.SetProperties(properties);
                listObstacles.Add(obs);
            }

            // Sinh các endblock 
            var deltaZ = 5;
            _changeCameraPointZ = listObstacles[listObstacles.Count - 1].transform.position.z;
            var positionZ = _changeCameraPointZ + 20; // khoảng cách từ obstacle cuối tới endblock
            var gameConfigData = DataManager.GameplayConfig;
            for (int order = 0; order < gameConfigData.ListEndBlockConfigs.Count; order++)
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
                    spawned.Initialize();

                    var endBlockEffect = spawned.GetComponent<EndBlockEffect>();
                    endBlockEffect.SetInfo(order);

                    listObstacles.Add(spawned);
                }
            }

            // Sinh cổng về đích
            positionZ += deltaZ * 4;
            var gateFinish = Spawn(ObstacleType.GateFinish);
            gateFinish.Initialize();
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

        public float GetChangeCameraPoint()
        {
            return _changeCameraPointZ;
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
