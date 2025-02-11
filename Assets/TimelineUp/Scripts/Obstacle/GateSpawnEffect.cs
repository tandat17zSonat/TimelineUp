using System.Collections;
using System.Collections.Generic;
using DarkTonic.PoolBoss;
using HyperCasualRunner;
using HyperCasualRunner.CollectableEffects;
using HyperCasualRunner.PopulatedEntity;
using TMPro;
using UnityEngine;

public class GateSpawnEffect : CollectableEffectBase
{
    [SerializeField] TMP_Text textNumberWarrior;

    private int numberWarrior;
    private Dictionary<int, int> dictWarriorSpawned = new();

    public void Initialize()
    {
        dictWarriorSpawned.Clear();
    }

    public override void ApplyEffect(PopulatedEntity entity)
    {
        // Có người đi qua bình thường
        var populationManager = GameplayManager.Instance.PopulationManager;
        dictWarriorSpawned = GameplayManager.Instance.DictWarriorSpawned;
        foreach (var kvp in dictWarriorSpawned)
        {
            var level = kvp.Key;
            var number = kvp.Value;
            for(int i = 0; i < number; i++)
            {
                var spawned = populationManager.Spawn(level);
                populationManager.AddToCrowd(spawned);
            }
        }

        PoolBoss.Despawn(transform);
    }

    public override void ApplyHitEffect(Projectile projectile)
    {

    }

    private void Update()
    {
        dictWarriorSpawned = GameplayManager.Instance.DictWarriorSpawned;
        numberWarrior = 0;
        foreach (var item in dictWarriorSpawned.Values)
        {
            numberWarrior += item;
        }
        textNumberWarrior.text= numberWarrior.ToString();
    }
}
