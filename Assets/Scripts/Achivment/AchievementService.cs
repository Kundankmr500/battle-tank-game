using Generic;
using System.Collections.Generic;
using UnityEngine;

public class AchievementService : MonoSingletonGeneric<AchievementService>
{
    public List<AchievementType> Achievements = new List<AchievementType>();
    [Range(5,20)]
    public int EnemyKillCountAchivement;
    [Range(10,200)]
    public int PlayerFireBulletAchivement;

    private int enemyKillCount;
    private int playerBulletFireCount;
    private int scoreValue;
    private int enemyKillValue;


    protected override void Awake()
    {
        base.Awake();
        SubscribeEvents();
    }


    private void SubscribeEvents()
    {
        EventService.Instance.OnEnemyDeath += OnDeathAchivement;
        EventService.Instance.OnBulletFired += OnPlayerBulletFireAchivement;
        enemyKillCount = 0;
        playerBulletFireCount = 0;
    }

    public void OnDeathAchivement()
    {
        enemyKillCount++;
        CalculateScore();
        if (enemyKillCount == EnemyKillCountAchivement)
        {
            enemyKillCount = 0;
            ProcessAchievement(AchievementName.EnemyDeathAchievement);
        }
    }


    public void OnPlayerBulletFireAchivement()
    {
        playerBulletFireCount++;

        if (playerBulletFireCount == PlayerFireBulletAchivement)
        {
            playerBulletFireCount = 0;
            ProcessAchievement(AchievementName.PlayerBulletFireAchievement);
        }
    }


    private void OnDestroy()
    {
        EventService.Instance.OnEnemyDeath -= OnDeathAchivement;
        EventService.Instance.OnBulletFired -= OnPlayerBulletFireAchivement;
    }


    private void ProcessAchievement( AchievementName achievementName)
    {
        for (int i = 0; i < Achievements.Count; i++)
        {
            if (Achievements[i].Name == achievementName)
            {
                UIService.Instance.ShowAchivementUI(Achievements[i].Achievement);
            }
        }
    }


    private void CalculateScore()
    {
        scoreValue += 10;
        enemyKillValue++;
        UIService.Instance.ShowEnemyKill(enemyKillValue);
        UIService.Instance.ShowScore(scoreValue);
    }

}
