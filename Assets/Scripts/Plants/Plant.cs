using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlantName
{
    NoPlant,
    Peashooter,
    Sunflower,
    CherryBomb,
    Wallnut,
    PotatoMine,
    SnowPea,
    Chomper,
    Repeater
}

public abstract class Plant : MonoBehaviour
{
    public PlantName plantName;
    public int cost;

    public int maxHealth;
    protected int currentHealth;



    protected void Start()
    {
        currentHealth = maxHealth;

    }

    protected void FixedUpdate()
    {
        //Debug.Log(currentHealth);
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log(currentHealth);
        if (currentHealth <= 0)
            Die();
    }

    /// <summary>
    /// 一般植物死亡时直接消失，爆炸类植物死亡时造成伤害并播放动画，然后死亡
    /// </summary>
    protected virtual void Die()
    {
        Destroy(gameObject);
    }

    /*/// <summary>
    /// 被铲子铲走
    /// </summary>
    public void Remove()
    {
        Destroy(gameObject);
    }*/
}
