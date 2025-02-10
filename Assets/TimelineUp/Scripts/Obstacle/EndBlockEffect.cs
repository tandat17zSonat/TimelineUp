using System;
using DarkTonic.PoolBoss;
using DG.Tweening;
using HyperCasualRunner.PopulationManagers;
using NaughtyAttributes;
using TimelineUp.Obstacle;
using TimelineUp.ScriptableObjects;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace HyperCasualRunner.CollectableEffects
{
    public class EndBlockEffect : CollectableEffectBase
    {
        [SerializeField] Slider sliderHp;
        [SerializeField] TMP_Text textNum;
        private int hp;
        private int maxHp;

        private Sequence seqEffect;

        public void Initialize(int order)
        {
            var gameConfigData = GameManager.Instance.GameConfigData;
            hp = gameConfigData.GetEndBlockHp(order);
            maxHp = hp;

            UpdateUi();
        }

        public override void ApplyEffect(PopulatedEntity.PopulatedEntity entity)
        {
            var populationManagerBase = entity.PopulationManagerBase;
            populationManagerBase.Depopulate(entity);

            if(populationManagerBase.ShownPopulatedEntities.Count == 0)
            {
                GameplayManager.Instance.SetResult(GameState.Loss);
            }
        }

        public override void ApplyHitEffect(Projectile projectile)
        {
            hp -= projectile.Damage;
            if(hp < 0)
            {
                PoolBoss.Despawn(this.transform);
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

        [Button("Setup", EButtonEnableMode.Editor)]
        void Reset()
        {

        }

        void UpdateUi()
        {
            textNum.text = hp.ToString();
            sliderHp.value = hp / maxHp;
        }
    }
}