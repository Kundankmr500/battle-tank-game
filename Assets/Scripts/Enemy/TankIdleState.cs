
using System.Collections;
using UnityEngine;


namespace Enemy
{
    public class TankIdleState : TankState
    {
        public override void OnEnterState()
        {
            base.OnEnterState();
            Debug.Log("Entering Idle state");
        }

        public override void OnExitState()
        {
            base.OnExitState();
            Debug.Log("Exiting idle state");
        }

    }
}
