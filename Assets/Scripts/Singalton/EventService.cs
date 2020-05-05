using System;
using Generic;

namespace Singalton
{
    public class EventService : MonoSingletonGeneric<EventService>
    {
        public event Action<AchievementName> OnEnemyDeath;
        public event Action<AchievementName> OnBulletFired;
        public event Action OnEnemiesHit;


        protected override void Awake()
        {
            base.Awake();
        }


        public void FireEnemyDeathEvent(AchievementName achievementName)
        {
            OnEnemyDeath?.Invoke(achievementName);
        }


        public void FireBulletFireEvent(AchievementName achievementName)
        {
            OnBulletFired?.Invoke(achievementName);
        }


        public void FireOnEnemiesHitEvent()
        {
            OnEnemiesHit?.Invoke();
        }
    }
}
