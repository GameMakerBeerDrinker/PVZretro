using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieManager : MonoBehaviour
{
    public static ZombieManager instance;
    public GameObject victoryReward;

    public GameObject[] zombies;

    public Level currentLevel;
    private ZombieWave[] zombieWaves;

    [HideInInspector]
    public List<GameObject> aliveZombies;
    [HideInInspector]
    public List<GameObject> currentWaveZombies;

    private int waveNum;
    private int waveTotalHealth;
    private int currentTotalHealth;
    private int refreshTimer;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        NewGame();
    }

    private void NewGame()
    {
        zombieWaves = currentLevel.zombieWaves;
        refreshTimer = 0;
        waveNum = 0;
    }

    private void FixedUpdate()
    {
        refreshTimer++;
        //���㱾����ʬʣ��Ѫ��
        currentTotalHealth = 0;
        foreach(GameObject zombie in currentWaveZombies)
        {
            currentTotalHealth += zombie.GetComponent<Zombie>().currentHealth;
        }

        //��ʱ�䵽�����������ǰ����ʬѪ������ ˢ������*��Ѫ�� ���£�������һ����ʬ
        if (waveNum < zombieWaves.Length && (refreshTimer >= zombieWaves[waveNum].tmax || currentTotalHealth < zombieWaves[waveNum].refreshFactor * waveTotalHealth))  
        {
            Debug.Log("refresh zombie");
            waveNum++;
            RefreshZombie();
        }


        //���һ����ʬ�����𣬹��ء�
        if(waveNum>=zombieWaves.Length&&aliveZombies.Count==0)
        {
            victoryReward.SetActive(true);
        }
    }

    private void RefreshZombie()
    {
        waveTotalHealth = 0;

        int[] lineHealth=new int[5];

        //ϴ���㷨���ұ�����ʬ����˳��
        Shuffle(zombieWaves[waveNum].zombies);

        List<int> minHealthLine = new List<int>();
        for (int i=0; i<zombieWaves[waveNum].zombies.Length;i++)
        {
            //����һ����ʬʱ��ѡ�񱾲����н�ʬѪ����������С���У����ж����С���У����ѡ��һ��

            int count = 0;
            minHealthLine.Clear();
            minHealthLine.Add(0);
            for (int j = 1; j < 5; j++) 
            {
                //Debug.Log(minHealthLine[0]);
                if(lineHealth[j]<lineHealth[minHealthLine[0]])
                {
                    count = 0;
                    minHealthLine.Clear();
                    minHealthLine.Add(j);
                }
                else if(lineHealth[j]==lineHealth[minHealthLine[0]])
                {
                    count++;
                    minHealthLine.Add(j);
                }
            }
            int generateLine = minHealthLine[Random.Range(0,count)];

            Vector3 positionCorrect = new Vector3(Random.Range(0, 2f), 0, 0);
            Vector3 linePosition = new Vector3(0, -generateLine * GameManager.instance.tilemap.cellSize.y, 0);
            Vector3 generatePosition = transform.position + linePosition + positionCorrect;


            GameObject newZombie = Instantiate(zombies[(int)zombieWaves[waveNum].zombies[i]], generatePosition, Quaternion.Euler(0, 0, 0));
            //����һ����ʬʱ�����������ʬ�б����Թ����ּ��
            aliveZombies.Add(newZombie);
            //����һ����ʬʱ���������뱾����ʬ�б���������ñ�����ʬ��ǰѪ��
            currentWaveZombies.Add(newZombie);
            //����һ����ʬʱ��������Ѫ�����뱾�����н�ʬѪ������Ķ�Ӧ��Ѫ��
            lineHealth[generateLine] += newZombie.GetComponent<Zombie>().maxhealth;
            //���㱾����ʬ��Ѫ��
            waveTotalHealth += newZombie.GetComponent<Zombie>().maxhealth;
        }

        refreshTimer = 0;
    }

    //ϴ���㷨
    private void Shuffle<T>(T[] targetList)
    {
        for (int i = targetList.Length - 1; i > 0; i--)
        {
            int exchange = Random.Range(0, i + 1);
            T temp = targetList[i];
            targetList[i] = targetList[exchange];
            targetList[exchange] = temp;
        }
    }
}