using DG.Tweening;
using HyperCasualRunner.PopulatedEntity;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TimelineUp.Obstacle
{
    public class WarriorCollectorEffect : BaseObstacleEffect
    {
        private int numWarrior;
        private int level;
        private int damageToUpgrade;
        private int currentDamage;

        [SerializeField] Slider sliderToUpgrade;
        [SerializeField] TMP_Text textLevel;
        [SerializeField] TMP_Text textNum;

        private Sequence seqEffect;

        public override void Reset()
        {
            numWarrior = 1;
            level = 0;
            currentDamage = 0;

            var gameConfigData = DataManager.GameplayConfig;
            damageToUpgrade = gameConfigData.GetDamageToUpgradeCollector(level + 1);

            UpdateUI();
        }

        public override void ApplyEffect(PopulatedEntity entity)
        {
            if (!IsCollider)
            {
                IsCollider = true;
                // Thêm nhân vật ở gate spawn
                var populationManager = GameplayManager.Instance.PopulationManager;
                var obstacleManager = GameplayManager.Instance.ObstacleManager;
                var gateSpawn = obstacleManager.GetNextGateSpawn();

                for (int i = 0; i < numWarrior; i++)
                {
                    var spawned = populationManager.Spawn(level, false);
                    gateSpawn.Add(spawned);
                }

                // --------------------
                var dict = GameplayManager.Instance.DictWarriorSpawned;
                if (!dict.ContainsKey(level))
                {
                    dict[level] = 0;
                }
                dict[level] += numWarrior;

                Destroy();
            }
        }

        public override void ApplyEffect(Projectile projectile)
        {
            currentDamage += projectile.Damage;

            if (currentDamage > damageToUpgrade)
            {
                level += 1;

                var gameConfigData = DataManager.GameplayConfig;
                if (level >= gameConfigData.ListWarriorDatas.Count)
                {
                    level = gameConfigData.ListWarriorDatas.Count - 1;
                }
                currentDamage = 0;
                UpdateInfo();
                // chưa tính phần damage thừa
            }

            EnableEffect();
            UpdateUI();
        }

        private void UpdateInfo()
        {
            var gameConfigData = DataManager.GameplayConfig;
            damageToUpgrade = gameConfigData.GetDamageToUpgradeCollector(level + 1);
        }

        private void Update()
        {
            numWarrior = GameplayManager.Instance.NumberInCollector;
            UpdateUI();
        }

        public void UpdateUI()
        {
            sliderToUpgrade.value = (float)currentDamage / damageToUpgrade;
            textLevel.text = level.ToString();
            textNum.text = numWarrior.ToString();
        }

        private void EnableEffect()
        {
            seqEffect.Kill();

            seqEffect = DOTween.Sequence();
            seqEffect.Append(transform.DOScale(Vector3.one * 1.1f, 0.1f));
            seqEffect.Append(transform.DOScale(Vector3.one, 0.1f));
        }
    }

}
