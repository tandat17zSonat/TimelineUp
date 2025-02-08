using System.Collections;
using System.Collections.Generic;
using HyperCasualRunner;
using HyperCasualRunner.CollectableEffects;
using HyperCasualRunner.PopulationManagers;
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

    public override void ApplyEffect(PopulationManagerBase populationManager)
    {
        // Có người đi qua bình thường
        dictWarriorSpawned = GameplayManager.Instance.DictWarriorSpawned;
        foreach(var kvp in dictWarriorSpawned)
        {
            var level = kvp.Key;
            var number = kvp.Value;
            populationManager.AddPopulation(level, number);
        }
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
