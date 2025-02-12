using System.Collections;
using System.Collections.Generic;
using HyperCasualRunner.CollectableEffects;
using HyperCasualRunner.PopulatedEntity;
using TMPro;
using UnityEngine;

public class LockEffect : CollectableEffectBase
{
    [SerializeField] TMP_Text textHp;

    private int hp = 10;

    public void Initialize(int amount)
    {
        hp = amount;
    }

    public override void ApplyEffect(PopulatedEntity entity)
    {
        // Đẩy lùi
        var player = GameplayManager.Instance.Player;
        var pos = player.transform.position;
        pos.z = pos.z - 5;
        player.transform.position = pos;

        gameObject.SetActive(false);
    }

    public override void ApplyHitEffect(Projectile projectile)
    {
        hp -= 1;

        if(hp == 0)
        {
            gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        textHp.text = (-hp).ToString();
    }
}
