using HyperCasualRunner.Interfaces;
using HyperCasualRunner.Locomotion;
using HyperCasualRunner.ScriptableObjects;
using NaughtyAttributes;
using UnityEngine;

namespace HyperCasualRunner
{
    /// <summary>
    /// Brain of the player object, it acts like a composition root for other components like RunnerMover, PopulationManagerBase. It controls managers, enables or disables them, initializes them,
    /// ticks all the tickables, etc. So it's the most crucial piece when you create player controlled character.
    /// </summary>
    [DisallowMultipleComponent]
    public class Player : MonoBehaviour
    {
        [SerializeField, Required] RunnerMover _runnerMover;
        [SerializeField, Required] PopulationManager _populationManagerBase;
        [SerializeField, Required] InputChannelSO _inputChannelSO;

        [SerializeField] AnimationModifier _animationModifier;

        ITickable[] _tickables;

        private Vector3 _beginPosition;

        void Awake()
        {
            _runnerMover.Initialize();
            //_populationManagerBase.Initialize();

            //foreach (IInitializable initializable in GetComponents<IInitializable>())
            //{
            //    initializable.Initialize(_populationManagerBase);
            //}

            _tickables = GetComponents<ITickable>();

            _beginPosition = transform.position;
        }

        void OnEnable()
        {
            _inputChannelSO.JoystickUpdated += OnJoystickUpdate;
            _inputChannelSO.PointerDown += OnTouchDown;
            _inputChannelSO.PointerUp += OnTouchUp;
        }

        void OnDisable()
        {
            _inputChannelSO.JoystickUpdated -= OnJoystickUpdate;
            _inputChannelSO.PointerDown -= OnTouchDown;
            _inputChannelSO.PointerUp -= OnTouchUp;
        }

        void OnDestroy()
        {
            _runnerMover.OnDestroying();
        }

        void Update()
        {
            if (GameplayManager.Instance.State == GameState.Playing)
            {
                foreach (ITickable tickable in _tickables)
                {
                    tickable.Tick();
                }
            }
        }

        void OnJoystickUpdate()
        {
            _runnerMover.Move();
        }

        void OnTouchDown()
        {
            _runnerMover.TryStartMovement();
            _animationModifier.CurrentAnimationName = _animationModifier.RunAnimationName;
            _animationModifier.ApplyAll();
        }

        void OnTouchUp()
        {
            bool isMovementStopped = _runnerMover.TryStopMovement();
            if (!isMovementStopped)
            {
                return;
            }

            // Stop -> Dừng animation
            _animationModifier.CurrentAnimationName = _animationModifier.IdleAnimationName;
            _animationModifier.ApplyAll();
        }

        public void Unload()
        {
            transform.position = _beginPosition;
            _runnerMover.Reset();
        }

        public void Stop()
        {
            foreach (var entity in _populationManagerBase.ListEntityInCrowd)
            {
                entity.Stop();
            }
        }
    }
}