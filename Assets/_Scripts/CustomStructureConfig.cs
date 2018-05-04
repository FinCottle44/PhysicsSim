using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomStructureConfig : MonoBehaviour {
    Collider col;

	// Use this for initialization
	void Start () {
        col = GetComponent<Collider>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Structure")
        {
            col.enabled = false;
        }
    }
}
