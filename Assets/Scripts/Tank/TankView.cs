using UnityEngine;
using System;


namespace Tank
{
    [RequireComponent(typeof(AudioSource), typeof(Rigidbody), typeof(TankHealth))]
    public class TankView : MonoBehaviour
    {
        public Transform FireTransform;
        public AudioSource MovementAudio;
        public AudioClip EngineIdling;
        public AudioClip EngineDriving;
        public ParticleSystem[] ParticleSystems;

        private string movementAxisName;
        private string turnAxisName;
        private Rigidbody tankBody;
        private float movementInputValue;
        private float turnInputValue;
        private float originalPitch;
        private float pitchRange;
        private float turnSpeed;
        private float speed;
        private int playerNumber;
        private KeyCode fireButton;
        private TankController tankController;


        private void Awake()
        {
            //Debug.Log("Awake "+ DateTime.Now.Millisecond);
        }

        private void Start()
        {
            //Debug.Log("Start "+ DateTime.Now.Millisecond);
            movementAxisName = Constants.VerticalInput + playerNumber;
            turnAxisName = Constants.HorizontalInput + playerNumber;

            originalPitch = MovementAudio.pitch;
        }

        public void Initialize(TankController tankController)
        {
            Debug.Log("Initialize "+ DateTime.Now.Millisecond);
            this.tankController = tankController;
            InitAllVariables();
        }


        private void InitAllVariables()
        {
            transform.SetParent(tankController.TankParent);
            playerNumber = tankController.GetModel().PlayerNumber;
            tankBody = GetComponent<Rigidbody>();
            fireButton = tankController.GetModel().FireKey;
            GetComponent<TankHealth>().Initialize(tankController);

            tankBody.isKinematic = false;

            movementInputValue = 0f;
            turnInputValue = 0f;

            for (int i = 0; i < ParticleSystems.Length; ++i)
            {
                ParticleSystems[i].Play();
            }
        }

        public void CheckTankTransform()
        {
            Enable();
            transform.position = tankController.GetModel().SpawnPoint.position;
            transform.rotation = tankController.GetModel().SpawnPoint.rotation;
        }


        private void OnDisable()
        {
            tankBody.isKinematic = true;

            for (int i = 0; i < ParticleSystems.Length; ++i)
            {
                ParticleSystems[i].Stop();
            }
        }


        private void Update()
        {
            //Debug.Log("Update " + DateTime.Now.Millisecond);
            ChekingPlayerInput();

            tankController.PlayEngineAudio(movementInputValue, turnInputValue, MovementAudio,
                                       EngineDriving, EngineIdling, originalPitch);
        }


        private void ChekingPlayerInput()
        {
            movementInputValue = Input.GetAxis(movementAxisName);
            turnInputValue = Input.GetAxis(turnAxisName);

            if (Input.GetKeyDown(fireButton))
            {
                tankController.FireBullet(FireTransform);
            }
        }


        private void FixedUpdate()
        {
            //Debug.Log("FixedUpdate " + DateTime.Now.Millisecond);
            tankController.TankMove(tankBody, transform, movementInputValue);

            tankController.TankTurn(tankBody, turnInputValue);
        }

        public void Disable()
        {
            gameObject.SetActive(false);
        }


        public void Enable()
        {
            gameObject.SetActive(true);
        }
    }
}
