using DarkTonic.PoolBoss;
using DG.Tweening;
using HyperCasualRunner.PopulatedEntity;
using UnityEngine;

namespace TimelineUp.Obstacle
{
    public class EnermyEffect : BaseObstacleEffect
    {
        [Header("")]
        [SerializeField] Transform _projectilePrefab;
        [SerializeField] Transform _container;

        [SerializeField] Transform _spawnPoint;

        private int _hp;

        private bool _fire = false;
        private float _timer;

        private float _fireRate;
        private float _fireSpeed;
        private float _fireRange;

        public override void ApplyEffect(PopulatedEntity entity)
        {
            _fire = false;
            Destroy();
        }

        public override void ApplyEffect(Projectile projectile)
        {
            _hp -= projectile.Damage;
            if( _hp < 0)
            {
                _fire = false;
                Destroy();
            }
        }

        public override void Reset()
        {
            _hp = 200;

            _fireRate = 1;
            _fireSpeed = -10;
            _fireRange = 20;

            _fire = true;
        }

        private void Update()
        {
            if (_fire)
            {
                _timer += Time.deltaTime;

                var _shootInterval = 1.0f / _fireRate;
                if (_timer >= _shootInterval)
                {
                    _timer -= _shootInterval;
                    Shoot();
                }
            }
        }

        private void Shoot()
        {
            var spawned = PoolBoss.Spawn(_projectilePrefab, _spawnPoint.position, Quaternion.identity, null);
            //spawned.transform.position = _spawnPoint.position;
            ProjectileEnermy projectile = spawned.GetComponent<ProjectileEnermy>();
            projectile.Initialize(1, _fireSpeed, _fireRange);
            projectile.Fire();
        }
    }

}