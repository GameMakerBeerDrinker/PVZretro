using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Backpack : MonoBehaviour
{
    public static Backpack instance;
    public int cardCount;
    public int maxCount = 5;

    //public List<>

    private void Awake()
    {
        if(instance==null)
        {
            instance = this;
        }

    }

    
}
