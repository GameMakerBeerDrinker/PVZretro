using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zombies;

public class ConeheadZombie : Zombie
{
    public Sprite sprite;
    protected new void Start()
    {
        base.Start();
        anim.armor.sprite = sprite;
    }
}
