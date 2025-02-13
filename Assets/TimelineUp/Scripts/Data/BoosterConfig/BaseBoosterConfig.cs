using System.Collections.Generic;
[System.Serializable]
public class BaseBoosterConfig
{
    public BoosterType Type;
    public List<int> Costs; // Giá để vượt qua level này

    public int GetMaxLevel()
    {
        return Costs.Count;
    }

    //public BaseBoosterConfig(BoosterType type)
    //{
    //    Type = type;
    //    Costs = new List<int>();
    //    if ( type == BoosterType.AddWarrior || type == BoosterType.Income)
    //    {
    //        for(int i =0;i <= 30; i++)
    //        {
    //            Costs.Add(50 * i);
    //        }
    //    }
    //    else if( type == BoosterType.WarriorUpgrade)
    //    {
    //        for (int i = 0; i <= 21; i++)
    //        {
    //            Costs.Add(5 * i);
    //        }
    //    }
    //}
}
