using UnityEngine;

namespace Enemy
{
    public class TankChasingState : TankState
    {
        float targetLookTime;
        Transform target;

        public override void OnEnterState()
        {
            base.OnEnterState();
            Debug.Log("Entering TankChasing state");
        }

        public override void OnExitState()
        {
            base.OnExitState();
            Debug.Log("Exiting TankChasing state");
        }

        private void OnTriggerEnter(Collider other)
        {
            IDestructable destructable = other.GetComponent<IDestructable>();

            if (destructable != null)
            {
                enemyView.ChangeState(this);
                target = other.transform;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            IDestructable destructable = other.GetComponent<IDestructable>();

            if (destructable != null)
            {
                enemyView.ChangeState(enemyView.tankChasingState);
            }
        }

        private void Update()
        {
            targetLookTime += Time.deltaTime;

            if (target && targetLookTime < 2f)
            {
                enemyView.enemyController.EnemyTurn(target);
            }
            else if(targetLookTime >= 4f)
            {
                targetLookTime = 0;
            }
        }
    }
}
