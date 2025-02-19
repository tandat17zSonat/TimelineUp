using System;
using System.Collections.Generic;

namespace TimelineUp.Obstacle
{
    [System.Serializable]
    public class MapData
    {
        public List<ObstacleDataInMap> ListMainObstacles;

        public MapData()
        {
            ListMainObstacles = new List<ObstacleDataInMap>();
        }

        public int Create(ObstacleType type, int x, int z)
        {
            int id = ListMainObstacles.Count;
            ListMainObstacles.Add(new ObstacleDataInMap(id, type, x, z, false, false, new List<int>()));
            Sort();
            return id;
        }

        public void Sort()
        {
            ListMainObstacles.Sort((a, b) =>
            {
                if (a.z == b.z) return 0;
                return a.z <= b.z ? -1 : 1;
            });

            //int count = 0;
            //foreach (var obs in ListMainObstacles)
            //{
            //    obs.Id = count;
            //    count += 1;
            //}
        }
    }

    [System.Serializable]
    public class ObstacleDataInMap
    {
        public int Id;
        public ObstacleType Type;
        public float x;
        public float z;
        public bool Locked;
        public bool Move;
        public List<int> Properties;

        public ObstacleDataInMap(int id, ObstacleType obstacleType, float x, float z, bool locked, bool move, List<int> properties)
        {
            Id = id;
            Type = obstacleType;
            this.x = x;
            this.z = z;
            Locked = locked;
            Move = move;
            Properties = properties;
        }

        public int GetLockNumber()
        {
            return Properties[Properties.Count - 1];
        }
    }


}

