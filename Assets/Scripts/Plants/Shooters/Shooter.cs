using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zombies;

public abstract class Shooter : Plant
{
    public GameObject bullet;

    public _Scripts.PeaShooterAnim peaShooterAnim;

    //?????????????¦Ë????????
    public GameObject bulletPosition;
    //?????¦Ë?¡Â?????????¦Ë
    public Vector3 shootDirection;

    public int bulletCount;
    
    //??????????????¦¶
    public int minAttackPeriod, maxAttackPeriod;

    protected int attackPeriod;
    protected int attackTimer;
    

    protected new void Start()
    {
        base.Start();
        ResetAttack();
    }
    
    protected new void FixedUpdate()
    {
        base.FixedUpdate();

        attackTimer++;
        if (attackTimer >= attackPeriod)
        {
            if (DetectZombie()) {
                peaShooterAnim.isAlarmed = true;
                Attack();
            }
            else {
                peaShooterAnim.isAlarmed = false;
            }

            ResetAttack();
        }
    }

    protected bool DetectZombie()
    {
        foreach(Zombie zombie in ZombieManager.instance.aliveZombies)
        {
            Vector3 view = Camera.main.WorldToViewportPoint(zombie.transform.position);
            //??????????&&????????????&&???¦Ë???????????
            if (Mathf.Abs(view.x) < 1 && Mathf.Abs(view.y) < 1 && Mathf.Abs(zombie.transform.position.y - transform.position.y) < GameManager.instance.tilemap.cellSize.y/2 && zombie.transform.position.x > bulletPosition.transform.position.x) 
            {
                //peaShooterAnim.SetShootingTrue();
                return true;
            }
        }
        return false;
    }

    protected void Attack()
    {
        for (int i = 0; i < bulletCount; i++)
        {
            
            //??????????????????????????????¦Ë???
            Invoke("GenerateBullet", i * 0.2f);
        }
    }

    

    protected void GenerateBullet()
    {
        peaShooterAnim.SetShootingTrue();
        Instantiate(bullet, bulletPosition.transform.position, Quaternion.Euler(0, 0, 0)).GetComponent<DirectBullet>().direction=shootDirection;
    }

    protected void ResetAttack()
    {
        attackTimer = 0;
        attackPeriod = Random.Range(minAttackPeriod, maxAttackPeriod);
    }

}
