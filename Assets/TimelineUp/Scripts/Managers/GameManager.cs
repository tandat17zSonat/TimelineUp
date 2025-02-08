using Base.Singleton;
using SonatFramework.UI;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Device;


public class GameManager : Singleton<GameManager>
{
    public static GameMode gameMode = GameMode.classic;
    public static GameScene gameScene = GameScene.loading;

    public static Action<GameScene> OnSwitchScene;

    private PlayerData playerData;
    public PlayerData PlayerData { get { return playerData; } }

    private GameConfigData gameConfigData;
    public GameConfigData GameConfigData { get { return gameConfigData; } }


    protected override void OnAwake()
    {
        playerData = new PlayerData();
        LoadGameConfig();
    }

    private void Start()
    {
        PanelManager.Instance.OpenPanel<PanelLobby>();
        StartCoroutine(IeOnGameInitComplete());
    }

    private IEnumerator IeOnGameInitComplete()
    {
        if (PlayerPrefs.HasKey("PLAYER_DATA"))
        {
            LoadData();
        }
        else
        {
            SaveData();
        }

        yield return new WaitForEndOfFrame();

        //Vibration.Init();

        //BoosterManager.isLockingAll = false;

        //UserDataManager.Instance.Init();
        //LevelManager.CountLevel();
        //TutorialManager.SetupOnStart();

        yield return new WaitForEndOfFrame();

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
        }
        else if (gameScene == GameScene.home)
        {

        }

        OnSwitchScene?.Invoke(scene);
    }

    public void SaveData()
    {
        string json = JsonUtility.ToJson(playerData);
        PlayerPrefs.SetString("PlayerData", json);
        PlayerPrefs.Save();
        Debug.Log($"Save playeData");
    }

    public void LoadData()
    {
        string json = PlayerPrefs.GetString("PlayerData");
        PlayerData data = JsonUtility.FromJson<PlayerData>(json);
        Debug.Log($"Load data: level - {data.Level}    coin - {data.Coin}");
    }

    private void LoadGameConfig()
    {
        gameConfigData = new GameConfigData();
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
