using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Card : MonoBehaviour
{
    public PlantName plantName;
    public GameObject plant;

    public GameObject dimImage;
    public GameObject rechargeImage;

    public enum CardState
    {
        Dim,
        Ready,
        Chosen,
        Recharging,
    }

    public CardState cardState;
    public int cost;
    public int recharge;
    public int startingCoolDown;
    public int rechargingTimer;

    private void Start()
    {
        rechargingTimer = startingCoolDown;
        cardState = CardState.Recharging;
    }

    private void Update()
    {
        if(cardState==CardState.Dim)
        {
            rechargeImage.SetActive(false);
            dimImage.SetActive(true);
            if (SunManager.instance.sun>=cost)
            {
                cardState = CardState.Ready;
            }
        }
        else if(cardState==CardState.Ready)
        {
            dimImage.SetActive(false);
            rechargeImage.SetActive(false);
            if (SunManager.instance.sun<cost)
            {
                cardState = CardState.Dim;
            }
            if(Input.GetMouseButtonDown(0)&&MouseOnCard()&& GameManager.instance.pickedUpCard == null)
            {
                Debug.Log("Card Picked");
                cardState = CardState.Chosen;
                Invoke("PickThisCard", 0.01f);
            }
        }
        else if(cardState==CardState.Chosen)
        {
            dimImage.SetActive(true);
            rechargeImage.SetActive(true);
            rechargeImage.transform.position = dimImage.transform.position;
            rechargeImage.transform.localScale = dimImage.transform.localScale;
        }
        else if(cardState==CardState.Recharging)
        {
            rechargeImage.SetActive(true);
            float rechargePercantage = rechargingTimer * 1f / recharge;
            rechargeImage.transform.localScale = new Vector3(dimImage.transform.localScale.x, rechargePercantage* dimImage.transform.localScale.y, 1);
            rechargeImage.transform.position = transform.position + Vector3.up * (1 - rechargePercantage)* dimImage.transform.localScale.y * 0.5f;
            if (rechargingTimer<=0)
            {
                cardState = CardState.Dim;
            }
        }
    }

    private void FixedUpdate()
    {
        if(rechargingTimer>0)
            rechargingTimer--;
        //Debug.Log(rechargingTimer);
        //Debug.Log(cardState);
    }

    private bool MouseOnCard()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        return Mathf.Abs((mousePosition.x - transform.position.x)) < dimImage.transform.localScale.x/2 && Mathf.Abs((mousePosition.y - transform.position.y)) < dimImage.transform.localScale.y/2;
    }

    private void PickThisCard()
    {
        GameManager.instance.pickedUpCard = this;
    }
}
