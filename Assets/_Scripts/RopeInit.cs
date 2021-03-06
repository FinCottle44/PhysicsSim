﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeInit : MonoBehaviour {
    public float radius = 50.0F;
    public float power = 1000.0F;

    GameObject go;
    GameObject goConnected;
    GameObject anchor;
    Rigidbody rb;
    HingeJoint hj;
    FixedJoint fj;
    Collider col;
	Cable_Procedural_Simple cabScript;


    // Use this for initialization
    void Start () {
        go = transform.gameObject;
        hj = go.GetComponent<HingeJoint>();
        fj = go.GetComponent<FixedJoint>();
        rb = go.GetComponent<Rigidbody>();
        col = go.GetComponent<Collider>();
		cabScript = go.GetComponent<Cable_Procedural_Simple>();
        anchor = fj.connectedBody.gameObject;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		
	}

    void OnJointBreak(float breakForce)
    {
        Vector3 explosionPos = go.transform.position;

        Debug.Log("dab!, force: " + breakForce);
        Debug.Log(anchor);
        Destroy(hj);
		rb.useGravity = false;
        //rb.AddExplosionForce(power, explosionPos, radius, 300.0F);
        rb.transform.LookAt(anchor.transform.position);
        rb.AddRelativeForce(Vector3.forward * 2000);
		cabScript.swayMultiplier = 10;
		rb.useGravity = true;
        col.enabled = true;
    }
}
