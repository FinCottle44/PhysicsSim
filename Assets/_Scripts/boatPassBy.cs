using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class boatPassBy : MonoBehaviour {
    public Rigidbody rb;
	public GameObject ocean;
	public GameObject road;
	public bool hjDestroyed = false;
	public GameObject RopeRoad;
	public GameObject RopeGround;
	public float distance = 10f;
	public Rigidbody GroundL;

	Scene currentScene;
	string sceneName;
	HingeJoint hj;
    Vector3 lastPos;
    float mass;
    float velocityRequired = 0.01f;

    // Use this for initialization
    void Start () {
        addForce();
        rb = GetComponent<Rigidbody>();
        mass = rb.mass;
		currentScene = SceneManager.GetActiveScene();
		sceneName = currentScene.name;

	}
	
	// Update is called once per frame
	void FixedUpdate () {
        addForce();

        calcvelocity();
        //Debug.Log(rb.velocity);
    }

    void addForce()
    {
		//Debug.Log (sceneName);
		if (sceneName == "Level01" || sceneName == "Level02" || sceneName == "Level03") {

			if (rb.transform.position.z <= -175) {
				float oceanPosY = ocean.transform.position.y;
				float newPos = oceanPosY - 0.73f;
				rb.transform.position = new Vector3 (-200, newPos, 100);
				rb.velocity = new Vector3 (0, 0, 0);
			} else {
				//float force = 2000f;
				float force = mass * (velocityRequired / Time.fixedDeltaTime);
				Vector3 f = force * -(transform.right); //go "left" dodgey positioning, this is forward.
				float oceanPosY = ocean.transform.position.y;
				float newPos = oceanPosY - 0.73f;
				float boatPosX = rb.transform.position.x;
				float boatPosZ = rb.transform.position.z;
				rb.transform.position = new Vector3 (boatPosX, newPos, boatPosZ);

				rb.AddForce (f, ForceMode.Force);
			}
		} 
		else if (sceneName == "Level04") {
			float force = mass * (velocityRequired / Time.fixedDeltaTime);
			Vector3 f = force * -(transform.right); //go "left" dodgey positioning, this is forward.

			rb.AddForce (f, ForceMode.Force);
			if (rb.position.x > -20 && hjDestroyed == false) {
				hj = road.GetComponent<HingeJoint> ();
				Destroy (hj);
				hjDestroyed = true;
			}
			float ropelength = Vector3.Distance (RopeRoad.transform.position, RopeGround.transform.position);
			Debug.Log (ropelength);
			if (hjDestroyed == true) {
				if (ropelength < distance) {
					Debug.Log ("Rope too long");
					FixedJoint fJoint = RopeRoad.AddComponent<FixedJoint> ();
					fJoint.connectedBody = GroundL;
				}
			}
		}
    }

    void calcvelocity()
    {
        float vel = Vector3.Magnitude(transform.position - lastPos);
        lastPos = transform.position;
        //Debug.Log(vel);
    }
}
