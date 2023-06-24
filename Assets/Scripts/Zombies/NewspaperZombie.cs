using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zombies;

public class NewspaperZombie : Zombie
{
    public Sprite mailEmpty, mail,mailfull;

    public float madSpeed;
    public int madDamage;
    public float stunTime;
    private bool isMad;

    protected new void Start()
    {
        base.Start();
        anim.armor.sprite = mailfull;
    }

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
        anim.armor.sprite = mailEmpty;
        isMad = true;
        speed = 0;
        damage = 0;
        Invoke("GetMad", stunTime);
    }

    private void GetMad()
    {
        anim.armor.sprite = mail;
        speed = madSpeed;
        damage = madDamage;
    }
}
