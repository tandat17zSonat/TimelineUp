using DG.Tweening;
using SonatFramework.UI;
using TimelineUp.Data;
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

    private PlayerData _playerData;

    public override void OnSetup()
    {
        base.OnSetup();
        _playerData = DataManager.PlayerData;

        AddPlayButtonHandlers();

        foreach (var btn in BtnBoosters)
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
        foreach (var btn in BtnBoosters)
        {
            btn.UpdateVisual();
        }

        //-------------------
        var playerData = DataManager.PlayerData;

        _textTimeline.text = $"Timeline {playerData.TimelineId + 1}";
        _textEra.text = DataManager.TimelineEraSO.EraName;

        _textLevel.text = $"Level {playerData.Level}";
    }

    private void AddPlayButtonHandlers()
    {
        btnTapToPlay.onClick.AddListener(() =>
        {
            if (_playerData.Energy > 0)
            {
                _playerData.Energy -= 1;
                DataManager.SavePlayerData();
                GameplayManager.Instance.StartGame();
            }
            else
            {
                PanelManager.Instance.OpenPanel<PopupEnergyPurchase>();
            }
        });

        // effect
        var seq = DOTween.Sequence();
        seq.Append(btnTapToPlay.transform.DOScale(Vector3.one * 0.9f, 0.2f));
        seq.Append(btnTapToPlay.transform.DOScale(Vector3.one * 1.2f, 0.3f));
        seq.Append(btnTapToPlay.transform.DOScale(Vector3.one, 0.25f));
        seq.SetLoops(-1);
    }
}
