using System.Collections.Generic;

[System.Serializable]
public class BoosterCapacityConfig : BaseBoosterConfig
{
    public List<int> Exps; // kinh nghiệm nhận được khi qua level này

    //public BoosterCapacityConfig(): base(BoosterType.Capacity)
    //{
    //    Costs = new List<int>();
    //    Exps = new List<int>();
    //    for (int i = 0; i <= 31; i++)
    //    {
    //        Exps.Add(5);
    //        Costs.Add(i * 5);
    //    }
    //}
}
