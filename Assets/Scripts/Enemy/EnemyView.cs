using System.Collections;
using UnityEngine;


namespace Enemy
{
    [RequireComponent(typeof(AudioSource), typeof(Rigidbody), typeof(EnemyHealth))]
    public class EnemyView : MonoBehaviour
    {
        public Transform FireTransform;
        public AudioSource MovementAudio;
        public AudioClip EngineIdling;
        public AudioClip EngineDriving;
        public ParticleSystem[] particleSystems;
        public TankPatrollingState tankPatrollingState;
        public TankChasingState tankChasingState;
        public TankFiringState tankFiringState;
        public TankIdleState tankIdleState;
        public Rigidbody enemyBody;

        [SerializeField]
        private TankState currentState;
        private Coroutine fireCoroutine;
        
        public EnemyController enemyController;

        internal void Initialize(EnemyController controller)
        {
            enemyController = controller;
            InitAllVariables();
        }


        private void InitAllVariables()
        {
            transform.SetParent(enemyController.EnemyParent);
            enemyBody = GetComponent<Rigidbody>();
            GetComponent<EnemyHealth>().Initialize(enemyController);

            enemyBody.isKinematic = false;

            for (int i = 0; i < particleSystems.Length; ++i)
            {
                particleSystems[i].Play();
            }

            ChangeState(tankIdleState);
        }


        private void OnCollisionEnter(Collision collision)
        {
            IDestructable destructable = collision.gameObject.GetComponent<IDestructable>();

            if (destructable != null)
            {
                destructable.TakeDamage(100);
            }
        }

        public IEnumerator StartFiring(Transform target)
        {
            enemyController.FireBullet(FireTransform);
            fireCoroutine = null;
            yield return new WaitForSeconds(1.5f);

            if (target && fireCoroutine == null && currentState == tankFiringState)
                fireCoroutine = StartCoroutine(StartFiring(target));
        }


        public void KillView()
        {
            Destroy(gameObject);
        }


        public void ChangeState(TankState newState)
        {
            if(currentState != null)
            {
                currentState.OnExitState();
            }

            currentState = newState;

            currentState.OnEnterState();
        }

    }
}
