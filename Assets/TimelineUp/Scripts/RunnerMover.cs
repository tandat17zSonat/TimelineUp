using System;
using NaughtyAttributes;
using UnityEngine;

namespace HyperCasualRunner.Locomotion
{
    /// <summary>
    /// Customizable object mover class. Specifically designed for runner games.
    /// </summary>
    public class RunnerMover : MonoBehaviour
    {
        [Header("Properties")]
        [SerializeField] float _forwardMoveSpeed = 3f;
        [SerializeField] bool _canControlForwardMovement;
        //[SerializeField, Tooltip("If it's true, object will rotate itself so it's up is aligned with the ground's up direction")] bool _shouldOrientUpDirectionToGround;
        //[SerializeField, ShowIf(nameof(_shouldOrientUpDirectionToGround)), Tooltip("Detection range of the ground when orienting itself")] float _orientUpDirectionRange = 2f

        //[SerializeField] bool _turnToMovingDirection;
        //[SerializeField, ShowIf(nameof(_turnToMovingDirection)), Required]
        //GameObject _gameObjectToTurn;
        //[SerializeField, ShowIf(nameof(_turnToMovingDirection))] float _maxRotatingLimit = 15f;
        //[SerializeField, ShowIf(nameof(_turnToMovingDirection))] float _rotationSpeed = 9f;

        [Header("Movement Constrain")]
        [SerializeField] bool _shouldConstrainMovement;
        [SerializeField, ShowIf(nameof(_shouldConstrainMovement))] MovementConstrainerBase _movementConstrainer;

        bool _canGoForward = true; // 1 yes, 0 no
        Vector3 _initialForwardDirection;

        bool _canControl = false;
        Vector3 _oldPosition;
        float _oldTouchPositionX;

        public float ForwardMoveSpeed { get => _forwardMoveSpeed; set => _forwardMoveSpeed = value; }

        public void Initialize()
        {
            _movementConstrainer.Initialize(gameObject);
            _initialForwardDirection = transform.forward;
        }

        public void OnDestroying()
        {
            _movementConstrainer.OnDestroying();
        }

        public void TryStartMovement()
        {
            ForwardMoveSpeed = GameManager.Instance.GameConfigData.GetForwardMoveSpeed();
            _canGoForward = true;

            _canControl = true;
            _oldPosition = transform.position;
            _oldTouchPositionX = ConvertScreenToGround().x;
        }

        public bool TryStopMovement()
        {
            _canControl = false;
            if (_canControlForwardMovement) _canGoForward = false;
            return _canControlForwardMovement;
        }

        /// <summary>
        /// If you feed this method with Vector2, it will move the RunnerMover towards that.
        /// </summary>
        /// <param name="moveDirection">Move Direction to go. It has to be normalized. It will be multiplied by the movement speed.</param>
        public void Move()
        {
            if (!enabled)
            {
                return;
            }

            var position = new Vector3(transform.position.x, transform.position.y, transform.position.z + Time.deltaTime * _forwardMoveSpeed);
            if (_canControl)
            {
                position.x = ConvertScreenToGround().x - _oldTouchPositionX + _oldPosition.x;
            }

            if (!_canControl && _canControlForwardMovement)
            {
                position.z = transform.position.z;
            }
            var finalPosition = _movementConstrainer.GetConstrainedPosition(position);
            transform.position = finalPosition;

            //if (_shouldOrientUpDirectionToGround)
            //{
            //    bool isNearGround = Physics.Raycast(transform.position, Vector3.down * _orientUpDirectionRange, out RaycastHit hitInfo, 2f);
            //    if (isNearGround) _groundNormal = hitInfo.normal;
            //}

            //if (_turnToMovingDirection)
            //{
            //    // TODO: This part might cause problem when rotating
            //    Quaternion targetRotation = Quaternion.LookRotation(_initialForwardDirection + horizontalMovementRaw * _maxRotatingLimit, _groundNormal);

            //    _gameObjectToTurn.transform.rotation = Quaternion.Slerp(
            //        _gameObjectToTurn.transform.rotation, targetRotation, _rotationSpeed * Time.deltaTime);
            //}
        }

        Vector3 ConvertScreenToGround()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit[] hits = Physics.RaycastAll(ray);

            foreach (RaycastHit hit in hits)
            {
                if (hit.collider.gameObject.tag == "Ground")
                {
                    return hit.point;
                }
            }
            Debug.Log($"Can't ConvertScreenToGround");
            _canControl = false;
            return Vector3.zero;
        }
    }
}