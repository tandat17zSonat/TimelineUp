using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using TimelineUp.Data;
using UnityEngine;

namespace TimelineUp.Obstacle
{
    [System.Serializable]
    public class MapData
    {
        public List<ObstacleDataInMap> ListMainObstacles;
    }

    [System.Serializable]
    public class ObstacleDataInMap
    {
        public ObstacleType Type;
        public float x;
        public float z;
        public bool Locked;
        public bool Move;
        public List<int> Properties;

        public ObstacleDataInMap(ObstacleType obstacleType, float x, float z, bool locked, bool move, List<int> properties)
        {
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

