using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class worldInit : MonoBehaviour {
	public GameObject ocean;
	public Text btnDrain;
	public Canvas canvas;
	bool blDrain;

	// Use this for initialization
	void Start () {
		canvas.enabled = true;
	}
	
	// Update is called once per frame
	void Update () {
		drain ();
	}

	public void DrainStatus() {
		blDrain = !blDrain;
	}

	void drain() {
		if (blDrain == true) {
		//if (1 == 1) {
			ocean.transform.Translate (Vector3.down * Time.deltaTime);
			btnDrain.text = "Stop Draining";
			//Debug.Log (ocean.transform.position.y);
		}
		else {
			btnDrain.text = "Drain Water";
		}
	}
}
