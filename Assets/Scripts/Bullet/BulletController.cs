using UnityEngine;

namespace Bullet
{
    public class BulletController
    {
        public BulletController(BulletModel bulletModel, BulletView bulletPrefab, Transform bulletParent)
        {
            BulletModel = bulletModel;
            BulletParent = bulletParent;
            BulletView = GameObject.Instantiate<BulletView>(bulletPrefab);
            BulletView.Initialize(this);
        }

        public BulletModel BulletModel { get; private set; }
        public BulletView BulletView { get; private set; }
        public Transform BulletParent { get; private set; }


        public BulletModel GetModel()
        {
            return BulletModel;
        }


        public void FireBullet(Transform bulletTransform, float bulletLaunchForce)
        {
            BulletView.bulletBody.velocity = bulletLaunchForce * bulletTransform.forward;
            Debug.Log("bulletLaunchForce "+ bulletLaunchForce);
        }

        public void DisableTank()
        {
            BulletView.Disable();
        }


        public void DestroyBulletChain()
        {
            BulletService.Instance.DestroyBullet(this);
        }


        public float CalculateDamage(Vector3 targetPosition, Vector3 bulletPosition)
        {
            Vector3 explosionToTarget = targetPosition - bulletPosition;

            float explosionDistance = explosionToTarget.magnitude;

            float relativeDistance = (BulletModel.ExplosionRadius - explosionDistance) / BulletModel.ExplosionRadius;

            float damage = relativeDistance * BulletModel.MaxDamage;

            damage = Mathf.Max(0f, damage);

            return damage;
        }
    }
}
