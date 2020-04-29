using UnityEngine;

namespace Enemy
{
    public class TankState : MonoBehaviour
    {
        [SerializeField]
        protected EnemyView enemyView;

        public virtual void OnEnterState()
        {
            this.enabled = true;
        }

        public virtual void OnExitState()
        {
            this.enabled = false;
        }

        public virtual void Tick() { }
    }
}
