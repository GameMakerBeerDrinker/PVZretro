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
    /// һ��ֲ������ʱֱ����ʧ����ը��ֲ������ʱ����˺������Ŷ�����Ȼ������
    /// </summary>
    protected virtual void Die()
    {
        Destroy(gameObject);
    }

    /*/// <summary>
    /// �����Ӳ���
    /// </summary>
    public void Remove()
    {
        Destroy(gameObject);
    }*/
}
