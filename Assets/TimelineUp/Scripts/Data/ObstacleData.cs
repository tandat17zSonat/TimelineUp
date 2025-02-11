using System.Collections.Generic;
using UnityEngine;

public class ObstacleData
{
    public List<(ObstacleType, Vector3)> ListObstacles;

    public ObstacleData()
    {
        ListObstacles = new();
        ListObstacles.Add((ObstacleType.ExpBlock, new Vector3(2, 0, 50)));
        ListObstacles.Add((ObstacleType.WarriorCollector, new Vector3(-2, 0, 80)));
        ListObstacles.Add((ObstacleType.GateSpawn, new Vector3(0, 0, 110)));
    }

}