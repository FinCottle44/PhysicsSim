using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class vehicleInit : MonoBehaviour {
    public VehicleSelect vehicleSelect;
    public Rigidbody rb;
    public bool move;
    public Vector3 com;

    float mass;
    float thrustMultiplier;
    float thrust;
    float velocityRequired = 0.4f;

    Scene currentScene;
    string sceneName;

    public InputField inputThrust;
    public InputField inputMass;

    Vector3 lastPos;

    // Used for initialization
    void Start()
    {
        rb = vehicleSelect.vehicle.GetComponent<Rigidbody>();
        lastPos = transform.position;
        StopPress();

        Rigidbody[] rbs = vehicleSelect.vehicle.GetComponentsInChildren<Rigidbody>();
        foreach (Rigidbody rb in rbs)
        {
            rb.isKinematic = true;
        }
        currentScene = SceneManager.GetActiveScene();
        sceneName = currentScene.name;

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
            //Debug.Log("dab");
            rb = vehicleSelect.jeep.GetComponent<Rigidbody>();
            //Debug.Log(rb.mass);
        }
        else if (vehicleSelect.vehicle == vehicleSelect.truck)
        {
            //Debug.Log("dabtruck");
            rb = vehicleSelect.truck.GetComponent<Rigidbody>();
            //Debug.Log(rb.mass);
        }
        //Debug.Log(vehicleSelect.vehicle);
    }

    void calcvelocity()
    {
        float vel = Vector3.Magnitude(transform.position - lastPos);
        lastPos = transform.position;
        //Debug.Log(vel);
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
        else
        {
            Rigidbody[] rbs = vehicleSelect.vehicle.GetComponentsInChildren<Rigidbody>();
            foreach (Rigidbody rb in rbs)
            {
                rb.velocity = new Vector3(0, 0, 0);
                rb.angularVelocity = new Vector3(0, 0, 0);
            }
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
            rb.velocity = new Vector3(0, 0, 0);
            rb.angularVelocity = new Vector3(0, 0, 0);

        }
        if (sceneName == "BridgeDesign")
        {
            foreach (Rigidbody rb in rbs)
            {
                rb.velocity = new Vector3(0, 0, 0);
                rb.angularVelocity = new Vector3(0, 0, 0);
                rb.isKinematic = true;
            }
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
            vehicleSelect.vehicle.transform.position = new Vector3(0.047593f, 21.37353f, -21.87712f);
            StopPress();
        }
        else if (vehicleSelect.vehicle.tag == "Truck")
        {
            vehicleSelect.vehicle.transform.position = new Vector3(-0.0006133558f, 24.21959f, 23.79094f);
            StopPress();
        }
    }

    public void OnCollisionEnter(Collision col)
    {
        //StopPress();
        //Debug.Log("stopped");
    }
}
