using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class UISetUp : MonoBehaviour
{
    [SerializeField] Button btnTapToPlay;
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
}
