using SonatFramework.UI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIInGame : Panel
{
    [Header("Exp Bar")]
    [SerializeField] Slider sliderExp;
    [SerializeField] TMP_Text textNum;
    [SerializeField] TMP_Text textNextNum;

    [Header("Resource")]
    [SerializeField] TMP_Text textCoin;
    [SerializeField] TMP_Text textDiamond;
    [SerializeField] TMP_Text textEnergy;

    public override void OnSetup()
    {
        base.OnSetup();
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

    public void SetUIExpBar(int currentLevel, float percent)
    {
        textNum.text = currentLevel.ToString();
        textNextNum.text = (currentLevel + 1).ToString();

        sliderExp.value = percent;
    }

    private void Update()
    {
        UpdateExpSlider();
        UpdateResource();
    }

    private void UpdateExpSlider()
    {
        var collectorLevel = GameplayManager.Instance.NumberInCollector;
        var exp = GameplayManager.Instance.ExpCollectorInGame;

        var gameConfigData = DataManager.GameplayConfig;

        if (collectorLevel == gameConfigData.WarriorCollectorConfig.GetMaxWarriorNumber())
        {
            SetUIExpBar(collectorLevel, 0);
        }
        else
        {
            var expToUpgrade = gameConfigData.GetExpToUpgradeWarriorNumber(collectorLevel + 1);

            exp = Mathf.Min(exp, expToUpgrade);
            SetUIExpBar(collectorLevel, (float)exp / expToUpgrade);
        }
    }

    private void UpdateResource()
    {
        textEnergy.text = DataManager.PlayerData.Energy.ToString();
        textCoin.text = DataManager.PlayerData.Coin.ToString();
        textDiamond.text = DataManager.PlayerData.Diamond.ToString();
    }
}
