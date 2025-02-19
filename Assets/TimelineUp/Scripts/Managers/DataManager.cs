using System;
using System.IO;
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

    public const int MAX_MAP_NUMBER = 7;
    public static MapData MapData;

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
            PlayerData = new PlayerData();
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
        int id = UnityEngine.Random.Range(0, MAX_MAP_NUMBER);
        //id = 6;
        Debug.LogWarning($"Load map {id}");
        TextAsset jsonFile = Resources.Load<TextAsset>($"TimelineUpConfig/Map/{id}");
        MapData = JsonUtility.FromJson<MapData>(jsonFile.text);
        return MapData;
    }

    public static MapData CreateMapData()
    {
        MapData = new MapData();
        return MapData;
    }

    public static void SaveMapData(string path, int level)
    {

        if (Directory.Exists(path) == false)
        {
            Directory.CreateDirectory(path);
        }

        MapData.Sort();
        string json = JsonUtility.ToJson(MapData, true);
        File.WriteAllText(path + $"/{level}.json", json);
    }

    public static void UpdateMapData(BaseObstacle obstacle)
    {
        foreach (var obs in MapData.ListMainObstacles)
        {
            if (obs.Id == obstacle.Id)
            {
                obs.x = (float)Math.Round(obstacle.transform.position.x, 1);
                obs.z = (float)Math.Round(obstacle.transform.position.z, 1);
                obs.Locked = obstacle.CheckLocked();
                obs.Move = obstacle.CheckMove();
                if (obs.Move) obs.x = 0;
                // setProperty
                obs.Properties = obstacle.GetProperties();
            }
        }
    }

    public static void DeleteMapData(BaseObstacle obstacle)
    {
        MapData.ListMainObstacles.RemoveAll(obs => { return obs.Id == obstacle.Id; });
    }
}
