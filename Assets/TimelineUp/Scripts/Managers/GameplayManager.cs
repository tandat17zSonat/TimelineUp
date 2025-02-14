using System;
using System.Collections.Generic;
using Base.Singleton;
using HyperCasualRunner;
using HyperCasualRunner.Locomotion;
using SonatFramework.UI;
using TimelineUp.Data;
using TimelineUp.Obstacle;
using UnityEngine;

public class GameplayManager : Singleton<GameplayManager>
{
    [SerializeField] Player player;
    [SerializeField] ObstacleManager obstacleManager;

    public Player Player { get { return player; } }

    private RunnerMover _runnerMover;
    private PopulationManager _populationManager;

    private TransformationModifier _transformationModifier;
    private AnimationModifier _animationModifier;
    private ProjectileShooterModifier _projectileShooterModifier;

    public RunnerMover RunnerMover {  get { return _runnerMover; } }
    public PopulationManager PopulationManager { get { return _populationManager; } }
    public ObstacleManager ObstacleManager {  get { return obstacleManager; } }

    public GameState State { get; private set; }

    public Action OnRestart;

    // Data trong màn chơi ----------------------------------------------
    public TimelineEraSO TimelineEraSO { get; private set; }
    public float Speed { get; set; }
    public int NumberInCollector { get; set; }
    public float ExpCollectorInGame { get; set; }
    public Dictionary<int, int> DictWarriorSpawned { get; set; }

    public float ProjectileSpeed { get { return Speed * 4; } }
    public float ProjectileRate { get; set; }
    public float ProjectileRange { get; set; }


    protected override void OnAwake()
    {
        _runnerMover = player.GetComponent<RunnerMover>();
        _populationManager = player.GetComponent<PopulationManager>();

        _transformationModifier = player.GetComponent<TransformationModifier>();
        _animationModifier = player.GetComponent<AnimationModifier>();
        _projectileShooterModifier = player.GetComponent<ProjectileShooterModifier>();

        State = GameState.Pause;
    }

    public void LoadGame()
    {
        State = GameState.Pause;

        // Load playerData
        var playerData = DataManager.PlayerData;
        Speed = playerData.Speed;
        ExpCollectorInGame = playerData.ExpCollector;
        ProjectileRate = playerData.ProjectileRate;
        ProjectileRange = playerData.ProjectileRange;

        NumberInCollector = 1;
        DictWarriorSpawned = new Dictionary<int, int>();

        // Load các obstacle
        MapData data = DataManager.LoadMapData();
        obstacleManager.LoadObstacle(data);

        // Khởi tạo các warrior đầu tiên
        int levelOfWarriors = playerData.LevelOfWarriors;
        for(int _ = 0; _ < playerData.NumberOfWarriors; _++)
        {
            AddEntity(levelOfWarriors);
        }

        //_animationModifier.CurrentAnimationName = _animationModifier.IdleAnimationName;
        //_animationModifier.ApplyAll(); // reset animation -> Idle
    }

    public void Restart()
    {
        PanelManager.Instance.OpenPanel<PanelLobby>();
        Unload();
        LoadGame();
        OnRestart?.Invoke();
    }

    public void StartGame()
    {
        PanelManager.Instance.ClosePanel<PanelLobby>();
        State = GameState.Playing;

        _animationModifier.CurrentAnimationName = _animationModifier.RunAnimationName;
        _animationModifier.ApplyAll(); // start animation -> run

        _projectileShooterModifier.ApplyAll();
    }

    public void Unload()
    {
        player.Unload();
        obstacleManager.Unload();
        _populationManager.Unload();
    }

    public void SetResult(GameState state)
    {
        State = state;
        if( state == GameState.Win)
        {
            // Lên hạng - complete
            DataManager.NextEra();

            DataManager.PlayerData.Coin += 100;
            DataManager.SavePlayerData();

            PanelManager.Instance.OpenPanel<PopupWin>();
        }
        else
        {
            PanelManager.Instance.OpenPanel<PopupLoss>();
        }
    }

    public void AddEntity(int level)
    {
        var entity = _populationManager.Spawn(level);
        _populationManager.AddToCrowd(entity);
    }
}

public enum GameState
{
    Playing,
    Pause,
    Win,
    Loss
}
