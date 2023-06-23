using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zombies;

[System.Serializable]
public struct ZombieWave 
{
    //[HideInInspector]
    public ZombieName[] zombies;

    //public ZombieName[] zombieNames;
    //public int[] zombieNum;

    public float refreshFactor;
    public int tmax;
}
