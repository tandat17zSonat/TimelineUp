using System;
using UnityEngine;

namespace HyperCasualRunner.ScriptableObjects
{
    /// <summary>
    /// Small class for distributing the input values that we collect from Joystick
    /// </summary>
    [CreateAssetMenu(menuName = "HyperCasualPack/Channels/Input Channel", fileName = "Input Channel", order = 0)]
    public class InputChannelSO : ScriptableObject
    {
        public event Action JoystickUpdated;
        public event Action PointerDown;
        public event Action PointerUp;
        public event Action Tapped;

        /// <summary>
        /// Cập nhật liên tục khi sử dụng Joystick
        /// </summary>
        public void OnJoystickUpdated()
        {
            JoystickUpdated?.Invoke();
        }

        public void OnPointerDown()
        {
            PointerDown?.Invoke();
        }

        public void OnPointerUp()
        {
            PointerUp?.Invoke();
        }

        public void OnTapped()
        {
            Tapped?.Invoke();
        }
    }
}
