using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeDesign : MonoBehaviour {
    public GameObject particle;

    // Use this for initialization
    void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		Draw ();
	}

	void Draw(){
		if (Input.GetMouseButtonDown(0)) {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray))
                Instantiate(particle, transform.position, transform.rotation);
        }
	}
}
