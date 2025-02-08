using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HyperCasualRunner.PopulatedEntity;
using HyperCasualRunner.Modifiables;
using HyperCasualRunner.Interfaces;
using HyperCasualRunner;
using HyperCasualRunner.PopulationManagers;

public class WarriorController : PopulatedEntity
{
    private WarriorData _warriorData;

    private TransformationModifiable _transformationModifiable;
    private ProjectileShooterModifiable _projectileShooterModifiable;


    private void Awake()
    {
        _transformationModifiable = GetComponent<TransformationModifiable>();
        _projectileShooterModifiable = GetComponent<ProjectileShooterModifiable>();

    }

    public override void SetInfo(int level)
    {
        var gameConfigData = GameManager.Instance.GameConfigData;
        _warriorData = gameConfigData.GetWarriorData(level);

        _transformationModifiable.SetLevel(level);
        _projectileShooterModifiable.SetProjectileData();
    }

    public ProjectileData GetProjectileData()
    {
        return _warriorData.ProjectileData;
    }

}

public class WarriorData
{
    public int Type;
    public int Level;
    public int Speed;

    public ProjectileData ProjectileData;
}


public class ProjectileData
{
    public int Damage;
    public float Speed;
    public float Range;
}
