using TimelineUp.Data;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public abstract class BaseButtonBooster : MonoBehaviour
{
    [SerializeField] protected BoosterType type;
    [SerializeField] protected Button _btn;
    [SerializeField] protected TMP_Text _textLevel;
    [SerializeField] TMP_Text _textCost;

    protected int id;
    protected int level;
    protected int cost;

    protected PlayerData playerData;

    public void Initialize()
    {

        this.id = ((int)type);
        playerData = DataManager.PlayerData;

        _btn.onClick.AddListener(() =>
        {
            playerData.Coin -= cost;
            HandleBooster();

            DataManager.SavePlayerData();
        });

    }
    public abstract void HandleBooster();

    public virtual void UpdateVisual()
    {
        var gameConfigData = DataManager.GameplayConfig;

        level = playerData.BoosterLevel[id];

        var boosterConfig = gameConfigData.GetBoosterConfig(type);
        cost = boosterConfig.Costs[level];

        // Ki?m tra level t?i ?a
        var nextLevel = level + 1;
        _textLevel.text = $"Level {level.ToString()}";

        if (nextLevel <= boosterConfig.GetMaxLevel())
        {
            _textCost.text = cost.ToString();
        }
        else
        {
            _textCost.text = $"MAX";
        }
        _btn.interactable = (cost <= playerData.Coin) && (nextLevel <= boosterConfig.GetMaxLevel());
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