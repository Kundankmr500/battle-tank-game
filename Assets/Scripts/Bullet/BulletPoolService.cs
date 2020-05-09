using UnityEngine;
using Generic;

namespace Bullet
{
    public class BulletPoolService : ServicePoolGeneric<BulletController>
    {
        private BulletModel bulletModel;
        private BulletView bulletPrefab;
        private Transform bulletParent;

        public BulletController GetBullet(BulletModel bulletModel, BulletView bulletView, Transform bulletParent)
        {
            this.bulletModel = bulletModel;
            bulletPrefab = bulletView;
            this.bulletParent = bulletParent;

            BulletController bulletController = GetItem();
            InitItem(bulletController);
            return bulletController;
        }

        public override void InitItem(BulletController bulletController)
        {
            bulletController.BulletView.CheckBulletTransform();
            Debug.Log("InitItem ");
        }

        protected override BulletController CreateItem()
        {
            BulletController bulletController = new BulletController(bulletModel, bulletPrefab, bulletParent);
            return bulletController;
        }

        public override void ReturnItem(BulletController item)
        {
            base.ReturnItem(item);
            item.DisableTank();
        }
    }
}
