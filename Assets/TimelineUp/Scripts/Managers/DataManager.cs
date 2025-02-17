using TimelineUp.Data;
using TimelineUp.Obstacle;
using UnityEngine;

public static class DataManager
{
    public const int MAX_TIMELINE_NUMBER = 2;
    public const int MAX_ERA_NUMBER = 5;

    public static PlayerData PlayerData;

    public static GameplayConfig GameplayConfig;
    public static TimelineEraSO TimelineEraSO;

    public const int MAX_MAP_NUMBER = 5;
    public static MapData MapData;

    // -----------------------------
    public static void LoadPlayerData()
    {
        //if (PlayerPrefs.HasKey("PLAYER_DATA"))
        //{
        //    string json = PlayerPrefs.GetString("PLAYER_DATA");
        //    PlayerData = JsonUtility.FromJson<PlayerData>(json);
        //}
        //else
        //{
        PlayerData = new PlayerData();
        //}
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
        TextAsset jsonFile = Resources.Load<TextAsset>($"TimelineUpConfig/Timeline/Config_{timelineId}_{eraId}");
        GameplayConfig = JsonUtility.FromJson<GameplayConfig>(jsonFile.text);
    }

    private static void LoadSO(int timelineId, int eraId)
    {
        TimelineEraSO = SOManager.GetSO<TimelineEraSO>(timelineId, eraId);

    }

    public static void ResetPlayerData()
    {
        PlayerData = new PlayerData();
        SavePlayerData();
    }

    public static void NextEra()
    {
        if (PlayerData.CheckNextEra())
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

    //----------------------------------------
    public static MapData LoadMapData()
    {
        int id = Random.Range(0, MAX_MAP_NUMBER);
        id = 0;
        TextAsset jsonFile = Resources.Load<TextAsset>($"TimelineUpConfig/Map/{id}");
        MapData = JsonUtility.FromJson<MapData>(jsonFile.text);
        return MapData;
    }
}
