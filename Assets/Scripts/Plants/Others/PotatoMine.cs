using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zombies;

public class PotatoMine : Plant
{
    protected Collider2D explodeArea;

    public _Scripts.PotMineAnim potMineAnim;

    public int prepareTime;
    protected int prepareTimer;

    public int drillOutTime;
    protected int drillOutTimer;
    private bool readyToExplode;
    private bool exploded;

    public int damage;

    protected new void Start()
    {
        base.Start();
        prepareTimer = 0;
        drillOutTimer = 0;
        
        explodeArea = GetComponent<Collider2D>();
        readyToExplode = false;
    }

    private new void FixedUpdate()
    {
        base.FixedUpdate();
        prepareTimer++;
        if (prepareTimer >= prepareTime)
            DrillOut();
    }

    private void DrillOut()
    {
        drillOutTimer++;
        if (drillOutTimer >= drillOutTime)
            ReadyToExplode();
    }

    private void ReadyToExplode()
    {
        if (!readyToExplode) {
            potMineAnim.GetReady();
            readyToExplode = true;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.tag=="Zombie"&&readyToExplode&&!exploded)
        {
            collision.GetComponent<Zombie>().TakeDamage(damage);
            exploded = true;
            Explode();
        }
    }

    private void Explode()
    {
        /*foreach(GameObject zombie in ZombieManager.instance.aliveZombies)
        {
            if(Mathf.Abs(zombie.transform.position.y - transform.position.y) < GameManager.instance.tilemap.cellSize.y / 2)
            {
                if (Mathf.Abs(zombie.transform.position.x - transform.position.x) < GameManager.instance.tilemap.cellSize.x *2/3)
                {
                    zombie.GetComponent<Zombie>().TakeDamage(damage);
                }
            }
        }*/
        explodeArea.enabled = false;
        //��ը����
        potMineAnim.Explode();

        Invoke("DestroyGameObject", 1f);
    }

    private void DestroyGameObject()
    {
        Destroy(gameObject);
    }
}
