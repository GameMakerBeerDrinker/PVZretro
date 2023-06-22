using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ZombieName
{
    RegularZombie,
    FlagZombie,
    ConeheadZombie,
    PoleVaultingZombie,
    BucketheadZombie,
    NewspaperZombie,
}

public abstract class Zombie : MonoBehaviour
{
    public ZombieName zombieName;

    public bool withArmor;
    public int armorHealth;
    public int bodyHealth;
    public int maxhealth;

    public int currentHealth;

    public float speed;
    protected float halfspeed;

    protected bool isEating;
    public int damage;
    protected float halfdamage;

    public bool isCold;
    public int coldTimer;

    private void Start()
    {
        currentHealth = maxhealth;
        halfspeed = speed / 2;
        halfdamage = damage * 1f / 2;
    }

    protected void FixedUpdate()
    {
        if (!isEating)
            Move();
        GetCold();
    }

    protected virtual void GetCold()
    {
        coldTimer++;
        if (coldTimer >= 1000)
            isCold = false;
    }

    private void Move()
    {
        if(!isCold)
            transform.position += speed * Vector3.left * Time.fixedDeltaTime;
        else
            transform.position += speed/2 * Vector3.left * Time.fixedDeltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag=="Plant")
        {
            isEating = true;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Plant")
        {
            collision.GetComponent<Plant>().isTakingDamage = true;
            collision.GetComponent<Plant>().TakeDamage(damage);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag=="Plant")
        {
            isEating = false;
            collision.GetComponent<Plant>().isTakingDamage = false;
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        //Debug.Log(currentHealth);
        if (currentHealth <= 0)
            Die();
    }

    protected void Die()
    {
        GetComponent<Collider2D>().enabled = false;
        ZombieManager.instance.aliveZombies.Remove(gameObject);
        ZombieManager.instance.currentWaveZombies.Remove(gameObject);
        //À¿Õˆ±Ìœ÷......

        Destroy(gameObject);
    }
}