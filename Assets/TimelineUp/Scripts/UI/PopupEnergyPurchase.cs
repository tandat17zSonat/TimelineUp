using SonatFramework.UI;
using TimelineUp.Data;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PopupEnergyPurchase : Panel
{
    [System.Serializable]
    class ButtonInfo
    {
        public Button Button;
        public int NumberOfEnergy;
    }

    [Header("")]
    [SerializeField] ButtonInfo[] btnPurchases;
    [SerializeField] Button btnClose;

    private PlayerData _playerData;

    public override void OnSetup()
    {
        base.OnSetup();

        _playerData = DataManager.PlayerData;

        foreach (var item in btnPurchases)
        {
            var num = item.NumberOfEnergy;
            item.Button.onClick.AddListener(() =>
            {
                BuyEnergy(num);
            });
        }

        btnClose.onClick.AddListener(() =>
        {
            Close();
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

    private void BuyEnergy(int num)
    {
        _playerData.Energy += num;
        DataManager.SavePlayerData();
        Debug.LogWarning($"Buy Energy: {num}");
        Close();
    }
}
