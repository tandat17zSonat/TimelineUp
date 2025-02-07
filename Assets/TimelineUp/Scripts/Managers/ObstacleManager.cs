using System.Collections;
using System.Collections.Generic;
using TimelineUp.Obstacle;
using TimelineUp.ScriptableObjects;
using UnityEngine;

public class ObstacleManager : MonoBehaviour
{
    [SerializeField] ObstaclePool obstaclePool;

    private List<ObstacleBase> listObstacles;

    public void Awake()
    {
        listObstacles = new List<ObstacleBase>();
    }

    public void LoadObstacle()
    {
        for (int i = 50; i < 150; i += 15)
        {
            var obs = obstaclePool.Get<GateObstacle>();
            obs.transform.position = new Vector3(Random.Range(-2, 2), 0, i);
            listObstacles.Add(obs);
        }
    }

    public void Unload()
    {
        foreach (var obstacle in listObstacles)
        {
            obstaclePool.Release(obstacle);
        }

        listObstacles.Clear();
    }
}
