using TMPro;
using UnityEngine;
using UnityEngine.UI;

public abstract class BaseButtonBooster : MonoBehaviour
{
    [SerializeField] protected BoosterType type;
    [SerializeField] protected Button _btn;
    [SerializeField] TMP_Text _textLevel;
    [SerializeField] TMP_Text _textCost;

    protected int id;
    protected int level;
    protected int cost;

    protected PlayerData playerData;

    public void Initialize()
    {

        this.id = ((int)type);
        playerData = GameManager.Instance.PlayerData;

        _btn.onClick.AddListener(() =>
        {
            playerData.Coin -= cost;
            HandleBooster();

            GameManager.Instance.SavePlayerData();
        });

    }
    public abstract void HandleBooster();

    public virtual void UpdateVisual()
    {
        var gameConfigData = GameManager.Instance.GameConfigData;

        level = playerData.BoosterLevel[id];

        var boosterConfig = gameConfigData.GetBoosterConfig(type);
        cost = boosterConfig.Costs[level];

        _textLevel.text = $"Level {level.ToString()}";
        _textCost.text = cost.ToString();
        

        // Ki?m tra level t?i ?a
        var newLevel = playerData.BoosterLevel[id] + 1;
        _btn.interactable = (cost <= playerData.Coin) && (newLevel < boosterConfig.GetMaxLevel());
    }
}

[System.Serializable]
public enum BoosterType
{
    Capacity = 0,
    AddWarrior = 1,
    WarriorUpgrade = 2,
    Income = 3
}