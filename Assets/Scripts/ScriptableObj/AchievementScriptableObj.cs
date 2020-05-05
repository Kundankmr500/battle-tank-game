using UnityEngine;


namespace ScriptableObj
{
    [CreateAssetMenu(fileName = "AchievementConfig", menuName = "ScriptableObjects/NewAchievementConfig")]
    public class AchievementScriptableObj : ScriptableObject
    {
        public AchievementName Name;
        public GameObject Achievement;
        [Range(5, 500)]
        public int KillCount;
        [Range(2, 10)]
        public int AchievementShowingTime;
    }
}


public enum AchievementName
{
    None,
    EnemyDeathAchievement,
    PlayerBulletFireAchievement
}
