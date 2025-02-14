using System.Collections;
using DG.Tweening;
using UnityEngine;

public class WayPointMover : MonoBehaviour
{
    [SerializeField] float _length = 4f;
    [SerializeField] float _time = 4f;

    private Sequence seq;

    Coroutine _coroutineRun;

    public void SetRun()
    {
        if (_coroutineRun != null) StopCoroutine(_coroutineRun);
        _coroutineRun = StartCoroutine(Run());
    }

    IEnumerator Run()
    {
        float rand = Random.Range(0, 2);
        yield return new WaitForSeconds(rand);

        seq.Kill();

        seq = DOTween.Sequence();
        var oldPos = transform.position + Vector3.right * _length / 2;
        transform.position = oldPos;
        seq.Append(transform.DOMove(oldPos - Vector3.right * _length, _time).SetEase(Ease.Linear));
        seq.Append(transform.DOMove(oldPos, _time).SetEase(Ease.Linear));
        seq.SetLoops(-1);
    }

}
