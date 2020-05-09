using System.Collections.Generic;
using UnityEngine;
using Generic;
using ScriptableObj;


namespace Bullet
{
    [RequireComponent(typeof(BulletPoolService))]
    public class BulletService : MonoSingletonGeneric<BulletService>
    {
        public Transform BulletParent;
        public List<BulletScriptableObj> BulletConfigurations;

        private BulletPoolService bulletPoolService;

        protected override void Awake()
        {
            base.Awake();
            bulletPoolService = GetComponent<BulletPoolService>();
        }


        public BulletController GetBullet(Transform bulletTransform, float tankDamageBooster)
        {
            BulletModel bulletmodel = new BulletModel(bulletTransform, tankDamageBooster, BulletConfigurations[0]);
            BulletController bulletController = bulletPoolService.GetBullet(bulletmodel, bulletmodel.BulletView, BulletParent);
            //bulletPoolService.InitItem(bulletController);
            return bulletController;
        }


        public void DestroyBullet(BulletController bulletController)
        {
            bulletPoolService.ReturnItem(bulletController);
        }

    }
}
