using UnityEngine;

public class CheatManager : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            // reset data
            DataManager.ResetPlayerData();
        }
        if ( Input.GetKeyDown(KeyCode.C))
        {
            DataManager.PlayerData.Coin += 10;
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            DataManager.PlayerData.Energy += 10;
        }
    }
}
