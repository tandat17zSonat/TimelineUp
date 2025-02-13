using Base.Singleton;
using SonatFramework.UI;
using System;
using System.Collections;
using TimelineUp.Data;
using UnityEngine;


public class GameManager : Singleton<GameManager>
{
    public static GameMode gameMode = GameMode.classic;
    public static GameScene gameScene = GameScene.loading;

    public static Action<GameScene> OnSwitchScene;

    // Data của user trong game -----------------------------------------------------
    private PlayerData playerData;
    public PlayerData PlayerData { get { return playerData; } }

    // Các config của game ----------------------------------------------------------
    private GameplayConfig gameConfigData;
    public GameplayConfig GameConfigData { get { return gameConfigData; } }
    public TimelineEraSO TimelineEraSO { get; set; }

    protected override void OnAwake()
    {
        playerData = new PlayerData();
        LoadGameConfig();
    }

    private void Start()
    {
        StartCoroutine(IeOnGameInitComplete());
    }

    private IEnumerator IeOnGameInitComplete()
    {
        // Load data -------------------------------------------
        if (PlayerPrefs.HasKey("PLAYER_DATA"))
        {
            LoadPlayerData();
        }
        else
        {
            SavePlayerData();
        }
        // Load Scriptable Object tương ứng
        TimelineEraSO = SOManager.GetSO<TimelineEraSO>(playerData.TimelineId, playerData.EraId);

        yield return new WaitForEndOfFrame();

        // Show ui
        PanelManager.Instance.OpenPanel<UIInGame>();
        PanelManager.Instance.OpenPanel<PanelLobby>();


        yield return new WaitForEndOfFrame();

        // Load game
        SwitchScene(GameScene.ingame);
    }

    public static void SwitchScene(GameScene scene)
    {
        if (scene == gameScene) return;

        gameScene = scene;

        if (gameScene == GameScene.ingame)
        {
            //PanelManager.Instance.OpenForget<GameplayPanel>();
            //GameplayManager.Instance.StartLevel(UserDataManager.Instance.GetCurrentLevel());
            //GameplayManager.Instance.StartLevel(1);

            //Kernel.Resolve<AdsManager>().ShowBanner();
            GameplayManager.Instance.LoadGame();
        }
        else if (gameScene == GameScene.home)
        {

        }

        OnSwitchScene?.Invoke(scene);
    }

    public void SavePlayerData()
    {
        string json = JsonUtility.ToJson(playerData);
        PlayerPrefs.SetString("PLAYER_DATA", json);
        PlayerPrefs.Save();
        Debug.Log($"Save playeData");
    }

    public void LoadPlayerData()
    {
        string json = PlayerPrefs.GetString("PLAYER_DATA");
        playerData = JsonUtility.FromJson<PlayerData>(json);
        Debug.Log($"Load data: level - {playerData.Level}    coin - {playerData.Coin}");
    }

    private void LoadGameConfig()
    {
        TextAsset jsonFile = Resources.Load<TextAsset>("config");
        gameConfigData = JsonUtility.FromJson<GameplayConfig>(jsonFile.text);
    }

    public void ResetPlayerData()
    {
        playerData = new PlayerData();
        SavePlayerData();
    }
}

public enum GameMode
{
    classic = 0,
}

public enum GameScene
{
    loading = 0,
    home = 1,
    ingame = 2
}
