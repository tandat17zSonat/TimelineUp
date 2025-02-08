using System;
using System.Collections;
using System.Collections.Generic;
using HyperCasualRunner;
using HyperCasualRunner.CollectableEffects;
using HyperCasualRunner.PopulationManagers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WarriorCollectorEffect : CollectableEffectBase
{
    private int numWarrior;
    private int level;
    private int damageToUpgrade;
    private int currentDamage;

    [SerializeField] Slider sliderToUpgrade;
    [SerializeField] TMP_Text textLevel;
    [SerializeField] TMP_Text textNum;


    public void Initialize()
    {
        numWarrior = 1;
        level = 0;
        currentDamage = 0;

        var gameConfigData = GameManager.Instance.GameConfigData;
        damageToUpgrade = gameConfigData.GetDamageToUpgradeCollector(level + 1);

        UpdateUI();

    }
    public override void ApplyEffect(PopulationManagerBase populationManager)
    {

    }

    public override void ApplyHitEffect(Projectile projectile)
    {
        currentDamage += projectile.Damage;

        if (currentDamage > damageToUpgrade)
        {
            level += 1;
            currentDamage = 0;
            UpdateInfo();
            // chưa tính phần damage thừa
        }

        UpdateUI();
    }

    private void UpdateInfo()
    {
        var gameConfigData = GameManager.Instance.GameConfigData;
        damageToUpgrade = gameConfigData.GetDamageToUpgradeCollector(level + 1);
    }

    public void UpdateUI()
    {
        sliderToUpgrade.value = (float) currentDamage / damageToUpgrade;
        textLevel.text = level.ToString();
        textNum.text = numWarrior.ToString();
    }
}
