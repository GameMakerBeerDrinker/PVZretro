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
        //计算本波僵尸剩余血量
        currentTotalHealth = 0;
        foreach(Zombie zombie in currentWaveZombies)
        {
            currentTotalHealth += zombie.currentHealth;
        }

        //当时间到达最长波长，或当前波僵尸血量降至 刷新因子*总血量 以下，生成下一波僵尸
        if (waveNum < zombieWaves.Length && (refreshTimer >= zombieWaves[waveNum].tmax || currentTotalHealth < zombieWaves[waveNum].refreshFactor * waveTotalHealth))  
        {
            Debug.Log("refresh zombie");
            waveNum++;
            RefreshZombie();
        }


        //最后一波僵尸被消灭，过关。
        if(waveNum>=zombieWaves.Length&&aliveZombies.Count==0)
        {
            victoryReward.gameObject.SetActive(true);
        }
    }

    private void RefreshZombie()
    {
        waveTotalHealth = 0;

        int[] lineHealth=new int[5];

        //洗牌算法打乱本波僵尸排列顺序
        Shuffle(zombieWaves[waveNum].zombies);

        List<int> minHealthLine = new List<int>();
        for (int i=0; i<zombieWaves[waveNum].zombies.Length;i++)
        {
            //生成一个僵尸时，选择本波各行僵尸血量数组中最小的行，如有多个最小的行，随机选择一行

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
            
            
            //生成一个僵尸时，把它加入存活僵尸列表，以供射手检测
            aliveZombies.Add(newZombie);
            //生成一个僵尸时，把它加入本波僵尸列表，用来获得本波僵尸当前血量
            currentWaveZombies.Add(newZombie);
            //生成一个僵尸时，把它的血量加入本波各行僵尸血量数组的对应行血量
            lineHealth[generateLine] += newZombie.GetComponent<Zombie>().maxhealth;
            //计算本波僵尸总血量
            waveTotalHealth += newZombie.GetComponent<Zombie>().maxhealth;
        }

        refreshTimer = 0;
    }

    //洗牌算法
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
