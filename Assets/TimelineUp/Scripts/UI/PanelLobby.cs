using DG.Tweening;
using SonatFramework.UI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PanelLobby : Panel
{
    [SerializeField] Button btnTapToPlay;

    [SerializeField] BaseButtonBooster[] BtnBoosters;

    public override void OnSetup()
    {
        base.OnSetup();

        btnTapToPlay.onClick.AddListener(() =>
        {
            Debug.Log($"Click tap to play");
            GameplayManager.Instance.StartGame();
            btnTapToPlay.gameObject.SetActive(false);
        });

        foreach(var btn in BtnBoosters)
        {
            btn.Initialize();

        }
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

    private void Update()
    {
        foreach(var btn in BtnBoosters)
        {
            btn.UpdateVisual();
        }
    }
}
