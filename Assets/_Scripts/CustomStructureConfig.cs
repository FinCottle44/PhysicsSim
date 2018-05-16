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
        if (collision.collider.tag.Contains("Structure"))
        {
            Collider[] col2 = collision.gameObject.GetComponents<Collider>();
            if (col2 == null)
            {
                col2 = collision.gameObject.GetComponentsInChildren<Collider>();
            }

            for (int i = 0; i < col2.Length; i++)
            {

                Physics.IgnoreCollision(col2[i], col);
            }
        }
    }
}
