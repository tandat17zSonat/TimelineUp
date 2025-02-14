using DarkTonic.PoolBoss;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class TextHp : MonoBehaviour
{
    [SerializeField] float timeExist = 0.2f;
    [SerializeField] TMP_Text hpText;

    public void Hit(int damage)
    {
        hpText.text = (-damage).ToString();
        Vector3 randomDir = Random.insideUnitSphere * 2f; // Hướng ngẫu nhiên xung quanh
        randomDir.y = Mathf.Abs(randomDir.y) + 2f; // Đảm bảo bay lên trên

        transform.DOMove(transform.position + randomDir, timeExist)
            .SetEase(Ease.OutQuad)
            .OnComplete(() => Release());

        hpText.color = Color.red; // Màu máu
        hpText.DOFade(0, timeExist); // Làm mờ dần
    }

    void Release()
    {
        PoolBoss.Despawn(transform);
    }

    private void Reset()
    {
        
    }
}
