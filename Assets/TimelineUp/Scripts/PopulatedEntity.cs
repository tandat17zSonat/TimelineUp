using DG.Tweening;
using HyperCasualRunner.Tweening;
using NaughtyAttributes;
using UnityEngine;

namespace HyperCasualRunner.PopulatedEntity
{
    /// <summary>
    /// Used by PopulationManagers. It represents a thing that is populatable so we can create new one or lose one while playing game.
    /// But in examples like Car Evolution, you can use one for the rest of the game and use other stuff like transformation.
    /// </summary>
    [DisallowMultipleComponent]
    public class PopulatedEntity : MonoBehaviour
    {
        [SerializeField, Required] Rigidbody _rigidbody;
        [SerializeField, Required] Collider _collider;
        [SerializeField, Required] Transform _visuals;
        [SerializeField] float _visibilityChangeDuration = 0.5f;

        Tween _scaleTween;
        Tween _jumpTween;

        private int _level;
        private int _damage;
        public int Level { get { return _level; } }
        public int Damage { get { return _damage; } }

        private TransformationModifiable _transformationModifiable;
        private ProjectileShooterModifiable _projectileShootModifiable;
        private AnimationModifiable _animationModifiable;

        public PopulationManager PopulationManagerBase { get; set; }
        public Transform Visuals => _visuals;

        public void Initialize(PopulationManager manager)
        {
            PopulationManagerBase = manager;
            //DisablePhysicsInteraction();
            //gameObject.SetActive(false);

            _transformationModifiable = GetComponent<TransformationModifiable>();
            _projectileShootModifiable = GetComponent<ProjectileShooterModifiable>();
            _animationModifiable = GetComponent<AnimationModifiable>();
        }

        public void SetInfo(int level)
        {
            var gameConfigData = GameManager.Instance.GameConfigData;
            if (level < gameConfigData.ListWarriorDatas.Count)
            {
                _level = level;

                _damage = gameConfigData.ListWarriorDatas[level].Damage;

                _transformationModifiable.Initialize(level);
                _projectileShootModifiable.Initialize(level);
            }
        }

        public void Play()
        {
            _projectileShootModifiable.Play();
        }

        void OnDestroy()
        {
            _scaleTween.Kill();
            _jumpTween.Kill();
        }

        public void Appear()
        {
            _scaleTween.Kill();
            _scaleTween = transform.ShowSmoothly(_visibilityChangeDuration);
            _collider.enabled = true;

            //if (_appearParticleEnabled)
            //{
            //    _appearParticle.Play();
            //}
        }

        public void TakeHit()
        {
            //PopulationManagerBase.Depopulate(this);
        }

        public void Disappear()
        {
            _scaleTween.Kill();
            _scaleTween = transform.HideSmoothly(_visibilityChangeDuration);
            _collider.enabled = false;

            _projectileShootModifiable.Disappear();
            //if (_disappearParticleEnabled)
            //{
            //    _disappearParticle.Play();
            //}
        }


        public void Move(Transform target, float moveSpeed, bool shouldRotate)
        {
            EnablePhysicsInteraction();
            var position = _rigidbody.position;
            Vector3 distance = target.position - position;
            Vector3 force;
            if (distance.sqrMagnitude < 0.04f)
            {
                force = Vector3.zero;
            }
            else
            {
                force = distance.normalized * moveSpeed;
            }
            _rigidbody.velocity = force;
            if (shouldRotate && force.magnitude > 0.2f)
            {
                _visuals.LookAt(position + force, Vector3.up);
            }
        }

        public void DisablePhysicsInteraction()
        {
            _rigidbody.isKinematic = true;
        }

        public void EnablePhysicsInteraction()
        {
            _rigidbody.isKinematic = false;
        }

        public void ResetVisualRotation()
        {
            _visuals.rotation = Quaternion.identity;
        }

    }
}