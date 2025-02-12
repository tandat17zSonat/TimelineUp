using System.Collections.Generic;

namespace TimelineUp.Obstacle
{
    public class DataInMatch
    {
        public List<MainObstacleDataInMatch> ListMainObstacles;

        public DataInMatch()
        {
            ListMainObstacles = new();
            ListMainObstacles.Add(new MainObstacleDataInMatch(ObstacleType.GateProjectileRate, 2, 150, false));
            ListMainObstacles.Add(new MainObstacleDataInMatch(ObstacleType.GateProjectileRange, -2, 180, true));
            ListMainObstacles.Add(new MainObstacleDataInMatch(ObstacleType.ExpBlock, 2, 220, false));
            ListMainObstacles.Add(new MainObstacleDataInMatch(ObstacleType.WarriorCollector, -2, 250, true));
            ListMainObstacles.Add(new MainObstacleDataInMatch(ObstacleType.GateSpawn, 0, 300, false));
        }
    }

    public class MainObstacleDataInMatch
    {
        public ObstacleType Type;
        public int x;
        public int z;
        public bool Locked;

        public MainObstacleDataInMatch(ObstacleType obstacleType, int x, int z, bool locked)
        {
            Type = obstacleType;
            this.x = x;
            this.z = z;
            Locked = locked;
        }
    }


}

