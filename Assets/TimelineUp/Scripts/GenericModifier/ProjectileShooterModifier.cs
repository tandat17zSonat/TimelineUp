using HyperCasualRunner.Interfaces;
using UnityEngine;

public class ProjectileShooterModifier : GenericModifier<ProjectileShooterModifiable>, ITickable
{
    private float _shootInterval;
    private float _timer = 0f;

    public override void Apply(ProjectileShooterModifiable modifiable)
    {
        modifiable.Shoot();
    }

    public void Tick()
    {
        _timer += Time.deltaTime;

        _shootInterval = 1.0f / GameplayManager.Instance.ProjectileRate;
        if (_timer >= _shootInterval)
        {
            _timer -= _shootInterval;
            ApplyAll();
        }
    }
}