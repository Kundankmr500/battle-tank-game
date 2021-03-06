﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tank;

namespace ScriptableObj
{
    [CreateAssetMenu(fileName = "TankConfiguration", menuName = "ScriptableObjects/NewTank")]
    public class TankScriptableObj : ScriptableObject
    {
        public TankTypes TankTypes;
        public TankView TankView;
        public Transform TankSpawnPoint;
        [Range(1, 10)]
        public float TankDamageBooster;
        [Range(1, 100)]
        public float Health;
        [Range(1, 20)]
        public float BulletLaunchForce;
        [Range(1, 10)]
        public int TankSpeed;
        [Range(1, 360)]
        public float TurnSpeed;
        [Range(0.1f, 0.5f)]
        public float PitchRange;
        public int PlayerNumber;
        public KeyCode FireKey;
    }
}
