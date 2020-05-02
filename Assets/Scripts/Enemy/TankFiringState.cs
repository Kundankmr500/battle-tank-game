using UnityEngine;

namespace Enemy
{
    public class TankFiringState : TankState
    {
        Transform target;
        Coroutine fireCoroutine;

        public override void OnEnterState()
        {
            base.OnEnterState();
            //Debug.Log("Entering TankFiring state");
        }


        public override void OnExitState()
        {
            base.OnExitState();
            //Debug.Log("Exiting TankFiring state");
        }


        private void OnTriggerEnter(Collider other)
        {
            IDestructable destructable = other.GetComponent<IDestructable>();

            if (destructable != null)
            {
                enemyView.ChangeState(this);
                target = other.transform;
                
                if (fireCoroutine == null)
                {
                    fireCoroutine = StartCoroutine(enemyView.StartFiring(target));
                }
            }
        }


        private void OnTriggerExit(Collider other)
        {
            IDestructable destructable = other.GetComponent<IDestructable>();

            if (destructable != null)
            {
                enemyView.ChangeState(enemyView.tankChasingState);
                fireCoroutine = null;
            }
        }


        private void Update()
        {
            if (target)
            {
                enemyView.enemyController.EnemyTurn(target);
            }
        }
    }
}
