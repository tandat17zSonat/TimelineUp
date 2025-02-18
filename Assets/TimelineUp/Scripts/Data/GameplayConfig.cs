using System.Collections.Generic;
using static Cinemachine.DocumentationSortingAttribute;

namespace TimelineUp.Data
{
    [System.Serializable]
    public class GameplayConfig
    {
        public int TimelineId;
        public int EraId;

        public List<WarriorConfig> ListWarriorDatas; // config của warrior
        public WarriorCollectorConfig WarriorCollectorConfig; // config của collector
        public List<EndBlockConfig> ListEndBlockConfigs;// config của endblock

        public BoosterCapacityConfig BoosterCapacityConfig;
        public List<BaseBoosterConfig> ListBoosterConfigs;

        public int GetExpToUpgradeWarriorNumber(int currentLevel)
        {
            return WarriorCollectorConfig.ExpToUpgradeNumberWarrior[currentLevel - 1];
        }

        public int GetDamageToUpgradeCollector(int level)
        {
            return WarriorCollectorConfig.DamageToUpgradeLevel[level];
        }

        public int GetEndBlockHp(int order)
        {
            return ListEndBlockConfigs[order].Hp;
        }

        public BaseBoosterConfig GetBoosterConfig(BoosterType type)
        {
            if (type == BoosterType.Capacity)
            {
                return BoosterCapacityConfig;
            }
            return ListBoosterConfigs[(int)type];
        }

        //public GameplayConfig()
        //{
        //    // config của warrior----------
        //    ListWarriorDatas = new List<WarriorConfig>();
        //    for (int i = 0; i < 10; i++)
        //    {
        //        ListWarriorDatas.Add(new WarriorConfig()
        //        {
        //            Level = i,
        //            Damage = i
        //        });
        //    }

        //    // config của warrior collector
        //    WarriorCollectorConfig = new WarriorCollectorConfig();

        //    // config end block
        //    ListEndBlockConfigs = new List<EndBlockConfig>();
        //    for (int i = 0; i < 8; i++)
        //    {
        //        ListEndBlockConfigs.Add(new EndBlockConfig()
        //        {
        //            Order = i,
        //            Hp = (i + 1) * 5,
        //            Coin = 5
        //        });
        //    }
        //    // config boosters
        //    ListBoosterConfigs = new List<BaseBoosterConfig>();

        //    BoosterCapacityConfig = new BoosterCapacityConfig();
        //    ListBoosterConfigs.Add(BoosterCapacityConfig);
        //    ListBoosterConfigs.Add(new BaseBoosterConfig(BoosterType.AddWarrior));
        //    ListBoosterConfigs.Add(new BaseBoosterConfig(BoosterType.WarriorUpgrade));
        //    ListBoosterConfigs.Add(new BaseBoosterConfig(BoosterType.Income));

        //}

    }
}
