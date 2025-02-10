﻿using System.Collections;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

namespace HyperCasualRunner.PopulationManagers
{
    /// <summary>
    /// It controls the PopulatedEntities, like in the Master Of Counts.
    /// </summary>
    public class CrowdManager : PopulationManagerBase
    {
        [SerializeField, Tooltip("Allowed time for regrouping of populated entities")]
        float _organizeDurationInSeconds = 1f;
        [SerializeField, Tooltip("Move speed of individual populated entity when they are organizing or battling")]
        float _entityMoveSpeed = 2f;

        [Tooltip("All populated entities will try to regroup themselves based on this point")]
        public Transform CrowdOrganizingPoint;

        public float OrganizeDurationInSeconds { get { return _organizeDurationInSeconds; } }

        Tween _delayedCallTween;
        WaitForSeconds _organizeWait;
        Coroutine _organizeCoroutine;
        Transform _moveTarget;
        bool _canMovePopulatedEntities;
        bool _shouldRotate;
        float _currentEntityMoveSpeed;
        readonly float _organizingDelayDuration = 1.5f;

        public override void Initialize()
        {
            base.Initialize();
            _organizeWait = new WaitForSeconds(_organizeDurationInSeconds);
            _currentEntityMoveSpeed = _entityMoveSpeed;
        }

        // TODO: battling game
        void FixedUpdate()
        {
            if (!_canMovePopulatedEntities)
            {
                return;
            }

            // Cho các warrior đừng gần lại nhau
            foreach (PopulatedEntity.PopulatedEntity shownPopulatedEntity in ShownPopulatedEntities)
            {
                shownPopulatedEntity.Move(_moveTarget, _currentEntityMoveSpeed, _shouldRotate);
            }
        }

        void OnDestroy()
        {
            KillOrganizingCalls();
        }

        void StartDelayedOrganizing()
        {
            _delayedCallTween.Kill();
            _delayedCallTween = DOVirtual.DelayedCall(_organizingDelayDuration, StartOrganizing, false);
        }

        public void StartOrganizing()
        {
            KillOrganizingCalls();
            _organizeCoroutine = StartCoroutine(Co_Organize());
        }

        protected override void OnPopulationChanged()
        {
            base.OnPopulationChanged();
            StartOrganizing();
        }

        void StopEntitiesMovement()
        {
            _canMovePopulatedEntities = false;
            foreach (PopulatedEntity.PopulatedEntity populatedEntity in ShownPopulatedEntities)
            {
                populatedEntity.DisablePhysicsInteraction();
            }
        }

        void Move(Transform target)
        {
            KillOrganizingCalls();
            _moveTarget = target;
            _canMovePopulatedEntities = true;
        }

        void KillOrganizingCalls()
        {
            if (_organizeCoroutine != null) StopCoroutine(_organizeCoroutine);
            _delayedCallTween.Kill();
        }

        void ResetEntityRotations()
        {
            _shouldRotate = false;
            foreach (PopulatedEntity.PopulatedEntity shownPopulatedEntity in ShownPopulatedEntities)
            {
                shownPopulatedEntity.ResetVisualRotation();
            }
        }

        IEnumerator Co_Organize()
        {
            ResetEntityRotations();
            _currentEntityMoveSpeed = _entityMoveSpeed;
            Move(CrowdOrganizingPoint);

            yield return _organizeWait;

            StopEntitiesMovement();
        }

        protected override void Populate(int level)
        {
            int hiddenListCount = HiddenPopulatedEntities.Count;
            if (hiddenListCount == 0)
            {
                return;
            }

            PopulatedEntity.PopulatedEntity populated = HiddenPopulatedEntities[hiddenListCount - 1];
            HiddenPopulatedEntities.RemoveAt(hiddenListCount - 1);
            float rndX = Random.Range(-0.5f, 0.5f);
            float rndZ = Random.Range(-0.5f, 0.5f);

            populated.SetInfo(level);
            populated.transform.localPosition = new Vector3(rndX, 0f, rndZ);
            populated.Appear();
            ShownPopulatedEntities.Add(populated);
            PopulatedEntityEnabled?.Invoke(populated);
        }

        public override void Depopulate(PopulatedEntity.PopulatedEntity entity)
        {
            ShownPopulatedEntities.Remove(entity);
            HiddenPopulatedEntities.Add(entity);
            entity.Disappear();
            if (!_canMovePopulatedEntities)
            {
                StartDelayedOrganizing();
            }
            base.OnPopulationChanged();
        }

    }
}