using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardForChoose : MonoBehaviour
{
    public GameObject card;

    private bool isMoving;
    private bool inStore=true;

    private Vector3 destination;
    private Vector3 rawPosition;


    private void Start()
    {
        rawPosition = transform.position;
        destination = rawPosition;
    }

    private void Update()
    {
        ChooseAndUndo();

    }

    private void FixedUpdate()
    {
        Move();
    }

    public float speed;

    private void Move()
    {
        Vector3 moveDirection = (destination - transform.position).normalized;
        if((transform.position-destination).magnitude>0.05f)
            transform.position += moveDirection * speed*Time.fixedDeltaTime;
        else
        {
            isMoving = false;
        }
    }

    private void ChooseAndUndo()
    {
        if (Input.GetMouseButtonDown(0) && !isMoving && MouseOnCard())
        {
            if (inStore&&Backpack.instance.cardCount<Backpack.instance.maxCount)
            {
                isMoving = true;
                inStore = false;
                destination = Backpack.instance.transform.position + new Vector3(Backpack.instance.cardCount * Store.instance.width, 0, 0);
                Backpack.instance.cardCount++;
            }
            else if (!inStore)
            {
                isMoving = true;
                inStore = true;
                destination = rawPosition;
                Backpack.instance.cardCount--;
            }
            
        }
    }

    private bool MouseOnCard()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        return Mathf.Abs(mousePosition.x - transform.position.x) <= Store.instance.width/2 && Mathf.Abs(mousePosition.y - transform.position.y) <= Store.instance.height/2;
    }
}
