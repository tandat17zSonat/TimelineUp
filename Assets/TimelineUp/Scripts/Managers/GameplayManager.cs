using System;
using System.Collections.Generic;
using Base.Singleton;
using HyperCasualRunner;
using HyperCasualRunner.Locomotion;
using SonatFramework.UI;
using UnityEngine;

public class GameplayManager : Singleton<GameplayManager>
{
    [SerializeField] Player player;
    [SerializeField] ObstacleManager obstacleManager;

    private RunnerMover _runnerMover;
    private PopulationManager _populationManager;
    private AnimationModifier _animationModifier;

    public PopulationManager PopulationManager { get { return _populationManager; } }
    public GameState State { get; private set; }

    public Action OnRestart;

    // Data trong màn chơi ----------------------------------------------
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
        _animationModifier = player.GetComponent<AnimationModifier>();

        State = GameState.Pause;
    }

    public void LoadGame()
    {
        State = GameState.Pause;

        // Load playerData
        var playerData = GameManager.Instance.PlayerData;
        Speed = playerData.Speed;
        ExpCollectorInGame = playerData.ExpCollector;
        ProjectileRate = playerData.ProjectileRate;
        ProjectileRange = playerData.ProjectileRange;

        NumberInCollector = 1;
        DictWarriorSpawned = new Dictionary<int, int>();

        // Load các obstacle
        ObstacleData data = new ObstacleData();
        obstacleManager.LoadObstacle(data);

        // Khởi tạo các warrior đầu tiên
        int levelOfWarriors = playerData.LevelOfWarriors;
        for(int _ = 0; _ < playerData.NumberOfWarriors; _++)
        {
            var entity = _populationManager.Spawn(levelOfWarriors);
            _populationManager.AddToCrowd(entity);
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
        Restart();
    }
}

public enum GameState
{
    Playing,
    Pause,
    Win,
    Loss
}
