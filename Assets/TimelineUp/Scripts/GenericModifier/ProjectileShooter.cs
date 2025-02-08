using DarkTonic.PoolBoss;
using HyperCasualRunner.ScriptableObjects;
using UnityEngine;

namespace HyperCasualRunner
{
    /// <summary>
    /// ProjectileShooterModifiable uses this class for picking right Projectile for the active PopulatedEntity. Use this when you have multiple gameObjects (like playerLevel1, playerLevel2) and
    /// everyone of them shoots different projectile. You can see the example in Evolution Of Crowds demo scene. 
    /// </summary>
    public class ProjectileShooter : MonoBehaviour
    {
        [Header("Pooling")]
        [SerializeField] Transform container;
        [SerializeField] Transform projectilePrefab;

        [Header("")]
        [SerializeField] Transform _projectileSpawnPoint;

        public Transform ProjectileSpawnPoint { get => _projectileSpawnPoint; set => _projectileSpawnPoint = value; }

        public Projectile Get()
        {
            var projectile = PoolBoss.Spawn(projectilePrefab, Vector3.zero, Quaternion.identity, container).GetComponent<Projectile>();
            return projectile;
        }
    }
}
