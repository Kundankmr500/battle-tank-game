using System;
using Generic;

public class EventService : MonoSingletonGeneric<EventService>
{
    public event Action OnEnemyDeath;
    public event Action OnBulletFired;


    protected override void Awake()
    {
        base.Awake();
    }


    public void FireEnemyDeathEvent()
    {
        OnEnemyDeath?.Invoke();
    }


    public void FireBulletFireEvent()
    {
        OnBulletFired?.Invoke();
    }
}
