using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewspaperZombie : Zombie
{
    public Sprite mailEmpty, mail;

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

    public override void ArmorBreak()
    {
        
    }

    private void LosePaper()
    {
        armor.GetComponent<SpriteRenderer>().sprite = mailEmpty;
        isMad = true;
        speed = 0;
        damage = 0;
        Invoke("GetMad", stunTime);
    }

    private void GetMad()
    {
        armor.GetComponent<SpriteRenderer>().sprite = mail;
        speed = madSpeed;
        damage = madDamage;
    }
}
