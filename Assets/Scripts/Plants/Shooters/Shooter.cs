using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Shooter : Plant
{
    public GameObject bullet;

    public _Scripts.PeaShooterAnim peaShooterAnim;

    //用来指示子弹发射位置的空物体
    public GameObject bulletPosition;
    //从发射位置发射的子弹方位
    public Vector3 shootDirection;

    public int bulletCount;
    
    //攻击周期的波动范围
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
            if (DetectZombie())
                Attack();
            ResetAttack();
        }
    }

    protected bool DetectZombie()
    {
        foreach(GameObject zombie in ZombieManager.instance.aliveZombies)
        {
            Vector3 view = Camera.main.WorldToViewportPoint(zombie.transform.position);
            //僵尸在屏幕内&&僵尸与射手同行&&僵尸位置在射手右方
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
            
            //连发时，每一发子弹比第一发推迟若干倍单位时间
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
