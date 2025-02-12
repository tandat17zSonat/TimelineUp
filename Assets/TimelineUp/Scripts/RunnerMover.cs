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
        [SerializeField, ReadOnly] float _forwardMoveSpeed;
        [SerializeField] bool _canControlForwardMovement;

        [Header("Movement Constrain")]
        [SerializeField] bool _shouldConstrainMovement;
        [SerializeField, ShowIf(nameof(_shouldConstrainMovement))] MovementConstrainerBase _movementConstrainer;

        bool _canGoForward = true; // 1 yes, 0 no

        bool _canControl = false;
        Vector3 _oldPosition;
        float _oldTouchPositionX;

        public float ForwardMoveSpeed { get => _forwardMoveSpeed; set => _forwardMoveSpeed = value; }

        public void Initialize()
        {
            _movementConstrainer.Initialize(gameObject);
        }

        public void OnDestroying()
        {
            _movementConstrainer.OnDestroying();
        }

        public void TryStartMovement()
        {
            ForwardMoveSpeed = GameplayManager.Instance.Speed;
            _canGoForward = true;

            // Lưu lại những giá trị lần chạm đầu
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
        /// Move được gọi liên tục trong hàm update của Joystick
        /// </summary>
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

            if (!_canGoForward)
            {
                position.z = transform.position.z;
            }

            // Giới hạn lại vùng di chuyển
            var finalPosition = _movementConstrainer.GetConstrainedPosition(position);
            transform.position = finalPosition;
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