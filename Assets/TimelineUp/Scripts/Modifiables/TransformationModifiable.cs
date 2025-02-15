using System;
using System.Collections.Generic;
using DG.Tweening;
using HyperCasualRunner.Tweening;
using UnityEngine;

public class TransformationModifiable : BaseModifiable
{
    [SerializeField] bool use3D;
    [Header("2D")]
    [SerializeField] SpriteRenderer _spriteRenderer;
    [Header("Model 3D")]
    [SerializeField] GameObject[] _renderersByLevel;

    public List<Sprite> ListEntitySprites
    {
        get { return DataManager.TimelineEraSO.entitySprites; }
    }

    Tweener _deactivateTween;
    Tween _activateTween;
    readonly float _smoothingDuration = 0.5f;
    int _currentLevel = -1;

    public event Action<GameObject> Transformed;

    public override void Initialize(int level)
    {
        if (_currentLevel == level) return;
        ChangeGameObject(level);

        _currentLevel = level;
    }

    void OnDestroy()
    {
        _deactivateTween.Kill();
        _activateTween.Kill();
    }

    void ChangeGameObject(int level)
    {
        if (use3D)
        {
            if (_currentLevel != -1) _deactivateTween = _renderersByLevel[_currentLevel].transform.DeactivateSlowly(_smoothingDuration);
            _activateTween = _renderersByLevel[level].transform.ShowSmoothly(_smoothingDuration);
            Transformed?.Invoke(_renderersByLevel[level]);
        }
        else
        {
            _spriteRenderer.sprite = ListEntitySprites[level];
        }

    }

    public void SetSpriteOrder(int order)
    {
        _spriteRenderer.sortingOrder = order;
    }
}