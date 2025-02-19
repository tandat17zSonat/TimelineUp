using HyperCasualRunner.PopulatedEntity;
using TMPro;
using UnityEngine;

namespace TimelineUp.Obstacle
{
    public class LockEffect : BaseObstacleEffect
    {
        [SerializeField] TMP_Text textHp;

        private bool _locked = false;
        private int _collisionCount = 0;

        public bool Locked
        {
            get { return _locked; }
            set
            {
                gameObject.SetActive(value);
                _locked = value;
            }
        }

        private int amount = 10;
        public int Amount { get { return amount; } }

        public override void ApplyEffect(PopulatedEntity entity)
        {
            // Đẩy lùi
            if (_collisionCount == 0)
            {
                var runnerMover = GameplayManager.Instance.RunnerMover;
                runnerMover.SetPushback();
            }
            else
            {
                Destroy();
            }
            _collisionCount++;
        }

        public override void ApplyEffect(Projectile projectile)
        {
            amount -= 1;

            if (amount == 0)
            {
                Locked = false;
            }
        }

        private void Update()
        {
            textHp.text = (-amount).ToString();
        }

        public override void Reset()
        {
            _collisionCount = 0;
            amount = 2;
        }

        public void SetAmount(int num)
        {
            amount = num;
        }
    }
}
