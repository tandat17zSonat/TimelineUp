using TimelineUp.Data;
using UnityEngine;

public static class DataManager
{
    public const int MAX_TIMELINE = 2;
    public const int MAX_ERA = 5;

    public static PlayerData PlayerData;

    public static GameplayConfig GameplayConfig;
    public static TimelineEraSO TimelineEraSO;
    
    // -----------------------------
    public static void LoadPlayerData()
    {
        if (PlayerPrefs.HasKey("PLAYER_DATA"))
        {
            string json = PlayerPrefs.GetString("PLAYER_DATA");
            PlayerData = JsonUtility.FromJson<PlayerData>(json);
        }
        else
        {
            PlayerData= new PlayerData();
        }
    }

    public static void SavePlayerData()
    {
        string json = JsonUtility.ToJson(PlayerData);
        PlayerPrefs.SetString("PLAYER_DATA", json);
        PlayerPrefs.Save();
        Debug.Log($"Save playeData");
    }


    // ---------------------------------------------
    public static void LoadGameData()
    {
        int timelineId = PlayerData.TimelineId,
            eraId = PlayerData.EraId;

        LoadConfig(timelineId, eraId);
        LoadSO(timelineId, eraId);
    }

    private static void LoadConfig(int timelineId, int eraId)
    {
        TextAsset jsonFile = Resources.Load<TextAsset>($"TimelineUpConfig/Config_{timelineId}_{eraId}");
        GameplayConfig = JsonUtility.FromJson<GameplayConfig>(jsonFile.text);
    }

    private static void LoadSO(int timelineId, int eraId)
    {
        TimelineEraSO = SOManager.GetSO<TimelineEraSO>(timelineId, eraId);

    }

    public static void ResetPlayerData()
    {
        PlayerData = new PlayerData();
    }

    public static void NextEra()
    {
        if(PlayerData.CheckNextEra())
        {
            PlayerData.NextEra();
            SavePlayerData();

            Resources.UnloadUnusedAssets();
            LoadGameData();
        }
        else
        {
            Debug.LogWarning($"TimelineUp limit level");
        }
        
    }
}
