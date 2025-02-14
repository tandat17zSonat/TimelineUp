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

        [Header("Camera")]
        [SerializeField] GameObject Camera0;

        bool _canGoForward = true; // 1 yes, 0 no

        bool _canControl = false;

        // Liên quan tới đẩy lùi
        bool _isPushBack = false;
        float _timePushBack = 0.5f;

        Vector3 _oldPosition;
        float _oldTouchPositionX;

        public float ForwardMoveSpeed { get => _forwardMoveSpeed; set => _forwardMoveSpeed = value; }

        public void Initialize()
        {
            _movementConstrainer.Initialize(gameObject);
            _isPushBack = false;
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

            if (_isPushBack)
            {
                // Bị tác động bởi đẩy lùi
                var pos = transform.position;
                pos.z = pos.z - Time.deltaTime * _forwardMoveSpeed * 3;
                transform.position = pos;
                _timePushBack -= Time.deltaTime;
                if (_timePushBack < 0)
                {
                    _isPushBack = false;
                    _timePushBack = 0.5f;
                }
            }
            else
            {
                // Điều khiển bằng input
                var pos = new Vector3(transform.position.x, transform.position.y, transform.position.z + Time.deltaTime * _forwardMoveSpeed);
                if (_canControl)
                {
                    pos.x = ConvertScreenToGround().x - _oldTouchPositionX + _oldPosition.x;
                }

                if (!_canGoForward)
                {
                    pos.z = transform.position.z;
                }

                // Giới hạn lại vùng di chuyển
                var finalPosition = _movementConstrainer.GetConstrainedPosition(pos);
                transform.position = finalPosition;
            }

            // Gần tới đích thì thay đổi góc camera
            var obstacleManager = GameplayManager.Instance.ObstacleManager;
            if ( transform.position.z > obstacleManager.GetChangeCameraPoint())
            {
                Camera0.SetActive(false);
            }
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

        public void SetPushback()
        {
            _isPushBack = true;
        }

        public void Reset()
        {
            Camera0.SetActive(true);
        }
    }
}