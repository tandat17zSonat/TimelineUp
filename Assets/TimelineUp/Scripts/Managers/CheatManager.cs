using Base.Singleton;
using UnityEngine;

public class CheatManager : Singleton<CheatManager>
{
    protected override void OnAwake()
    {
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            // reset data
            DataManager.ResetPlayerData();
        }
        //if (Input.GetKeyDown(KeyCode.C))
        //{
        //    DataManager.PlayerData.Coin += 100;
        //}
        //if (Input.GetKeyDown(KeyCode.E))
        //{
        //    DataManager.PlayerData.Energy += 100;
        //}
    }

    public void CheatResource()
    {
        DataManager.PlayerData.Coin += 100;
        DataManager.PlayerData.Energy += 10;
    }
}
