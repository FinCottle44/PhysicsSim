using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boatPass : MonoBehaviour {
    public Rigidbody rb;

    Vector3 lastPos;
    float mass;
    float velocityRequired = 0.01f;

    // Use this for initialization
    void Start () {
        addForce();
        rb = GetComponent<Rigidbody>();
        mass = rb.mass;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        addForce();

        calcvelocity();
        //Debug.Log(rb.velocity);
    }

    void addForce()
    {
        if (rb.transform.position.z <= -175)
        {
            rb.transform.position = new Vector3(-115, 15.27f, 100);
            rb.velocity = new Vector3(0,0,0);
        }
        else
        {
            //float force = 2000f;
            float force = mass * (velocityRequired / Time.fixedDeltaTime);
            Vector3 f = force * -(transform.right); //go "left" dodgey positioning, this is forward.

            rb.AddForce(f, ForceMode.Force);
        }
    }

    void calcvelocity()
    {
        float vel = Vector3.Magnitude(transform.position - lastPos);
        lastPos = transform.position;
        //Debug.Log(vel);
    }
}
