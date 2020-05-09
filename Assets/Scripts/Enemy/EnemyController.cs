using Singalton;
using UnityEngine;
using Bullet;

namespace Enemy
{
    public class EnemyController
    {
        public EnemyController(EnemyModel enemyModel, EnemyView enemyPrefab, Transform enemyParent, Vector3 spawnPos)
        {
            EnemyModel = enemyModel;
            EnemyParent = enemyParent;
            EnemyView = GameObject.Instantiate<EnemyView>(enemyPrefab);
            EnemyView.Initialize(this);
        }


        public EnemyModel EnemyModel { get; private set; }
        public EnemyView EnemyView { get; private set; }
        public Transform EnemyParent { get; private set; }


        public EnemyModel GetModel()
        {
            return EnemyModel;
        }


        public void FireBullet(Transform bulletTransform)
        {
            BulletController bulletConroller = BulletService.Instance.GetBullet(bulletTransform, EnemyModel.TankDamageBooster);
            bulletConroller.FireBullet(bulletTransform, EnemyModel.BulletLaunchForce);
            
        }

        public void EnemyTurn(Transform target)
        {
            Vector3 enemyDir = target.position - EnemyView.transform.position;
            enemyDir.Normalize();
            EnemyMove(enemyDir);
            EnemyView.transform.rotation = Quaternion.Slerp(EnemyView.transform.rotation, 
                Quaternion.LookRotation(enemyDir), EnemyModel.TurnSpeed * Time.deltaTime);
        }


        public void EnemyMove(Vector3 enemyDir)
        {
            EnemyView.enemyBody.velocity = enemyDir ;
        }


        public void DisableTank()
        {
            VFXManager.Instance.PlayVFXClip(VFXName.TankExplosion, EnemyView.transform.position, EnemyParent);
            SoundManager.Instance.PlaySoundClip(ClipName.TankExplosion);
            EnemyView.Disable();
        }

        public void OnDeath()
        {
            EnemyService.Instance.DestroyEnemy(this);
        }
    }
}
