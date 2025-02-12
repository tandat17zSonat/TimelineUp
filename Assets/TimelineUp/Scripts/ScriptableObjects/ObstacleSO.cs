using System.Collections.Generic;
using TimelineUp.Obstacle;
using UnityEngine;

namespace TimelineUp.SO
{
    [CreateAssetMenu(menuName = "TimelineUp/ObstacleSO", fileName = "ObstacleSO")]
    public class ObstacleSO : ScriptableObject
    {
        [SerializeField] Transform[] obstaclePrefabs;

        Dictionary<ObstacleType, Transform> mapPrefabs;

        public Transform GetPrefabs(ObstacleType type)
        {
            if (mapPrefabs == null)
            {
                mapPrefabs = new Dictionary<ObstacleType, Transform>();
                foreach (Transform prefab in obstaclePrefabs)
                {
                    var obs = prefab.GetComponent<BaseObstacle>();
                    mapPrefabs.Add(obs.Type, prefab);
                }
            }

            return mapPrefabs[type];
        }
    }

}
