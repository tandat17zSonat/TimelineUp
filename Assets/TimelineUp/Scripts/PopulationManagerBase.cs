using System;
using System.Collections.Generic;
using HyperCasualRunner.Interfaces;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Events;

namespace HyperCasualRunner.PopulationManagers
{
    /// <summary>
    /// Base class for creating custom PopulationManagers.
    /// </summary>
    public abstract class PopulationManagerBase : MonoBehaviour
    {
        [SerializeField, Range(1, 500), Tooltip("This limit can't be exceedable later on, so give it a higher number")]
        protected int maxPopulationCount = 50;

        [SerializeField, Tooltip("Prefab to use for populated entities")]
        protected PopulatedEntity.PopulatedEntity populatedEntityPrefab;

        [SerializeField, Required(), Tooltip("Populated entities will be spawned as a child of this game object")]
        protected Transform instantiateRoot;

        [SerializeField] protected UnityEvent OnLastEntityDied;

        List<PopulatedEntity.PopulatedEntity> _shownPopulatedEntities = new List<PopulatedEntity.PopulatedEntity>();
        List<PopulatedEntity.PopulatedEntity> _hiddenPopulatedEntities = new List<PopulatedEntity.PopulatedEntity>();

        public int MaxPopulationCount => maxPopulationCount;

        public List<PopulatedEntity.PopulatedEntity> ShownPopulatedEntities
        {
            get => _shownPopulatedEntities;
            protected set => _shownPopulatedEntities = value;
        }
        public List<PopulatedEntity.PopulatedEntity> HiddenPopulatedEntities
        {
            get => _hiddenPopulatedEntities;
            protected set => _hiddenPopulatedEntities = value;
        }

        public Action<int> PopulationChanged;
        public Action<PopulatedEntity.PopulatedEntity> PopulatedEntityEnabled;
        public Action<PopulatedEntity.PopulatedEntity> PopulatedEntityDisabled;

        public virtual void Initialize()
        {
            for (int i = 0; i < maxPopulationCount; i++)
            {
                PopulatedEntity.PopulatedEntity instantiated = Instantiate(populatedEntityPrefab, instantiateRoot);
                instantiated.Initialize(this);
                _hiddenPopulatedEntities.Add(instantiated);
            }
        }

        protected abstract void Populate(int level);
        public abstract void Depopulate(PopulatedEntity.PopulatedEntity populatedEntity);

        protected virtual void OnPopulationChanged()
        {
            PopulationChanged?.Invoke(_shownPopulatedEntities.Count);
            if (_shownPopulatedEntities.Count == 0)
            {
                OnLastEntityDied.Invoke();
            }
        }

        public void AddPopulation(int level, int amount)
        {
            if (_shownPopulatedEntities.Count >= maxPopulationCount)
            {
                return;
            }

            if (_shownPopulatedEntities.Count + amount > maxPopulationCount)
            {
                amount = maxPopulationCount - _shownPopulatedEntities.Count;
            }

            for (int i = 0; i < amount; i++)
            {
                Populate(level);
            }
            OnPopulationChanged();
        }

        public void Unload()
        {
            foreach (var entity in _shownPopulatedEntities)
            {
                entity.Disappear();
                _hiddenPopulatedEntities.Add(entity);
            }
            _shownPopulatedEntities.Clear();
        }
    }
}