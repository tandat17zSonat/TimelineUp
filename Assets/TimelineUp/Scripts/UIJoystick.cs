using HyperCasualRunner.ScriptableObjects;
using UnityEngine;

namespace HyperCasualRunner
{
    /// <summary>
    /// It's a general purpose joystick.
    /// </summary>
    public class UIJoystick : MonoBehaviour
    {
        const float TAP_THRESHOLD = 0.2f;

        [SerializeField, Tooltip("background of the joystick gameObject")] CanvasGroup _background;
        [SerializeField, Tooltip("input channel to get the inputs, with this, input values will be distributed across other components")] InputChannelSO _inputChannelSO;
        [SerializeField, Range(0f, 1f), Tooltip("opacity of the gameObject when you press it")] float _pressedOpacity = 0.5f;
        [SerializeField, Range(0f, 1f), Tooltip("Max range the joystick center can go")] float _maxRange = 0.2f;
        [SerializeField, Tooltip("Whether you can control joystick horizontally")] bool _horizontalAxisEnabled = true;
        [SerializeField, Tooltip("Whether you can control joystick vertically")] bool _verticalAxisEnabled = true;
        //[SerializeField, Tooltip("If enabled, center follows the player's finger position.")] bool _isCenterDynamic;

        Transform _knobTransform;
        Vector2 _joystickValue;
        float _initialOpacity;
        float _touchTime;

        void Awake()
        {
            _initialOpacity = _background.alpha;
            _knobTransform = _background.transform.GetChild(0);
            _inputChannelSO.SetActiveInput += InputChannelSoOnSetActiveInput;
        }

        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                _inputChannelSO.OnPointerDown();

                SetJoystickPosition();
            }
            else if (Input.GetMouseButton(0))
            {
                CalculateAndUpdateInput();

                _touchTime += Time.deltaTime;
            }
            else if (Input.GetMouseButtonUp(0))
            {
                _inputChannelSO.OnPointerUp();

                if (_touchTime <= TAP_THRESHOLD)
                {
                    _inputChannelSO.OnTapped();
                }

                _touchTime = 0f;
                ResetJoystick();
            }

            if (_horizontalAxisEnabled || _verticalAxisEnabled)
            {
                _inputChannelSO.OnJoystickUpdated(_joystickValue);
            }
        }

        void OnDestroy()
        {
            _inputChannelSO.SetActiveInput -= InputChannelSoOnSetActiveInput;
        }

        void InputChannelSoOnSetActiveInput(bool activeness)
        {
            gameObject.SetActive(activeness);
        }

        void SetJoystickPosition()
        {
            _background.transform.position = Input.mousePosition;
            _background.alpha = _pressedOpacity;
        }

        void CalculateAndUpdateInput()
        {
            Vector2 deltaPosition = (Vector2)Input.mousePosition - (Vector2)_background.transform.position;

            if (!_horizontalAxisEnabled)
            {
                deltaPosition.x = 0;
            }

            if (!_verticalAxisEnabled)
            {
                deltaPosition.y = 0;
            }

            _joystickValue.x = EvaluateInputValue(deltaPosition.x);
            _joystickValue.y = EvaluateInputValue(deltaPosition.y);
            _joystickValue = Vector2.ClampMagnitude(_joystickValue, 1f);
            //Debug.Log($"_joystickValue {_joystickValue.x} {_joystickValue.y}");

            //if (_isCenterDynamic)
            //{
            //    _background.transform.position = Input.mousePosition;
            //}

            // clamp the knobTransform's position
            int maxRangeInPixel = Mathf.RoundToInt(_maxRange * UnityEngine.Screen.width);
            Vector2 clampedKnobPosition = (Vector2)_background.transform.position + Vector2.ClampMagnitude(deltaPosition, maxRangeInPixel);
            _knobTransform.position = clampedKnobPosition;
        }

        void ResetJoystick()
        {
            _knobTransform.position = _background.transform.position;
            _joystickValue.x = 0f;
            _joystickValue.y = 0f;
            _inputChannelSO.OnJoystickUpdated(_joystickValue);

            _background.alpha = _initialOpacity;
        }

        float EvaluateInputValue(float input)
        {
            return Mathf.InverseLerp(0f, _maxRange, Mathf.Abs(input / UnityEngine.Device.Screen.width)) * Mathf.Sign(input);
        }
    }
}