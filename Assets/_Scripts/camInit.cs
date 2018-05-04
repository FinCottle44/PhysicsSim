using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class camInit : MonoBehaviour
{
    public float dragSpeed = 2;
	public LevelSelect lvlSelect;
    private Vector3 dragOrigin;
    public Dropdown ddCam;
    public Toggle togMoveCam;
    
    string ddCamOption;
    int ddCamValue;
	Scene currentScene;
	string sceneName;
	bool editing;

    public float speed = 1f;
    private float X;
    private float Y;

	public Camera cam1;
	public Camera cam2;
	public Camera cam2d;

    void Update()
    {
        Pan();
        Zoom();
		SelectCam();
		currentScene = SceneManager.GetActiveScene();
		sceneName = currentScene.name;
    }
	void Start()
	{
		currentScene = SceneManager.GetActiveScene();
		sceneName = currentScene.name;
		SelectCam ();
		editing = true;
	}

    void Pan()
    {
        if (togMoveCam.isOn == true)
        {
            if (Input.GetMouseButton(0))
            {
                transform.Rotate(new Vector3(Input.GetAxis("Mouse Y") * speed, -Input.GetAxis("Mouse X") * speed, 0));
                X = transform.rotation.eulerAngles.x;
                Y = transform.rotation.eulerAngles.y;
                transform.rotation = Quaternion.Euler(X, Y, 0);
            }
        }
    }

    void Zoom()
    {
        float minFov = 15f;
        float maxFov = 90f;
        float sensitivity = -10f; //negative sens inverts direction of scroll
        float fov = Camera.main.fieldOfView;
        fov += Input.GetAxis("Mouse ScrollWheel") * sensitivity;
		fov = Mathf.Clamp(fov, minFov, maxFov);
        Camera.main.fieldOfView = fov;
    }

    public void SelectCam()
    {
        ddCamValue = ddCam.value;
        ddCamOption = ddCam.options[ddCamValue].text;
		if (sceneName == "BridgeDesign" && editing == true) {
			cam1.enabled = false;
			cam2.enabled = false;
			cam2d.enabled = true;
			cam2d.gameObject.SetActive (true);
		} else {
			if (ddCamOption == "Camera 1") {
				cam1.enabled = true;
				cam2.enabled = false;
				//cam2d.enabled = false;
				cam1.gameObject.SetActive (true);
			} if (ddCamOption == "Camera 2") {
				cam1.enabled = false;
				cam2.enabled = true;
				//cam2d.enabled = false;
				cam2.gameObject.SetActive (true);
			}
		}

    }
}
