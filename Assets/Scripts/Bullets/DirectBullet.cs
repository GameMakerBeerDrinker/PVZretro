using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zombies;

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
        if (transform.position.x>ZombieManager.instance.transform.position.x) 
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
            Zombie takeDamageZombie = null;
            foreach(Zombie zombie in ZombieManager.instance.aliveZombies)
            {
                //��ʬ���ӵ�ͬ��
                if(Mathf.Abs( zombie.transform.position.y-transform.position.y)<GameManager.instance.tilemap.cellSize.y/2)
                {
                    //������Ľ�ʬ
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
