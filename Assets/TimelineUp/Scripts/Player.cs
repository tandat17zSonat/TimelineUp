using System;
using HyperCasualRunner.GenericModifiers;

//using HyperCasualRunner.GenericModifiers;
using HyperCasualRunner.Interfaces;
using HyperCasualRunner.Locomotion;
using HyperCasualRunner.PopulationManagers;
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
    public class Player : MonoBehaviour, IInteractor
    {
        [SerializeField, Required] RunnerMover _runnerMover;
        [SerializeField, Required] PopulationManagerBase _populationManagerBase;
        [SerializeField, Required] InputChannelSO _inputChannelSO;
        [SerializeField] int _startingEntityCount;

        [SerializeField] bool _useAnimation;
        [SerializeField, ShowIf(nameof(_useAnimation))] AnimationModifier _animationModifier;

        ITickable[] _tickables;

        private Vector3 _originalPosition;

        void Awake()
        {
            _runnerMover.Initialize();
            _populationManagerBase.Initialize();

            foreach (IInitializable initializable in GetComponents<IInitializable>())
            {
                initializable.Initialize(_populationManagerBase);
            }

            _tickables = GetComponents<ITickable>();

            _originalPosition = transform.position;
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

        void Start()
        {

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

        public void OnInteractionBegin()
        {
            _runnerMover.enabled = false;
            enabled = false;
        }

        public void OnInteractionEnded()
        {
            _runnerMover.enabled = true;
            enabled = true;
        }

        void OnJoystickUpdate(Vector2 direction)
        {
            _runnerMover.Move(direction);
        }

        void OnTouchDown()
        {
            _runnerMover.TryStartMovement();
            if (_useAnimation)
            {
                _animationModifier.PlayLocomotion(1f);
            }
            //else if (_useAnimator)
            //{
            //    _animatorModifier.PlayLocomotion(1f);
            //}
        }

        void OnTouchUp()
        {
            bool isMovementStopped = _runnerMover.TryStopMovement();
            if (!isMovementStopped)
            {
                return;
            }
            if (_useAnimation)
            {
                _animationModifier.PlayLocomotion(0f);
            }
            //else if (_useAnimator)
            //{
            //    _animatorModifier.PlayLocomotion(0f);
            //}
        }

        public void Unload()
        {
            transform.position = _originalPosition;
        }
    }
}