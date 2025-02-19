using Base.Singleton;
using SonatFramework.UI;
using TimelineUp.Obstacle;
using UnityEngine;

public class ToolManager : Singleton<ToolManager>
{
    [SerializeField] Transform player;
    [SerializeField] ObstacleManager obstacleManager;

    private BaseObstacle _obstacle;

    public BaseObstacle Obstacle { get { return _obstacle; } }

    protected override void OnAwake()
    {
        DataManager.LoadPlayerData();
        DataManager.LoadGameData();

    }

    private void Start()
    {
        PanelManager.Instance.OpenPanel<UITools>();
        Clear();
    }

    public void Clear()
    {
        DataManager.CreateMapData();
        obstacleManager.Unload();
    }

    private void Update()
    {
        OnClick();
    }

    private void OnClick()
    {

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (hit.collider.TryGetComponent<BaseObstacle>(out BaseObstacle obs))
                {
                    if (_obstacle != null) _obstacle.SetSelected(false);

                    if (obs.CheckSelected())
                    {
                        obs.SetSelected(false);
                        _obstacle = null;
                    }
                    else
                    {
                        obs.SetSelected(true);
                        _obstacle = obs;
                    }

                    var uiTools = PanelManager.Instance.GetPanel<UITools>();
                    if (uiTools != null)
                    {
                        uiTools.SetLog($"{obs.Type} --> pos: {obs.transform.position}");
                    }
                }

            }
        }
    }
    // di chuyển camera
    public void MoveUp()
    {
        var pos = player.position;
        pos += new Vector3(0, 0, 1) * 5;
        player.position = pos;

        var uiTools = PanelManager.Instance.GetPanel<UITools>();
        uiTools.SetLog($"Move {pos}");
    }

    public void MoveDown()
    {
        var pos = player.position;
        pos -= new Vector3(0, 0, 1) * 5;
        player.position = pos;

        var uiTools = PanelManager.Instance.GetPanel<UITools>();
        uiTools.SetLog($"Move {pos}");
    }

    // Spawn
    public void Create(ObstacleType type)
    {
        if (_obstacle != null) _obstacle.SetSelected(false);

        _obstacle = obstacleManager.Spawn(type);
        _obstacle.transform.position = Vector3.zero;
        obstacleManager.AddList(_obstacle);
        var id = DataManager.MapData.Create(type, 0, 0);
        _obstacle.Id = id;
    }

    // Settings
    public void SetPosition(float x, float z)
    {
        if (x < -3 || x > 3) x = _obstacle.transform.position.x;
        if (z < 0) z = _obstacle.transform.position.z;

        _obstacle.transform.position = new Vector3(x, 0, z);

        DataManager.UpdateMapData(_obstacle);
    }

    public void SetProperty(int numProperty)
    {
        _obstacle.SetProperty(numProperty);
        DataManager.UpdateMapData(_obstacle);
    }

    public void SetLock(int numLock)
    {
        _obstacle.SetLock(numLock);

        DataManager.UpdateMapData(_obstacle);
    }

    public void SaveMapData(string text, int level)
    {
        string path = Application.dataPath + $"/Level";
        if (text != null && text != "") path = text;

        DataManager.SaveMapData(path, level);

        var uiTools = PanelManager.Instance.GetPanel<UITools>();
        uiTools.SetLog($"Save Map {path} {level}");

    }

    public void LoadMapData(string text, int level)
    {
        string path = Application.dataPath + $"/Level/{level}.json";
        if (text != null && text != "") path = text + $"/{level}.json";

        var uiTools = PanelManager.Instance.GetPanel<UITools>();
        uiTools.SetLog($"Load map: {path}");
        var mapData = DataManager.LoadMapData(path);

        Clear();
        foreach (var item in mapData.ListMainObstacles)
        {
            Create(item.Type);
            SetPosition(item.x, item.z);
            if (item.Locked == true)
            {
                SetLock(item.Properties[item.Properties.Count - 1]);
            }
            SetMove(item.Move ? 1 : 0);

            if (item.Properties.Count > 0) SetProperty(item.Properties[0]);
        }
    }

    public void Delete()
    {
        var uiTools = PanelManager.Instance.GetPanel<UITools>();
        uiTools.SetLog($"Delete {_obstacle.Type}");

        DataManager.DeleteMapData(_obstacle);
        obstacleManager.Remove(_obstacle);
        _obstacle = null;

    }

    public void SetMove(int move = -1)
    {
        // load
        if (move != -1)
        {
            if (move == 0)
            {
                _obstacle.ResetRun();
            }
            else
            {
                _obstacle.SetRun();
            }
            DataManager.UpdateMapData(_obstacle);
            return;
        }

        var uiTools = PanelManager.Instance.GetPanel<UITools>();
        if (uiTools) uiTools.SetLog($"{_obstacle.Type} setMove Ok");
        // settings in tools
        if (_obstacle.CheckMove() == false)
        {
            _obstacle.SetRun();
        }
        else
        {
            _obstacle.ResetRun();
        }

        DataManager.UpdateMapData(_obstacle);
    }
}
