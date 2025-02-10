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

        [SerializeField, Tooltip("input channel to get the inputs, with this, input values will be distributed across other components")] InputChannelSO _inputChannelSO;
        [SerializeField, Tooltip("Whether you can control joystick horizontally")] bool _horizontalAxisEnabled = true;
        [SerializeField, Tooltip("Whether you can control joystick vertically")] bool _verticalAxisEnabled = true;
        //[SerializeField, Tooltip("If enabled, center follows the player's finger position.")] bool _isCenterDynamic;

        float _touchTime;


        void Update()
        {
            if (GameplayManager.Instance.State == GameState.Playing)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    _inputChannelSO.OnPointerDown();
                }
                else if (Input.GetMouseButton(0))
                {
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
                }

                if (_horizontalAxisEnabled || _verticalAxisEnabled)
                {
                    _inputChannelSO.OnJoystickUpdated();
                }

            }
        }
    }
}