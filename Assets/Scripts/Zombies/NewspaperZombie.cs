using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewspaperZombie : Zombie
{
    public float madSpeed;
    public int madDamage;
    public float stunTime;
    private bool isMad;

    private void Update()
    {
        if (currentHealth <= bodyHealth && !isMad)
            LosePaper();
    }

    private new void FixedUpdate()
    {
        base.FixedUpdate();
        if(currentHealth>=bodyHealth)
        {
            isCold = false;
        }
    }

    private void LosePaper()
    {
        isMad = true;
        speed = 0;
        damage = 0;
        Invoke("GetMad", stunTime);
    }

    private void GetMad()
    {
        speed = madSpeed;
        damage = madDamage;
    }
}
