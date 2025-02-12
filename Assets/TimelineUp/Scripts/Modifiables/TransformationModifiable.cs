using System;
using DG.Tweening;
using HyperCasualRunner.Tweening;
using NaughtyAttributes;
using UnityEngine;

public class TransformationModifiable : BaseModifiable
{
    [SerializeField] bool use3D;
    [Header("2D")]
    [SerializeField] SpriteRenderer _spriteRenderer;
    [Header("Model 3D")]
    [SerializeField] GameObject[] _renderersByLevel;


    private CharacterSO _characterSO;

    Tweener _deactivateTween;
    Tween _activateTween;
    readonly float _smoothingDuration = 0.5f;
    int _currentLevel = -1;

    public event Action<GameObject> Transformed;

    private void Awake()
    {
        _characterSO = SOManager.GetSO<CharacterSO>();
    }

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
        if(use3D)
        {
            if (_currentLevel != -1) _deactivateTween = _renderersByLevel[_currentLevel].transform.DeactivateSlowly(_smoothingDuration);
            _activateTween = _renderersByLevel[level].transform.ShowSmoothly(_smoothingDuration);
            Transformed?.Invoke(_renderersByLevel[level]);
        }
        else
        {
            _spriteRenderer.sprite = _characterSO.GetCharacterSprite(level);
        }
        
    }
}