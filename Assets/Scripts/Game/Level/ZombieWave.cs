using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct ZombieWave 
{
    public ZombieName[] zombies;

    public float refreshFactor;
    public int tmax;
}
