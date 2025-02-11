using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using HyperCasualRunner;
using HyperCasualRunner.CollectableEffects;
using HyperCasualRunner.PopulatedEntity;
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

    private Sequence seqEffect;
    public void Initialize()
    {
        numWarrior = 1;
        level = 0;
        currentDamage = 0;

        var gameConfigData = GameManager.Instance.GameConfigData;
        damageToUpgrade = gameConfigData.GetDamageToUpgradeCollector(level + 1);

        UpdateUI();

    }
    public override void ApplyEffect(PopulatedEntity entity)
    {
        var dict = GameplayManager.Instance.DictWarriorSpawned;
        if(!dict.ContainsKey(level))
        {
            dict[level] = 0;
        }
        dict[level] += numWarrior;
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

        EnableEffect();
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

    private void Update()
    {
        numWarrior = GameplayManager.Instance.NumberInCollector;
        UpdateUI();
    }

    private void EnableEffect()
    {
        seqEffect.Kill();

        seqEffect = DOTween.Sequence();
        seqEffect.Append(transform.DOScale(Vector3.one * 1.1f, 0.1f));
        seqEffect.Append(transform.DOScale(Vector3.one, 0.1f));
    }
}
