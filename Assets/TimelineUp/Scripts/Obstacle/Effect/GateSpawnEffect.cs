using System.Collections.Generic;
using HyperCasualRunner.PopulatedEntity;
using TMPro;
using UnityEngine;

namespace TimelineUp.Obstacle
{
    public class GateSpawnEffect : BaseObstacleEffect
    {
        [SerializeField] TMP_Text textNumberWarrior;
        [SerializeField] Transform[] stands;

        private int numberWarrior;
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

        }

        private void Update()
        {
            dictWarriorSpawned = GameplayManager.Instance.DictWarriorSpawned;
            numberWarrior = 0;
            foreach (var item in dictWarriorSpawned.Values)
            {
                numberWarrior += item;
            }
            textNumberWarrior.text = numberWarrior.ToString();
        }

        public void Add(PopulatedEntity entity)
        {
            listEntityInGate.Add(entity);
            entity.DisableCollider();
            int max = Mathf.Max(listNumWarrior.ToArray());
            for (int i = 0; i < stands.Length; i++)
            {
                if (listNumWarrior[i] < max)
                {
                    entity.transform.position = stands[i].position;
                    listNumWarrior[i] += 1;
                    return;
                }
            }

            entity.transform.position = stands[0].position;
            listNumWarrior[0] += 1;
            return;
        }

        public override void Destroy()
        {
            var populationManager = GameplayManager.Instance.PopulationManager;
            foreach (var entity in listEntityInGate)
            {
                populationManager.Remove(entity);
            }
            listEntityInGate.Clear();
            base.Destroy();
        }
    }

}
