using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shovel : MonoBehaviour
{
    public GameObject shovel;

    private bool isPickedUp;

    private void Update()
    {
        if(isPickedUp)
        {
            shovel.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition)+new Vector3(0.5f,0.5f,10);
            //shovel.SetActive(false);
            if(Input.GetMouseButtonDown(0))
            {
                GameManager.instance.RemovePlant();
                isPickedUp = false;

            }
            else if(Input.GetMouseButtonDown(1))
            {
                isPickedUp = false;
            }
            
        }
        else
        {
            shovel.transform.position = transform.position;
            //shovel.SetActive(true);
            if(Input.GetMouseButtonDown(0)&&MouseOnShovel())
            {
                isPickedUp = true;
            }
        }
    }

    private bool MouseOnShovel()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        return Mathf.Abs((mousePosition.x - transform.position.x)) < 0.75 && Mathf.Abs((mousePosition.y - transform.position.y)) < 0.75;
    }
}
