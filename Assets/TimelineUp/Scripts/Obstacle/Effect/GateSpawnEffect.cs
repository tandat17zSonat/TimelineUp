using System;
using System.Collections.Generic;
using HyperCasualRunner.PopulatedEntity;
using TMPro;
using UnityEngine;

namespace TimelineUp.Obstacle
{
    public class GateSpawnEffect : BaseObstacleEffect
    {
        [SerializeField] float offsetY;
        [SerializeField] float deltaY;
        [SerializeField] Transform[] stands;

        private Dictionary<int, int> dictWarriorSpawned = new();

        private List<PopulatedEntity> listEntityInGate;
        private List<int> listNumWarrior;

        private void Awake()
        {
            listNumWarrior = new List<int>();
            foreach (var stand in stands)
            {
                listNumWarrior.Add(0);
            }

            listEntityInGate = new List<PopulatedEntity>();
        }

        public override void Reset()
        {
            for (int i = 0; i < listNumWarrior.Count; i++)
            {
                listNumWarrior[i] = 0;
            }

            var populationManager = GameplayManager.Instance.PopulationManager;
            foreach (var entity in listEntityInGate)
            {
                populationManager.Remove(entity);
            }
            listEntityInGate.Clear();
            dictWarriorSpawned.Clear();
        }

        public override void ApplyEffect(PopulatedEntity entity)
        {
            // Có người đi qua bình thường
            var populationManager = GameplayManager.Instance.PopulationManager;
            dictWarriorSpawned = GameplayManager.Instance.DictWarriorSpawned;
            foreach (var kvp in dictWarriorSpawned)
            {
                var level = kvp.Key;
                var number = kvp.Value;
                for (int i = 0; i < number; i++)
                {
                    var spawned = populationManager.Spawn(level);
                    populationManager.AddToCrowd(spawned);
                    spawned.Play();
                }
            }
            Destroy();
        }

        public override void ApplyEffect(Projectile projectile)
        {
            //Debug.Log("Projectile collision gatespawn");
        }

        public void Add(PopulatedEntity entity)
        {
            listEntityInGate.Add(entity);
            return;
        }

        public override void Destroy()
        {
            var populationManager = GameplayManager.Instance.PopulationManager;
            foreach (var entity in listEntityInGate)
            {
                entity.gameObject.SetActive(false);
                populationManager.Remove(entity);
            }
            listEntityInGate.Clear();
            base.Destroy();
        }

        public Vector3 GetFreeSlot()
        {
            var populationManager = GameplayManager.Instance.PopulationManager;
            int max = Mathf.Max(listNumWarrior.ToArray());
            for (int i = 0; i < stands.Length; i++)
            {
                if (listNumWarrior[i] < max)
                {
                    listNumWarrior[i] += 1;

                    return stands[i].position + Vector3.up * (offsetY + (listNumWarrior[i] - 1) * deltaY);
                }
            }

            listNumWarrior[0] += 1;
            return stands[0].position + Vector3.up * (offsetY + (listNumWarrior[0] - 1) * deltaY);
        }
    }

}
