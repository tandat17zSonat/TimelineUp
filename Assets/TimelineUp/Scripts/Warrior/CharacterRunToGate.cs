using System;
using DG.Tweening;
using HyperCasualRunner.PopulatedEntity;
using HyperCasualRunner.Tweening;
using TimelineUp.Obstacle;
using UnityEngine;

public class CharacterRunToGate : MonoBehaviour
{
    [SerializeField] private float duration;

    private Vector3 _startPoint;
    private Vector3 _endPoint;
    private Vector3 _postionInGate;

    private Sequence _sequence;
    private Action _onComplete;

    public void SetInfo(Vector3 start, Vector3 end, Vector3 positionInGate, Action action)
    {
        _sequence.Kill();

        _startPoint = start;
        _endPoint = end;
        _postionInGate = positionInGate;
        _onComplete = action;
    }

    public void Run()
    {
        _sequence.Kill();

        _sequence = DOTween.Sequence();
        _sequence.Append(transform.DOJump(_startPoint, 1, 1, 1f));
        _sequence.Append(transform.DOMove(_endPoint, duration));
        _sequence.Append(transform.DOJump(_postionInGate, 1, 1, 0.5f));
        _sequence.OnComplete(() =>
        {
            _onComplete?.Invoke();
        });
    }
}
