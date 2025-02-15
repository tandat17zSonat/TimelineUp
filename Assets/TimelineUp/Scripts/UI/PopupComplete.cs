using SonatFramework.UI;
using TimelineUp.Data;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PopupComplete : Panel
{
    [Header("")]
    [SerializeField] Button _btnReceive;
    [SerializeField] TMP_Text _textCoin;

    private PlayerData _playerData;
    private int _coin;
    private int _coinReward;

    public override void OnSetup()
    {
        base.OnSetup();

        _playerData = DataManager.PlayerData;
        _btnReceive.onClick.AddListener(() =>
        {
            _playerData.Coin += _coinReward;
            DataManager.SavePlayerData();
            GameplayManager.Instance.Restart();
            Close();
        });
    }

    public override void Open(UIData uiData)
    {
        base.Open(uiData);

        _coin = (int)uiData.Get("COMPLETE_COIN");
        _coinReward = _coin;
        _textCoin.text = _coin.ToString();
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
}
