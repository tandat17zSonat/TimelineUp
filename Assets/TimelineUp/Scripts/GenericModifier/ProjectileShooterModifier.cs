using HyperCasualRunner.Interfaces;
using UnityEngine;

public class ProjectileShooterModifier : GenericModifier<ProjectileShooterModifiable>
{
    public override void Apply(ProjectileShooterModifiable modifiable)
    {
        modifiable.Play();
    }
}