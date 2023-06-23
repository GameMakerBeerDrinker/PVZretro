using System.Collections;
using System.Collections.Generic;
using _Scripts;
using UnityEngine;
using Zombies;

public class ZombieManager : MonoBehaviour
{
    public static ZombieManager instance;
    public GameObject victoryReward;

    public Zombie[] zombies;

    public Level currentLevel;
    private ZombieWave[] zombieWaves;

    [HideInInspector]
    public List<Zombie> aliveZombies;
    [HideInInspector]
    public List<Zombie> currentWaveZombies;

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

    /*[ExecuteInEditMode]
    private void SetZombie()
    {
        
    }*/

    private void NewGame()
    {
        /*for (int wave = 0; wave < currentLevel.zombieWaves.Length; wave++)
        {
            int zombieCount = 0;
            for (int zombieName = 0; zombieName < currentLevel.zombieWaves[wave].zombieNames.Length; zombieName++)
                for (int zombieNum = 0; zombieNum < zombieWaves[wave].zombieNum[zombieName]; zombieNum++)
                {
                    currentLevel.zombieWaves[wave].zombies[zombieCount] = currentLevel.zombieWaves[wave].zombieNames[zombieName];
                    zombieCount++;
                }
        }*/
        
        zombieWaves = currentLevel.zombieWaves;
        refreshTimer = 0;
        waveNum = 0;
    }

    private void FixedUpdate()
    {
        refreshTimer++;
        //���㱾����ʬʣ��Ѫ��
        currentTotalHealth = 0;
        foreach(Zombie zombie in currentWaveZombies)
        {
            currentTotalHealth += zombie.currentHealth;
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
            victoryReward.gameObject.SetActive(true);
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
            Vector3 linePosition = new Vector3(0, -generateLine * GameManager.instance.tilemap.cellSize.x, 0);
            Vector3 generatePosition = transform.position + linePosition + positionCorrect;


            Zombie newZombie = Instantiate(zombies[(int)zombieWaves[waveNum].zombies[i]], generatePosition, Quaternion.Euler(0, 0, 0));
            var anim = ZombieAnimManager.Manager.zombieAnimPool.Get();
            newZombie.anim = anim;
            anim.body = newZombie.gameObject;
            anim.SetArmor(newZombie.zombieName);
            
            
            //����һ����ʬʱ�����������ʬ�б��Թ����ּ��
            aliveZombies.Add(newZombie);
            //����һ����ʬʱ���������뱾����ʬ�б�������ñ�����ʬ��ǰѪ��
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
