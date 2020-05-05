using UnityEngine;
using TMPro;
using Generic;

namespace Singalton
{
    public class UIService : MonoSingletonGeneric<UIService>
    {
        public Transform AchievementParent;
        public TextMeshProUGUI ScoreText;
        public TextMeshProUGUI EnemyKillText;

        protected override void Awake()
        {
            base.Awake();
            Init();
        }


        void Init()
        {
            ShowScore(0);
            ShowEnemyKill(0);
        }


        public async void ShowAchivementUI(GameObject achievement, float showTime)
        {
            GameObject go = Instantiate(achievement, AchievementParent);
            await new WaitForSeconds(showTime);
            Destroy(go);

        }


        public void ShowScore(int scoreValue)
        {
            ScoreText.text = Constants.Score + scoreValue;
        }


        public void ShowEnemyKill(int enemyKillCount)
        {
            EnemyKillText.text = Constants.EnemiesKilled + enemyKillCount;
        }
    }
}