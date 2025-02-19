using System;
using System.Collections;
using System.Collections.Generic;
using SonatFramework.UI;
using TimelineUp.Obstacle;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UITools : Panel
{
    [System.Serializable]
    public class ButtonObstacle
    {
        public Button Button;
        public ObstacleType Type;
    }

    [Header("Spawn obstacle")]
    [SerializeField] ButtonObstacle[] spawnBtns;
    [SerializeField] Button btnDelete;

    [Header("Move camera")]
    [SerializeField] Button btnUp;
    [SerializeField] Button btnDown;


    [Header("Settings")]
    [SerializeField] TMP_InputField inputX;
    [SerializeField] TMP_InputField inputZ;
    [SerializeField] TMP_InputField inputProperty;
    [SerializeField] TMP_InputField inputLock;

    [SerializeField] Button enterPosition;
    [SerializeField] Button enterProperty;
    [SerializeField] Button enterLock;

    [SerializeField] Button enterMove;

    [Header("File")]
    [SerializeField] TMP_InputField inputId;
    [SerializeField] TMP_InputField inputPath;

    [SerializeField] Button btnSave;
    [SerializeField] Button btnLoad;
    [SerializeField] Button btnClear;

    [Header("Log")]
    [SerializeField] TMP_Text textLog;

    public override void OnSetup()
    {
        base.OnSetup();

        HandleButtonsSpawn();
        HandleButtonsMoveCamera();
        HandleSettings();
        HandleFile();
    }

    public override void Open(UIData uiData)
    {
        base.Open(uiData);
    }

    public override void OnOpenCompleted()
    {
        base.OnOpenCompleted();
    }

    public override void Close()
    {
        base.Close();
    }

    protected override void OnCloseCompleted()
    {
        base.OnCloseCompleted();
    }

    private void HandleButtonsSpawn()
    {
        foreach (var btnObs in spawnBtns)
        {
            var type = btnObs.Type;
            btnObs.Button.onClick.AddListener(() =>
            {
                ToolManager.Instance.Create(type);
                SetLog($"Spawn {type}");
            });
        }

        btnDelete.onClick.AddListener(() =>
        {
            ToolManager.Instance.Delete();
        });
    }

    private void HandleSettings()
    {
        enterPosition.onClick.AddListener(() =>
        {
            float x = 0;
            float.TryParse(inputX.text, out x);

            float z = 0;
            float.TryParse(inputZ.text, out z);

            if (ToolManager.Instance.Obstacle != null)
            {
                SetLog($"Update pos {x} {z}");
                ToolManager.Instance.SetPosition(x, z);
            }
            else
            {
                SetLog($"Chưa chọn được Obstacle");
            }
        });

        enterProperty.onClick.AddListener(() =>
        {
            if (int.TryParse(inputProperty.text, out int numProperty))
            {
                if (ToolManager.Instance.Obstacle != null)
                {
                    ToolManager.Instance.SetProperty(numProperty);
                }
                else
                {
                    SetLog($"Chưa chọn được Obstacle");
                }
            }
        });

        enterLock.onClick.AddListener(() =>
        {
            if (int.TryParse(inputLock.text, out int numLock))
            {
                if (ToolManager.Instance.Obstacle != null)
                {
                    ToolManager.Instance.SetLock(numLock);
                }
                else
                {
                    SetLog($"Chưa chọn được Obstacle");
                }
            }
        });

        enterMove.onClick.AddListener(() =>
        {
            if (ToolManager.Instance.Obstacle != null)
            {
                ToolManager.Instance.SetMove();
            }
            else
            {
                SetLog($"Chưa chọn được Obstacle");
            }
        });
    }

    private void HandleButtonsMoveCamera()
    {
        btnUp.onClick.AddListener(() =>
        {
            ToolManager.Instance.MoveUp();
        });
        btnDown.onClick.AddListener(() =>
        {
            ToolManager.Instance.MoveDown();
        });
    }

    private void HandleFile()
    {
        btnSave.onClick.AddListener(() =>
        {
            int level = 0;
            int.TryParse(inputId.text, out level);
            ToolManager.Instance.SaveMapData(inputPath.text, level);
        });

        btnLoad.onClick.AddListener(() =>
        {
            int level = 0;
            int.TryParse(inputId.text, out level);
            ToolManager.Instance.LoadMapData(inputPath.text, level);
        });


        btnClear.onClick.AddListener(() =>
        {
            ToolManager.Instance.Clear();
            SetLog("Clear");
        });
    }

    public void SetLog(string v)
    {
        Debug.Log(v);
        textLog.text = v;
    }

}
