using UnityEngine;
using Generic;
using System.Collections.Generic;
using System.Collections;
using ScriptableObj;
using Enemy;

namespace Tank
{
    [RequireComponent(typeof(TankPoolService))]
    public class TankService : MonoSingletonGeneric<TankService>
    {
        
        public Transform TankParent;
        public List<TankScriptableObj> TankScriptableObjs;

        private TankPoolService tankPoolService;
        private TankView TankPrefab;

        protected override void Awake()
        {
            base.Awake();
        }

        void Start()
        {
            tankPoolService = GetComponent<TankPoolService>();
            for (int i = 0; i < TankScriptableObjs.Count; i++)
            {
                SpawnTank(i);
            }
            
        }

        void SpawnTank(int tankIndex)
        {
            TankModel tankModel = new TankModel(TankScriptableObjs[tankIndex]);
            TankPrefab = tankModel.TankView;
            TankController tankController = tankPoolService.GetTank(tankModel, TankPrefab, TankParent);
            tankPoolService.InitItem(tankController);
        }


        public void DestroyTank(TankController tankController)
        {
            tankPoolService.ReturnItem(tankController);

            StartCoroutine(Haltgame(0.2f));
            StartCoroutine(EnemyService.Instance.DestroyAllEnemies());
        }


        public IEnumerator Haltgame(float scaleValue)
        {
            Time.timeScale = scaleValue;
            yield return new WaitForSeconds(.5f);

            scaleValue += .2f;
            if (scaleValue < 1)
                StartCoroutine(Haltgame(scaleValue));
        }

    }
}
