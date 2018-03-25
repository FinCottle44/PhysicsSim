using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomGravity : MonoBehaviour {
    public float gravMultipler;
    public Slider gravSlider;
    float yGrav;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void FixedUpdate () {
        gravMultipler = gravSlider.value;
        yGrav = -9.81f * gravMultipler;
        Physics.gravity = new Vector3(0, yGrav, 0);
	}
}
