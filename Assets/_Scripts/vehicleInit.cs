using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class vehicleInit : MonoBehaviour {
    public VehicleSelect vehicleSelect;
    public Rigidbody rb;
    public bool move;
    public Vector3 com;

    float mass;
    float thrustMultiplier;
    float thrust;
    float velocityRequired = 0.4f;

    public InputField inputThrust;
    public InputField inputMass;

    Vector3 lastPos;

    // Used for initialization
    void Start()
    {
        rb = vehicleSelect.vehicle.GetComponent<Rigidbody>();
        lastPos = transform.position;
        StopPress();
    }

    void FixedUpdate() {
        getMass();
        vehStatus();
        rb.centerOfMass = com;
        calcvelocity();
    }

    // Update is called once per frame
    void Update()
    {
        if (vehicleSelect.vehicle == vehicleSelect.jeep)
        {
            Debug.Log("dab");
            rb = vehicleSelect.jeep.GetComponent<Rigidbody>();
            Debug.Log(rb.mass);
        }
        else if (vehicleSelect.vehicle == vehicleSelect.truck)
        {
            Debug.Log("dabtruck");
            rb = vehicleSelect.truck.GetComponent<Rigidbody>();
            Debug.Log(rb.mass);
        }
        //Debug.Log(vehicleSelect.vehicle);
    }

    void calcvelocity()
    {
        float vel = Vector3.Magnitude(transform.position - lastPos);
        lastPos = transform.position;
        Debug.Log(vel);
    }

    public void vehMove()
    {

        if (vehicleSelect.vehicle == vehicleSelect.jeep)
        {
            float force = mass * (velocityRequired / Time.fixedDeltaTime);
            Vector3 f = force * transform.forward;

            rb.AddForce(f, ForceMode.Force);
            //Debug.Log("jeep");
        }
        else if (vehicleSelect.vehicle == vehicleSelect.truck)
        {
            //Debug.Log("Truck");
            float force = mass * (velocityRequired / Time.fixedDeltaTime);
            Vector3 f = force * -(transform.right); //Truck is weirdly rotated, this acts as "forward" when it is really left, see "if tag == jeep statement".

            rb.AddForce(f, ForceMode.Force);
        }
    }

    public void vehStatus()
    {
        if (move == true)
        {
            vehMove();
        }
    }

    public void StartPress()
    {
        move = true;
        //rb.isKinematic = false;

        Rigidbody[] rbs = vehicleSelect.vehicle.GetComponentsInChildren<Rigidbody>();
        foreach (Rigidbody rb in rbs) 
        {
            rb.isKinematic = false;
        }
    }

    public void StopPress()
    {
        move = false;
        //rb.isKinematic = true;

        Rigidbody[] rbs = vehicleSelect.vehicle.GetComponentsInChildren<Rigidbody>();
        foreach (Rigidbody rb in rbs)
        {
            rb.isKinematic = true;
        }
    }

    public void getMass()
    {
        if (inputMass.text == "")
        {
            if (vehicleSelect.vehicle == vehicleSelect.jeep)
            {
                mass = 1500;
            }
            else if (vehicleSelect.vehicle == vehicleSelect.truck)
            {
                mass = 5000;
            }
        }
        else
        {
            mass = float.Parse(inputMass.text);
        }
        rb.mass = mass;
    }

    public void Reset()
    {
        if (vehicleSelect.vehicle.tag == "Jeep")
        {
            vehicleSelect.vehicle.transform.position = new Vector3(0.0311477f, 22.51f, -26.58f);
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
        else if (vehicleSelect.vehicle.tag == "Truck")
        {
            vehicleSelect.vehicle.transform.position = new Vector3(-0.0006133558f, 24.21959f, 23.79094f);
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
    }
}
