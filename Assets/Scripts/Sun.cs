using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sun : MonoBehaviour
{
    public int value;

    public Vector3 destination;
    private float collectSpeed=30f;

    [HideInInspector]
    public bool isFalling;
    public float fallSpeed;

    [HideInInspector]
    public bool isProduced;
    public float produceDistance;
    public float produceMoveSpeed;
    private Vector3 produceDirection;
    private Vector3 produceDestination;

    public float collectDistance;
    public int disappearTime;
    private int disappearTimer;
    public bool isCollected;

    private void Start()
    {
        produceDirection = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0).normalized;
        produceDestination = transform.position + produceDirection * produceDistance;
    }

    private void Update()
    {
        float distanceBetweenMouseAndSun = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - Camera.main.transform.position - transform.position).magnitude;
        if (Input.GetMouseButton(0) && distanceBetweenMouseAndSun < collectDistance && !isCollected)
            Collect();
    }

    private void FixedUpdate()
    {
        Fall();
        disappearTimer++;
        if (disappearTimer >= disappearTime && !isCollected)
            Disappear();
        if(isCollected)
        {
            Vector3 direction = (destination - transform.position).normalized;

            if((transform.position-destination).magnitude>0.2f)
                transform.position += collectSpeed * direction * Time.fixedDeltaTime;
            else
            {
                Destroy(gameObject);
            }
        }
        if(isProduced)
        {
            //生产出的阳光随机落点
            if ((transform.position - produceDestination).magnitude > 0.1f)
                transform.position += produceMoveSpeed * produceDirection * Time.fixedDeltaTime;
        }
    }

    private void Disappear()
    {
        Debug.Log("sun disappear");
        Destroy(gameObject);
    }

    public void Collect()
    {
        isCollected = true;
        SunManager.instance.sun += value;

        //(动画表现...)
    }

    public Vector3 fallDestination;
    public void Fall()
    {
        if ((transform.position - fallDestination).magnitude > 0.1f&&isFalling)
        {
            transform.position += Vector3.down * fallSpeed * Time.fixedDeltaTime;
        }
    }
}
