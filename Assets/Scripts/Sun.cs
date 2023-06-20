using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sun : MonoBehaviour
{
    public int value;

    public bool isFalling;
    public float fallSpeed;

    public float collectDistance;
    public int disappearTime;
    private int disappearTimer;
    public bool isCollected;

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

        //(¶¯»­±íÏÖ...)

        Destroy(gameObject);
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
