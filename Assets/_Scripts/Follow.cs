using System;
using System.Collections;
using System.Collections.Generic;
using _Scripts;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Follow : MonoBehaviour {
    public Transform parent;
    public float speed;

    public float curX;
    public Vector3 curRot;

    public bool rotFollow;
    public bool ySwing;

    public float oriY;
    private void FixedUpdate() {
        curX.ApproachRef(parent.transform.position.x, speed);
        curRot.ApproachRef(parent.transform.rotation.eulerAngles, speed * Vector3.one);

        if (ySwing)
            transform.position = new Vector3(curX,
                parent.transform.position.y + oriY + 0.1f * Mathf.Sin(Time.time * 5f), 0f);
        else transform.position = new Vector3(curX, parent.transform.position.y, 0f);
        if(rotFollow) transform.rotation = Quaternion.Euler(curRot);
    }
}