using UnityEngine;

namespace Enemy
{
    public class TankPatrollingState : TankState
    {
        public override void OnEnterState()
        {
            base.OnEnterState();
            //Debug.Log("Entering Patrolling state");
        }

        public override void OnExitState()
        {
            base.OnExitState();
            //Debug.Log("Exiting Patrolling state");
        }

        private void OnTriggerEnter(Collider other)
        {
            IDestructable destructable = other.GetComponent<IDestructable>();

            if (destructable != null)
            {
                enemyView.ChangeState(this);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            IDestructable destructable = other.GetComponent<IDestructable>();

            if (destructable != null)
            {
                enemyView.ChangeState(enemyView.tankIdleState);
            }
        }

        private void Start()
        {
        }
    }
}
