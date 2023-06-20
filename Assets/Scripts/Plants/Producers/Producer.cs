using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Producer : Plant
{
    public GameObject product;
    public GameObject productPosition;

    //初始生产周期范围与正常生产周期范围
    public int minFirstProducePeriod, maxFirstProducePeriod;
    public int minProducePeriod, maxProducePeriod;

    private int producePeriod;
    private int produceTimer;

    protected new void Start()
    {
        base.Start();
        //Debug.Log("Sunflower planted");
        produceTimer = 0;
        producePeriod = Random.Range(minFirstProducePeriod, maxFirstProducePeriod);
    }

    private new void FixedUpdate()
    {
        base.FixedUpdate();
        produceTimer++;
        if (produceTimer >= producePeriod)
            Produce(); ;
    }

    private void Produce()
    {
        Debug.Log("sun produced");
        ResetProduce();
        Instantiate(product, productPosition.transform.position, Quaternion.Euler(0, 0, 0)).GetComponent<Sun>().isFalling=false;
    }

    private void ResetProduce()
    {
        produceTimer = 0;
        producePeriod = Random.Range(minProducePeriod, maxProducePeriod);
    }
}
