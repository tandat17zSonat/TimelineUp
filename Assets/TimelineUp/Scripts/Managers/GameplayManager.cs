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

    protected override void OnAwake()
    {
        _runnerMover = player.GetComponent<RunnerMover>();
        _crowdManager = player.GetComponent<CrowdManager>();

        _startingEntityCount = 5;
    }

    public void LoadGame()
    {
        obstacleManager.LoadObstacle();
        _crowdManager.AddPopulation(_startingEntityCount);
    }

    public void Unload()
    {
        obstacleManager.Unload();
        _crowdManager.Unload();
    }


}

public enum GameState
{
    Playing,
    Pause
}
