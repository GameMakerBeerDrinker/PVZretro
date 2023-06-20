using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DirectBullet : MonoBehaviour
{
    public int damage;
    public bool isFrozen;

    //public int range;

    public float speed;
    public Vector3 direction;

    private void FixedUpdate()
    {
        Move();
        Vector3 view = Camera.main.WorldToViewportPoint(transform.position);
        if (Mathf.Abs(view.x) > 1 && Mathf.Abs(view.y) > 1) 
        {
            Destroy(gameObject);
        }
    }

    private void Move()
    {
        transform.position += direction.normalized * speed * Time.fixedDeltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag=="Zombie")
        {
            float distanceBetweenBulletAndZombie = Mathf.Infinity;
            GameObject takeDamageZombie = null;
            foreach(GameObject zombie in ZombieManager.instance.aliveZombies)
            {
                //僵尸与子弹同行
                if(Mathf.Abs( zombie.transform.position.y-transform.position.y)<GameManager.instance.tilemap.cellSize.y/2)
                {
                    //找最近的僵尸
                    if(Mathf.Abs(zombie.transform.position.x-transform.position.x)<distanceBetweenBulletAndZombie)
                    {
                        distanceBetweenBulletAndZombie = Mathf.Abs(zombie.transform.position.x - transform.position.x);
                        takeDamageZombie = zombie;
                    }
                }
            }
            if (takeDamageZombie != null)
            {
                takeDamageZombie.GetComponent<Zombie>().TakeDamage(damage);
                if(isFrozen)
                {
                    takeDamageZombie.GetComponent<Zombie>().isCold = true;
                    takeDamageZombie.GetComponent<Zombie>().coldTimer = 0;
                }
            }
            Split();
        }
    }

    private void Split()
    {
        Destroy(gameObject);
    }
}
