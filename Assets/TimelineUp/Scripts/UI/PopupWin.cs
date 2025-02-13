using SonatFramework.UI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PopupWin : Panel
{
    [Header("")]
    [SerializeField] Button _btnReceive;

    public override void OnSetup()
    {
        base.OnSetup();

        _btnReceive.onClick.AddListener(() =>
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
