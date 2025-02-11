using System;
using DarkTonic.PoolBoss;
using HyperCasualRunner.CollectableEffects;
using NaughtyAttributes;
using UnityEngine;

namespace HyperCasualRunner
{
    /// <summary>
    /// It gathers all possible CollectableEffects and applies them when PopulationManagerBase contacts with Collectable's collider. You can use multiple effects
    /// and all of them would be applied to the PopulationManagerBase.
    /// </summary>
    public class Collectable : MonoBehaviour
    {
        [InfoBox("This component centralizes collectableEffects by binding all the effects on the same gameObject")]
        [SerializeField] CollectableEffectBase[] _collectableEffects;
        [SerializeField] GameObject _visuals;
        [SerializeField] bool _particleFeedbackEnabled;
        [ShowIf(nameof(_particleFeedbackEnabled)), Required("you need to assign a particle if you enable Particle Feedback Enabled")]
        [SerializeField] ParticleSystem _collectingParticleFeedback;

        [ReadOnly] public bool IsCollected;

        public event Action<Collectable> Collected;

        public void Init()
        {
            Reset();
        }

        void OnTriggerEnter(Collider other)
        {
            // Warrior hoặc Projectile đâm vào
            if (IsCollected) return;

            if (other)
            {
                if (other.TryGetComponent(out PopulatedEntity.PopulatedEntity entity))
                {
                    ApplyCollectEffects(entity);
                }
                else if (other.TryGetComponent(out Projectile projectile))
                {
                    ApplyHitEffect(projectile);
                    return;
                }
            }

            IsCollected = true;
            Collected?.Invoke(this);
        }

        void ApplyCollectEffects(PopulatedEntity.PopulatedEntity entity)
        {
            foreach (CollectableEffectBase collectableEffectBase in _collectableEffects)
            {
                collectableEffectBase.ApplyEffect(entity);
            }

            if (_particleFeedbackEnabled)
            {
                _collectingParticleFeedback.Play();
            }

            PoolBoss.Despawn(this.transform);
        }

        void ApplyHitEffect(Projectile projectile)
        {
            foreach (CollectableEffectBase collectableEffectBase in _collectableEffects)
            {
                collectableEffectBase.ApplyHitEffect(projectile);
            }
            projectile.Release();
        }

        [Button("Setup Collectable", EButtonEnableMode.Editor)]
        void Reset()
        {
            IsCollected = false;
            _collectableEffects = GetComponents<CollectableEffectBase>();
            Collider[] colls = GetComponents<Collider>();
            foreach (var item in colls)
            {
                if (item.isTrigger)
                {
                    return;
                }
            }

            Debug.LogError("You need to have at least one collider with isTrigger enabled!");
        }
    }
}
