using UnityEngine;
using System.Collections;
using Singalton;
using Enemy;

namespace Bullet
{
    [RequireComponent(typeof(Rigidbody), typeof(AudioSource))]
    public class BulletView : MonoBehaviour
    {
        public LayerMask TankMask;                        
        public Rigidbody bulletBody;

        private BulletController bulletController;


        public void Initialize(BulletController bulletController)
        {
            this.bulletController = bulletController;
            InitAllVariables();
        }


        private void InitAllVariables()
        {
            bulletBody = GetComponent<Rigidbody>();
            transform.SetParent(bulletController.BulletParent);
            SoundManager.Instance.PlaySoundClip(ClipName.ShotFiring);
            StartCoroutine(AutoDestroy());
        }


        IEnumerator AutoDestroy()
        {
            yield return new WaitForSeconds(bulletController.GetModel().MaxLifeTime);
            bulletController.DestroyBulletChain();
        }


        private void OnTriggerEnter(Collider other)
        {
            if(!other.GetComponent<TankState>())
                BulletHitProcess();
        }


        private void BulletHitProcess()
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, 
                            bulletController.GetModel().ExplosionRadius, TankMask);

            for (int colliderIndex = 0; colliderIndex < colliders.Length; colliderIndex++)
            {
                Rigidbody targetRigidbody = colliders[colliderIndex].GetComponent<Rigidbody>();

                if (!targetRigidbody)
                    continue;

                targetRigidbody.AddExplosionForce(bulletController.GetModel().ExplosionForce, 
                            transform.position, bulletController.GetModel().ExplosionRadius);

                IDestructable targetHealth = targetRigidbody.GetComponent<IDestructable>();

                if (targetHealth == null)
                    continue;

                float damage = bulletController.CalculateDamage(targetRigidbody.position, transform.position);

                damage += bulletController.GetModel().TankDamageBooster;
                targetHealth.TakeDamage(damage);
            }

            VFXManager.Instance.PlayVFXClip(VFXName.BulletExplosion, 
                    transform.position, bulletController.BulletParent);

            SoundManager.Instance.PlaySoundClip(ClipName.BulletExplosion);

            bulletController.DestroyBulletChain();
        }


        public void KillView()
        {
            Destroy(gameObject);
        }

    }
}
