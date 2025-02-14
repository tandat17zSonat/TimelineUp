using DG.Tweening;
using SonatFramework.UI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PanelLobby : Panel
{
    [SerializeField] Button btnTapToPlay;

    [SerializeField] BaseButtonBooster[] BtnBoosters;

    [SerializeField] TMP_Text _textLevel;
    [SerializeField] TMP_Text _textTimeline;
    [SerializeField] Button _btnEra;
    [SerializeField] TMP_Text _textEra;

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

        // ButtonEra
        _btnEra.onClick.AddListener(() =>
        {
            Debug.Log($"Show era");
        });
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

        //-------------------
        var playerData = DataManager.PlayerData;

        _textTimeline.text = $"Timeline {playerData.TimelineId + 1}";
        _textEra.text = DataManager.TimelineEraSO.EraName;

        _textLevel.text = $"Level {playerData.Level}";
    }
}
