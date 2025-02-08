using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using HyperCasualRunner.GenericModifiers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UISetUp : MonoBehaviour
{
    [SerializeField] Button btnTapToPlay;
    
    [Header("Exp Bar")]
    [SerializeField] Slider sliderExp;
    [SerializeField] TMP_Text textNum;
    [SerializeField] TMP_Text textNextNum;

    void Start()
    {
        btnTapToPlay.onClick.AddListener(() =>
        {
            Debug.Log($"Click tap to play");
            GameplayManager.Instance.StartGame();
            btnTapToPlay.gameObject.SetActive(false);
        });

        // effect button
        var oldScale = btnTapToPlay.transform.localScale;
        var seq = DOTween.Sequence();
        seq.Append(btnTapToPlay.transform.DOScale(new Vector3(1.1f, 1.1f, 1f), 1f).SetEase(Ease.InOutQuad));
        seq.Append(btnTapToPlay.transform.DOScale(oldScale, 1f).SetEase(Ease.InOutQuad));
        seq.SetLoops(-1, LoopType.Yoyo);

        GameplayManager.Instance.OnRestart += () =>
        {
            btnTapToPlay.gameObject.SetActive(true);
        };
    }

    public void SetUIExpBar(int currentLevel, float percent)
    {
        textNum.text = currentLevel.ToString();
        textNextNum.text = (currentLevel + 1).ToString();

        sliderExp.value = percent;
    }

    private void Update()
    {
        // Test upgrade + add population
        if (Input.GetKeyDown(KeyCode.Space))
        {
            var populationManager = GameplayManager.Instance.PopulationManager;
            populationManager.GetComponent<TransformationModifier>().TransformDirectly(1);
            populationManager.AddPopulation(1);
        }

        var collectorLevel = GameplayManager.Instance.CollectorLevel;
        var exp = GameplayManager.Instance.ExpCollectorInGame;

        var gameConfigData = GameManager.Instance.GameConfigData;
        var expToUpgrade = gameConfigData.GetExpToUpgradeWarriorNumber(collectorLevel + 1);

        exp = Mathf.Min(exp, expToUpgrade);
        SetUIExpBar(collectorLevel, (float)exp / expToUpgrade);
    }
}
