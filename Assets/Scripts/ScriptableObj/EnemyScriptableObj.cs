﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enemy;

[CreateAssetMenu(fileName = "EnemyScriptableObj", menuName = "ScriptableObjects/NewEnemyTank")]
public class EnemyScriptableObj : ScriptableObject
{
    public EnemyType enemyType;
    public EnemyView enemyView;
    public Transform EnemySpawnPoint;
    [Range(1, 10)]
    public float EnemyDamageBooster;
    [Range(1, 100)]
    public float Health;
    [Range(1, 20)]
    public float BulletLaunchForce;
    [Range(1, 10)]
    public int EnemyTankSpeed;
    [Range(1, 360)]
    public float EnemyTurnSpeed;
    [Range(0.1f, 0.5f)]
    public float PitchRange;
    public int EnemyNumber;
}
