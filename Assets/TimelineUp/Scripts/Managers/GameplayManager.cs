using System;
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

    private int _startingEntityCount;

    public GameState State { get; private set; }

    public Action OnRestart;

    protected override void OnAwake()
    {
        _runnerMover = player.GetComponent<RunnerMover>();
        _crowdManager = player.GetComponent<CrowdManager>();

        _startingEntityCount = 5;

        State = GameState.Pause;
        OnRestart += LoadGame;
    }

    private void Start()
    {
        ReStart();
    }

    public void LoadGame()
    {
        obstacleManager.LoadObstacle();
        _crowdManager.AddPopulation(_startingEntityCount);
    }

    public void ReStart()
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
        ReStart();
    }
}

public enum GameState
{
    Playing,
    Pause,
    Win,
    Loss
}
