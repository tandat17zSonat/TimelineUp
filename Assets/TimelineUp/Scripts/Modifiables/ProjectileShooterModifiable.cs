using DarkTonic.PoolBoss;
using HyperCasualRunner.PopulatedEntity;
using UnityEngine;

public class ProjectileShooterModifiable : BaseModifiable
{
    [Header("")]
    [SerializeField] PopulatedEntity _entity;

    [Header("")]
    [SerializeField] Transform _projectilePrefab;
    [SerializeField] Transform _container;

    [SerializeField] Transform _spawnPoint;

    public override void Initialize(int level)
    {

    }

    public void Shoot()
    {
        Debug.Log("Shoot");
        var spawned = PoolBoss.Spawn(_projectilePrefab, Vector3.zero, Quaternion.identity, null);
        if (spawned == null) Debug.Log("Can't spawned projectile");
        else
        {
            Debug.Log($"Spawned projectile {spawned.name}");
        }
        spawned.transform.SetPositionAndRotation(_spawnPoint.position, transform.rotation); 

        Projectile projectile = spawned.GetComponent<Projectile>();
        projectile.Initialize(_entity.Level);
        projectile.Fire();
    }
}
