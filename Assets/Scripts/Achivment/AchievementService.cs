using Generic;
using ScriptableObj;
using System.Collections.Generic;
using UnityEngine;

namespace Singalton
{
    public class AchievementService : MonoSingletonGeneric<AchievementService>
    {
        public List<AchievementScriptableObj> Achievements = new List<AchievementScriptableObj>();

        private int enemyKillCount;
        private int playerBulletFireCount;
        private int scoreValue;
        private int enemyKillValue;
        private GameData gameData = new GameData();


        protected override void Awake()
        {
            base.Awake();
            SubscribeEvents();
            enemyKillCount = 0;
            playerBulletFireCount = 0;
        }


        private void SubscribeEvents()
        {
            EventService.Instance.OnEnemyDeath += OnDeathAchivement;
            EventService.Instance.OnBulletFired += OnPlayerBulletFireAchivement;
            EventService.Instance.OnEnemiesHit += OnEnemiesHitAchivement;
        }


        public void OnEnemiesHitAchivement()
        {
            gameData.EnemiesHit++;
            GameService.Instance.SaveData(gameData);
        }


        public void OnDeathAchivement(AchievementName achievementName)
        {
            enemyKillCount++;
            CalculateScore();
            gameData.EnemiesKilled++;
            GameService.Instance.SaveData(gameData);
            int index = FindAchievementIndex(achievementName);

            if (enemyKillCount == Achievements[index].KillCount)
            {
                enemyKillCount = 0;
                UIService.Instance.ShowAchivementUI(Achievements[index].Achievement,
                                        Achievements[index].AchievementShowingTime);
            }
        }


        public void OnPlayerBulletFireAchivement(AchievementName achievementName)
        {
            playerBulletFireCount++;
            gameData.BulletsFired++;
            GameService.Instance.SaveData(gameData);
            int index = FindAchievementIndex(achievementName);

            if (playerBulletFireCount == Achievements[index].KillCount)
            {
                playerBulletFireCount = 0;
                UIService.Instance.ShowAchivementUI(Achievements[index].Achievement,
                                        Achievements[index].AchievementShowingTime);
            }
        }


        private void OnDestroy()
        {
            UnsubscribeEvents();

        }


        private void UnsubscribeEvents()
        {
            EventService.Instance.OnEnemyDeath -= OnDeathAchivement;
            EventService.Instance.OnBulletFired -= OnPlayerBulletFireAchivement;
            EventService.Instance.OnEnemiesHit -= OnEnemiesHitAchivement;
        }


        private int FindAchievementIndex(AchievementName achievementName)
        {
            int index = 0;
            for (int i = 0; i < Achievements.Count; i++)
            {
                if (Achievements[i].Name == achievementName)
                {
                    index = i;
                    return index;
                }
            }
            return index;
        }


        private void CalculateScore()
        {
            scoreValue += 10;
            enemyKillValue++;
            gameData.PlayerScore = scoreValue;
            UIService.Instance.ShowEnemyKill(enemyKillValue);
            UIService.Instance.ShowScore(scoreValue);
        }

    }
}