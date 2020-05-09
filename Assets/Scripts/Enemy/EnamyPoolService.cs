using UnityEngine;
using Generic;

namespace Enemy
{
    public class EnamyPoolService : ServicePoolGeneric<EnemyController>
    {
        private EnemyModel enemyModel;
        private EnemyView enemyPrefab;
        private Transform enemyParent;
        private Vector3 spawnPos;

        public EnemyController GetEnemy(EnemyModel enemyModel, EnemyView enemyView, Transform enamyParent, Vector3 spawnPos)
        {
            this.enemyModel = enemyModel;
            enemyPrefab = enemyView;
            this.enemyParent = enamyParent;
            this.spawnPos = spawnPos;

            EnemyController enemyController = GetItem();
            InitItem(enemyController);
            return enemyController;
        }


        public override void InitItem(EnemyController enemyController)
        {
            enemyController.EnemyView.CheckBulletTransform(spawnPos);
            Debug.Log("InitItem ");
        }

        protected override EnemyController CreateItem()
        {
            EnemyController enemyController = new EnemyController(enemyModel, enemyPrefab, enemyParent, spawnPos);
            return enemyController;
        }

        public override void ReturnItem(EnemyController item)
        {
            base.ReturnItem(item);
            item.DisableTank();
        }

    }
}
