using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSLimit : MonoBehaviour
{
    private void Awake()
    {
        Application.targetFrameRate = 100;
    }
}
