using System.Collections.Generic;

namespace TimelineUp.Obstacle
{
    public class DataInMatch
    {
        public List<MainObstacleDataInMatch> ListMainObstacles;

        public DataInMatch()
        {
            ListMainObstacles = new();
            ListMainObstacles.Add(new MainObstacleDataInMatch(ObstacleType.Enermy, 2, 30, false));
            ListMainObstacles.Add(new MainObstacleDataInMatch(ObstacleType.GateProjectileRate, 0, 50, false));
            ListMainObstacles.Add(new MainObstacleDataInMatch(ObstacleType.GateProjectileRange, 0, 80, true));
            ListMainObstacles.Add(new MainObstacleDataInMatch(ObstacleType.ExpBlock, 2, 120, false));
            ListMainObstacles.Add(new MainObstacleDataInMatch(ObstacleType.WarriorCollector, -2, 150, true));
            ListMainObstacles.Add(new MainObstacleDataInMatch(ObstacleType.GateSpawn, 0, 200, false));
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

