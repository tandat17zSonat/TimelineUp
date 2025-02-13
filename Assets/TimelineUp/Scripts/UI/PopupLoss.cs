using SonatFramework.UI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PopupLoss : Panel
{
    [Header("")]
    [SerializeField] Button _btnBack;

    public override void OnSetup()
    {
        base.OnSetup();

        _btnBack.onClick.AddListener(() =>
        {
            GameplayManager.Instance.Restart();
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
}
