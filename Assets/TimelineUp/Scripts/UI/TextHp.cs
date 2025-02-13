using DarkTonic.PoolBoss;
using DG.Tweening;
using TMPro;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class TextHp : MonoBehaviour
{
    [SerializeField] float timeExist = 1.2f;
    [SerializeField] TMP_Text hpText;

    public void Hit(int damage)
    {
        hpText.text = damage.ToString();
        Vector3 randomDir = Random.insideUnitSphere * 1.5f; // Hướng ngẫu nhiên xung quanh
        randomDir.y = Mathf.Abs(randomDir.y) + 1.0f; // Đảm bảo bay lên trên
        transform.localPosition += randomDir;
        //transform.DOMove(transform.position + randomDir, timeExist)
        //    .SetEase(Ease.OutQuad)
        //    .OnComplete(() => Release());

        //hpText.color = Color.red; // Màu máu
        //hpText.DOFade(0, timeExist); // Làm mờ dần
    }

    void Release()
    {
        PoolBoss.Despawn(transform);
    }

    private void Reset()
    {
        
    }
}
