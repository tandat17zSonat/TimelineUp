using System;
using System.Collections.Generic;
using Base.Singleton;
using HyperCasualRunner;
using HyperCasualRunner.Locomotion;
using HyperCasualRunner.PopulationManagers;
using UnityEngine;

public class GameplayManager : Singleton<GameplayManager>
{
    [SerializeField] Player player;
    [SerializeField] ObstacleManager obstacleManager;

    private RunnerMover _runnerMover;
    private CrowdManager _crowdManager;
    public PopulationManagerBase PopulationManager { get { return _crowdManager; } }

    private int _startingEntityCount;

    public GameState State { get; private set; }

    public Action OnRestart;

    // Data trong màn chơi ----------------------------------------------
    public int CollectorLevel { get; set; }
    public float ExpCollectorInGame { get; set; }
    public Dictionary<int, int> DictWarriorSpawned {  get; set; }

    protected override void OnAwake()
    {
        _runnerMover = player.GetComponent<RunnerMover>();
        _crowdManager = player.GetComponent<CrowdManager>();

        _startingEntityCount = 1;

        State = GameState.Pause;
        OnRestart += LoadGame;

        CollectorLevel = 0;
        ExpCollectorInGame = 0;
        DictWarriorSpawned = new Dictionary<int, int>();
    }

    private void Start()
    {
        Restart();
    }

    public void LoadGame()
    {
        ObstacleData data = new ObstacleData();
        obstacleManager.LoadObstacle(data);

        var beginPlayerData = GameManager.Instance.PlayerData;
        int num = beginPlayerData.warriorNumber,
            level = beginPlayerData.warriorLevel;
        _crowdManager.AddPopulation(level, num);

        // ResetData
        CollectorLevel = 0;
        ExpCollectorInGame = GameManager.Instance.PlayerData.ExpCollector;
        DictWarriorSpawned.Clear();
    }

    public void Restart()
    {
        OnRestart?.Invoke();
    }

    public void StartGame()
    {
        State = GameState.Playing;

    }

    public void Unload()
    {
        player.Unload();
        obstacleManager.Unload();
        _crowdManager.Unload();
    }

    public void SetResult(GameState state)
    {
        State = state;
        Unload();
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
