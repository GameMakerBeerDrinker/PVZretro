using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SunManager : MonoBehaviour
{
    public static SunManager instance;

    public GameObject sunPrefab;

    public TextMeshProUGUI sunText;
    private int textsun;
    public int sun;

    public bool isDay;

    private int generateCount;
    private int generatePeriod;
    private int generateTimer;

    private void Awake()
    {
        if (instance == null)
            instance = this;

        sun = 50;
        textsun = sun;
        isDay = true;
    }

    private void FixedUpdate()
    {
        GenerateSun();
        ChangeSunText();
    }

    public float sunGenerateRangeX,sunGenerateRangeYStart,sunGenerateRangeYEnd;
    private void GenerateSun()
    {
        generateTimer++;
        if (generateTimer >= generatePeriod)
        {
            //Debug.Log("generate sun");
            generateTimer = 0;

            float x = Random.Range(0, sunGenerateRangeX);
            float y = Random.Range(sunGenerateRangeYStart, sunGenerateRangeYEnd);
            Sun sun = Instantiate(sunPrefab, transform.position + Vector3.right * x, Quaternion.Euler(0, 0, 0)).GetComponent<Sun>();
            sun.fallDestination = sun.transform.position + Vector3.down * y;
            sun.isFalling = true;

            generateCount++;
            ResetGeneratePeriod();
        }
    }

    private void ResetGeneratePeriod()
    {
        int A = 10 * generateCount + 425;
        int B = Random.Range(0, 274);
        if(A<=950)
        {
            generatePeriod = A + B;
        }
        else
        {
            generatePeriod = 950 + B;
        }
    }

    private void ChangeSunText()
    {
        if (textsun<sun)
        {
            textsun++;
        }
        else if(textsun>sun)
        {
            textsun--;
        }
        sunText.text = textsun.ToString();
    }
}
