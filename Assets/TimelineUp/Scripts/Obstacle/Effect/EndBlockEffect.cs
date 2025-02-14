using DG.Tweening;
using HyperCasualRunner.PopulatedEntity;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TimelineUp.Obstacle
{
    public class EndBlockEffect : BaseObstacleEffect
    {
        [SerializeField] Slider sliderHp;
        [SerializeField] TMP_Text textNum;
        private int hp;
        private float maxHp;
        private int coin;

        private Sequence seqEffect;

        public override void ApplyEffect(PopulatedEntity entity)
        {
            var populationManager = GameplayManager.Instance.PopulationManager;
            populationManager.RemoveEntityFromCrowd(entity);

            if( populationManager.ListEntityInCrowd.Count == 0 )
            {
                GameplayManager.Instance.SetResult(GameState.Loss);
            }
        }

        public override void ApplyEffect(Projectile projectile)
        {
            hp -= projectile.Damage;
            if(hp <= 0)
            {
                var playerData = DataManager.PlayerData;
                var gameplayConfig = DataManager.GameplayConfig;
                playerData.Coin += coin;
                Destroy();
                return;
            }
            EnableEffect();
            UpdateUi();
        }

        private void EnableEffect()
        {
            if(seqEffect != null) seqEffect.Kill();

            seqEffect = DOTween.Sequence();
            seqEffect.Append(transform.DOScale(Vector3.one * 1.1f, 0.1f));
            seqEffect.Append(transform.DOScale(Vector3.one, 0.1f));
        }

        void UpdateUi()
        {
            textNum.text = hp.ToString();
            sliderHp.value = hp / maxHp;
        }

        public void SetInfo(int order)
        {
            var gameConfigData = DataManager.GameplayConfig;
            var endBlockConfig = gameConfigData.ListEndBlockConfigs[order];
            hp = endBlockConfig.Hp;
            maxHp = hp;

            coin = endBlockConfig.Coin;

            UpdateUi();
        }

        public override void Reset()
        {

        }
    }
}