using DG.Tweening;
using HyperCasualRunner.PopulatedEntity;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TimelineUp.Obstacle
{
    public class WarriorCollectorEffect : BaseObstacleEffect
    {
        private int level; // Level của collector này = số warrior 
        private int levelWarrior; // Level của warrior đang huấn luyện
        private int damageToUpgrade;
        private int currentDamage;

        [SerializeField] Slider sliderToUpgrade;
        [SerializeField] TMP_Text textLevel;
        [SerializeField] TMP_Text textNum;

        [SerializeField] GameObject[] _levelObjects;
        private Sequence seqEffect;

        public override void Reset()
        {
            level = 1;
            levelWarrior = 0;
            currentDamage = 0;

            var gameConfigData = DataManager.GameplayConfig;
            damageToUpgrade = gameConfigData.GetDamageToUpgradeCollector(levelWarrior + 1);

            UpdateVisual();
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

                for (int i = 0; i < level; i++)
                {
                    //var spawned = populationManager.Spawn(levelWarrior, false);
                    gateSpawn.Add(levelWarrior); ;
                }

                // --------------------
                var dict = GameplayManager.Instance.DictWarriorSpawned;
                if (!dict.ContainsKey(levelWarrior))
                {
                    dict[levelWarrior] = 0;
                }
                dict[levelWarrior] += level;

                Destroy();
            }
        }

        public override void ApplyEffect(Projectile projectile)
        {
            currentDamage += projectile.Damage;

            if (currentDamage > damageToUpgrade)
            {
                levelWarrior += 1;

                var gameConfigData = DataManager.GameplayConfig;
                if (levelWarrior >= gameConfigData.ListWarriorDatas.Count)
                {
                    levelWarrior = gameConfigData.ListWarriorDatas.Count - 1;
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
            damageToUpgrade = gameConfigData.GetDamageToUpgradeCollector(levelWarrior + 1);
        }

        private void Update()
        {
            level = GameplayManager.Instance.NumberInCollector;
            UpdateVisual();
            UpdateUI();
        }

        public void UpdateUI()
        {
            sliderToUpgrade.value = (float)currentDamage / damageToUpgrade;
            textLevel.text = levelWarrior.ToString();
            textNum.text = level.ToString();
        }

        private void UpdateVisual()
        {
            // Đỡ vào vòng for lãng phí
            if (level == 0) return;
            if (_levelObjects[level - 1].activeSelf == true) return;

            foreach (var obj in _levelObjects)
            {
                obj.SetActive(false);
            }
            _levelObjects[level - 1].SetActive(true);
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
