using HyperCasualRunner.PopulatedEntity;
using TMPro;
using UnityEngine;

namespace TimelineUp.Obstacle
{
    public class LockEffect : BaseObstacleEffect
    {
        [SerializeField] TMP_Text textHp;

        private bool _locked = false;
        public bool Locked
        {
            get { return _locked; }
            set
            {
                gameObject.SetActive(value);
                _locked = value;
            }
        }

        private int hp = 10;

        public override void ApplyEffect(PopulatedEntity entity)
        {
            //// Đẩy lùi
            //var player = GameplayManager.Instance.Player;
            //var pos = player.transform.position;
            //pos.z = pos.z - 5;
            //player.transform.position = pos;

            //gameObject.SetActive(false);
        }

        public override void ApplyEffect(Projectile projectile)
        {
            hp -= 1;

            if (hp == 0)
            {
                Locked = false;
            }
        }

        private void Update()
        {
            textHp.text = (-hp).ToString();
        }

        public override void Reset()
        {
        }
    }
}
