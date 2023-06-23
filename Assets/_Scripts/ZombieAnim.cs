using System;
using System.Collections;
using System.Collections.Generic;
using _Scripts;
using UnityEngine;
using Random = UnityEngine.Random;

public class ZombieAnim : MonoBehaviour {

    public GameObject body;

    public int timer;
    public int nextMoveTime;
    public float nextXPos;
    public float curXPos;
    public Vector3 nextRandomRot;
    public Vector3 curRot;
    public Transform head;
    public Transform bloodPrefab;
    public Transform[] bloodCircle;

    public int hp;

    private void Start() {
        bloodCircle = new Transform[hp];
        for (int i = 0; i < 10; i++) {
            bloodCircle[i] = Instantiate(bloodPrefab, transform);
            bloodCircle[i].transform.localPosition = 1f * Calc.Deg2Dir3(i * 36f);
        }
    }

    public void RefreshBlood() {
        for (int i = 0; i < hp; i++) {
            bloodCircle[i].transform.position = transform.position + 1f * Calc.Deg2Dir3(i * 360f*20/body.GetComponent<Zombie>().maxhealth + 2f * timer);
        }

        for (int i = hp; i < 10; i++) {
            bloodCircle[i].transform.position = transform.position;
        }
    }



    private void Update() {
        //if(Input.anyKeyDown) hp--;
    }
    private void FixedUpdate() {
        RefreshBlood();
        Movement();
        timer++;
    }

    private void Movement() {
        if (timer == nextMoveTime) {
            nextXPos = body.transform.position.x;
            nextMoveTime += Random.Range(100, 150);
            nextRandomRot += new Vector3(Random.Range(-90f, 90f), Random.Range(-90f, 90f), Random.Range(-90f, 90f));
        }

        curRot.ApproachRef(nextRandomRot, 32f * Vector3.one);
        curXPos.ApproachRef(nextXPos, 64f);
        
        head.transform.rotation = Quaternion.Euler(curRot);
        head.transform.position = new Vector3(curXPos, head.transform.position.y, 0f);
    }
}
