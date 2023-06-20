using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Exploder : Plant
{
    private Collider2D explodeArea;

    public int damage;

    public int explodeTime;
    private int explodeTimer;

    private bool exploded;

    protected new void Start()
    {
        base.Start();
        explodeTimer = 0;
        explodeArea = GetComponent<Collider2D>();
        explodeArea.enabled = false;
    }

    private new void FixedUpdate()
    {
        base.FixedUpdate();
        explodeTimer++;
        if (explodeTimer >= explodeTime)
            Explode();
    }

    protected override void Die()
    {
        Explode();
    }

    private void Explode()
    {
        exploded = true;
        explodeArea.enabled = true;
        Debug.Log("explode");

        //爆炸的动画表现......
        Invoke("destory", 0.01f);
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag=="Zombie"&&exploded)
        {
            collision.GetComponent<Zombie>().TakeDamage(damage);
        }
    }

    private void destory()
    {
        Destroy(gameObject);
    }
}
