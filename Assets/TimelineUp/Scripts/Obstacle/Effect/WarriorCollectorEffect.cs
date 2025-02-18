using System.Collections.Generic;
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
        //[SerializeField] TMP_Text textNum;

        [SerializeField] GameObject[] _levelObjects;
        private Sequence seqEffect;

        public override void Reset()
        {
            level = 1;
            levelWarrior = 0;
            currentDamage = 0;

            UpdateInfo();

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

                if (gateSpawn != null)
                {
                    // Hiệu ứng nhân vật chạy
                    var start = transform.position;
                    start.x = -4;

                    var end = gateSpawn.transform.position;
                    end.x = -4;

                    var listPosInCollector = GetPositionInCollector();

                    for (int i = 0; i < level; i++)
                    {
                        var spawned = populationManager.Spawn(levelWarrior, false);
                        spawned.transform.position = listPosInCollector[i];

                        var offset = listPosInCollector[i] - transform.position;
                        var freeSlotInGateSpawn = gateSpawn.GetFreeSlot();

                        var characterRunToGate = spawned.GetComponent<CharacterRunToGate>();
                        characterRunToGate.SetInfo(start + offset, end + offset, freeSlotInGateSpawn, () =>
                        {
                            gateSpawn.Add(spawned);
                        });
                        characterRunToGate.Run();
                        //gateSpawn.Add(levelWarrior);
                    }

                    // --------------------
                    var dict = GameplayManager.Instance.DictWarriorSpawned;
                    if (!dict.ContainsKey(levelWarrior))
                    {
                        dict[levelWarrior] = 0;
                    }
                    dict[levelWarrior] += level;
                }
                else
                {
                    // Nếu không có gate spawn ở cuối thì cộng trực tiếp
                    var dict = GameplayManager.Instance.DictWarriorSpawned;
                    for (int i = 0; i < level; i++)
                    {
                        var spawned = populationManager.Spawn(levelWarrior);
                        populationManager.AddToCrowd(spawned);
                        spawned.Play();

                    }
                }

                Destroy();
            }
        }

        public override void ApplyEffect(Projectile projectile)
        {
            currentDamage += projectile.Damage;

            if (currentDamage > damageToUpgrade && damageToUpgrade != 0)
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
            if (levelWarrior < gameConfigData.WarriorCollectorConfig.GetMaxWarriorLevel())
            {
                damageToUpgrade = gameConfigData.GetDamageToUpgradeCollector(levelWarrior + 1);
            }
            else
            {
                damageToUpgrade = 0;
            }
        }

        private void Update()
        {
            level = GameplayManager.Instance.NumberInCollector;
            UpdateVisual();
            UpdateUI();
        }

        public void UpdateUI()
        {
            if (damageToUpgrade != 0 && damageToUpgrade != null)
            {
                sliderToUpgrade.value = (float)currentDamage / damageToUpgrade;

            }
            textLevel.text = $"Level {levelWarrior + 1}";
            //textNum.text = level.ToString();
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

        private List<Vector3> GetPositionInCollector()
        {
            var list = new List<Vector3>();
            foreach (Transform child in _levelObjects[level - 1].transform)
            {
                list.Add(child.position);
            }
            return list;
        }
    }

}
