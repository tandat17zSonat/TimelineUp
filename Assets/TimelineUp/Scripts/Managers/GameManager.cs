using Base.Singleton;
using SonatFramework.UI;
using System;
using System.Collections;
using UnityEngine;


public class GameManager : Singleton<GameManager>
{
    public static GameMode gameMode = GameMode.classic;
    public static GameScene gameScene = GameScene.loading;

    public static Action<GameScene> OnSwitchScene;

    protected override void OnAwake()
    {
        //throw new NotImplementedException();
    }

    private void Start()
    {
        Application.targetFrameRate = 60;
        StartCoroutine(IeOnGameInitComplete());
    }

    private IEnumerator IeOnGameInitComplete()
    {
        // Load data -------------------------------------------
        DataManager.LoadPlayerData();
        DataManager.LoadGameData();

        yield return new WaitForEndOfFrame();

        // Show ui----------------------------------------
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
