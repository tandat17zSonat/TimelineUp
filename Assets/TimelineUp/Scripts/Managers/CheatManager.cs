using UnityEngine;

public class CheatManager : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            // reset data
            GameManager.Instance.ResetPlayerData();
        }
        if ( Input.GetKeyDown(KeyCode.C))
        {
            GameManager.Instance.PlayerData.Coin += 10;
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            GameManager.Instance.PlayerData.Energy += 10;
        }
    }
}
