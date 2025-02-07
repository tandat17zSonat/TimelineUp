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

    protected override void OnAwake()
    {

    }

    private void Start()
    {
        StartCoroutine(IeOnGameInitComplete());
    }

    private IEnumerator IeOnGameInitComplete()
    {
        if (!PlayerPrefs.HasKey("GAME_INITED"))
        {
            Debug.LogWarning($"New player");
            PlayerPrefs.SetInt("GAME_INITED", 1);
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
