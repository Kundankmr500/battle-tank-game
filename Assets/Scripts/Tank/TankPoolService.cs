using UnityEngine;
using Generic;

namespace Tank
{
    public class TankPoolService : ServicePoolGeneric<TankController>
    {
        private TankModel tankModel;
        private TankView tankPrefab;
        private Transform tankParent;


        public TankController GetTank(TankModel tankModel, TankView tankView, Transform tankParent)
        {
            this.tankModel = tankModel;
            tankPrefab = tankView;
            this.tankParent = tankParent;

            TankController tankController = GetItem();
            InitItem(tankController);
            return tankController;
        }


        public override void InitItem(TankController tankController)
        {
            tankController.TankView.CheckTankTransform();
        }


        protected override TankController CreateItem()
        {
            TankController tankController = new TankController(tankModel, tankPrefab, tankParent);
            return tankController;
        }

        public override void ReturnItem(TankController item)
        {
            base.ReturnItem(item);
            item.DisableTank();
        }
    }
}
