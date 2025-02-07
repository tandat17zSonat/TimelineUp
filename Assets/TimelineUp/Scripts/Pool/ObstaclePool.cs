using System;
using System.Collections.Generic;
using TimelineUp.Obstacle;
using UnityEngine;
using UnityEngine.Pool;

namespace TimelineUp.ScriptableObjects
{
    [CreateAssetMenu(fileName = "Obstacle Pool", menuName = "TimelineUp/Obstacle Pool", order = 0)]
    public class ObstaclePool : ScriptableObject
    {
        [SerializeField] ObstacleBase[] _obstaclePrefabs;
        [SerializeField] bool _collectionCheck;
        [SerializeField] int _defaultCapacity = 50;
        [SerializeField] int _maxSize = 100;

        private Dictionary<Type, ObjectPool<ObstacleBase>> _dictPool;

        void OnEnable()
        {
            _dictPool = new Dictionary<Type, ObjectPool<ObstacleBase>>();
            foreach (var prefab in _obstaclePrefabs)
            {
                var pool = new ObjectPool<ObstacleBase>(() =>
                {
                    ObstacleBase instantiated = Instantiate(prefab);
                    instantiated.Pool = this;
                    return instantiated;
                }, shape =>
                {
                    shape.gameObject.SetActive(true);
                }, shape =>
                {
                    shape.gameObject.SetActive(false);
                }, shape =>
                {
                    Destroy(shape.gameObject);
                }, _collectionCheck, _defaultCapacity, _maxSize);

                _dictPool.Add(prefab.GetType(), pool);
            }
        }

        public T Get<T>() where T : ObstacleBase
        {
            _dictPool.TryGetValue(typeof(T), out ObjectPool<ObstacleBase> pool);
            return pool.Get() as T;
        }

        public void Release(ObstacleBase prefab)
        {
            _dictPool.TryGetValue(prefab.GetType(), out ObjectPool<ObstacleBase> pool);
            pool.Release(prefab);
        }
    }
}
