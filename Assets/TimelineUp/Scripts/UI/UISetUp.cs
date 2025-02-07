using System.Collections;
using System.Collections.Generic;
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
            GameplayManager.Instance.LoadGame();
            btnTapToPlay.gameObject.SetActive(false);
        });
    }
}
