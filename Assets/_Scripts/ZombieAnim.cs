using System;
using System.Collections;
using System.Collections.Generic;
using _Scripts;
using UnityEngine;
using Zombies;
using Random = UnityEngine.Random;

public class ZombieAnim : MonoBehaviour {
    public MeshRenderer[] meshes;
    public GameObject body;
    public SpriteRenderer armor;
    public Sprite[] armorSprites;

    public int timer;
    public int nextMoveTime;
    public float nextXPos;
    public float curXPos;
    public Vector3 nextRandomRot;
    public Vector3 curRot;
    public Transform head;
    public Transform bloodPrefab;
    public Transform[] bloodCircle;

    public int maxHp;
    public int curHp;

    private void Start() {
        foreach (var mesh in meshes) {
            mesh.sortingLayerName = "Zombie";
        }

        bloodCircle = new Transform[maxHp];
        for (int i = 0; i < maxHp; i++) {
            bloodCircle[i] = Instantiate(bloodPrefab, transform);
            bloodCircle[i].transform.localPosition = 1f * Calc.Deg2Dir3(i * 360f / maxHp);
        }
    }


    public void SetArmor(ZombieName name) {
        armor.sprite = armorSprites[(int)name];
    }
    public void RefreshBlood() {
        for (int i = 0; i < curHp; i++) {
            bloodCircle[i].transform.position = transform.position + 1f * Calc.Deg2Dir3(i * 360f / maxHp + 2f * timer);
        }

        for (int i = curHp; i < maxHp; i++) {
            bloodCircle[i].transform.position = transform.position;
        }
    }
    
    private void FixedUpdate() {
        //RefreshBlood();
        Movement();
        timer++;
    }

    private void Movement() {
        if (body == null) {
            ZombieAnimManager.Manager.zombieAnimPool.Release(this);
        }
        
        if (timer == nextMoveTime) {
            nextXPos = body.transform.position.x;
            nextMoveTime += Random.Range(100, 150);
            nextRandomRot += new Vector3(Random.Range(-90f, 90f), Random.Range(-90f, 90f), Random.Range(-90f, 90f));
        }

        curRot.ApproachRef(nextRandomRot, 32f * Vector3.one);
        curXPos.ApproachRef(nextXPos, 64f);
        
        head.transform.rotation = Quaternion.Euler(curRot);
        head.transform.position = new Vector3(curXPos, body.transform.position.y, 0f);
    }
}
