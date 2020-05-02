using UnityEngine;

[System.Serializable]
public class AchievementType
{
    public AchievementName Name;
    public GameObject Achievement;
}


public enum AchievementName
{
    None,
    EnemyDeathAchievement,
    PlayerBulletFireAchievement
}
