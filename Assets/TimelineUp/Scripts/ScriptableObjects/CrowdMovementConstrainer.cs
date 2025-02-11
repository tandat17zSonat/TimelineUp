using System;
using DG.Tweening;
using UnityEngine;

namespace HyperCasualRunner.Locomotion
{
    /// <summary>
    /// Use this when you want to constrain the movement based on the entities you have. It's useful in games like Master Of Counts.
    /// </summary>
    [CreateAssetMenu(menuName = "HyperCasualPack/Movement Constrainers/CrowdMovementConstrainer", fileName = "CrowdMovementConstrainer", order = 0)]
    public class CrowdMovementConstrainer : MovementConstrainerBase
    {
        [SerializeField] float _xLimit;

        PopulationManager _crowdManager;
        Tween _delayedCall;
        Bounds _movableBounds;

        private float delayCalculateBounds;
        public override void Initialize(GameObject runnerGameObject)
        {
            _crowdManager = runnerGameObject.GetComponent<PopulationManager>();
            _crowdManager.PopulationChanged += CrowdManager_PopulationChanged;

            delayCalculateBounds = _crowdManager.OrganizeDurationInSeconds;
        }

        void OnDisable()
        {
            if (_crowdManager)
            {
                _crowdManager.PopulationChanged -= CrowdManager_PopulationChanged;
            }
        }

        public override void OnDestroying()
        {
            _delayedCall.Kill();
        }

        void CrowdManager_PopulationChanged()
        {
            _delayedCall.Kill();
            _delayedCall = DOVirtual.DelayedCall(delayCalculateBounds, CalculateBoundaries, false);
        }

        void CalculateBoundaries()
        {
            Bounds bounds = new Bounds(_crowdManager.transform.position, Vector3.zero);
            foreach (PopulatedEntity.PopulatedEntity populatedEntity in _crowdManager.ListEntityInCrowd)
            {
                bounds.Encapsulate(populatedEntity.transform.position);
            }
            _movableBounds = bounds;
        }

        public override Vector3 GetConstrainedPosition(Vector3 position)
        {
            if (position.x > _xLimit - _movableBounds.extents.x)
            {
                return new Vector3(_xLimit - _movableBounds.extents.x, position.y, position.z);
            }
            else if (position.x < -_xLimit + _movableBounds.extents.x)
            {
                return new Vector3(-_xLimit + _movableBounds.extents.x, position.y, position.z);
            }

            return position;
        }
    }
}