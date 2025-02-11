using System;
using DG.Tweening;
using HyperCasualRunner.Tweening;
using UnityEngine;

public class TransformationModifiable : BaseModifiable
{
    [SerializeField] GameObject[] _renderersByLevel;

    Tweener _deactivateTween;
    Tween _activateTween;
    readonly float _smoothingDuration = 0.5f;
    int _currentLevel = -1;

    public event Action<GameObject> Transformed;

    public override void Initialize(int level)
    {
        if( _currentLevel == level ) return;
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
        if (_currentLevel != -1) _deactivateTween = _renderersByLevel[_currentLevel].transform.DeactivateSlowly(_smoothingDuration);
        _activateTween = _renderersByLevel[level].transform.ShowSmoothly(_smoothingDuration);
        Transformed?.Invoke(_renderersByLevel[level]);
    }
}