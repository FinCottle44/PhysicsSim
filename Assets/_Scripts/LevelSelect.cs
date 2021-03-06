﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelect : MonoBehaviour {
    //public int level;
    //public GameObject canvasDefault;
    public GameObject canvasDefault;
    public GameObject canvasBD;

    Scene currentScene;
    string sceneName;

    // Use this for initialization
    void Start () {
        currentScene = SceneManager.GetActiveScene();
        sceneName = currentScene.name;
    }
	
	// Update is called once per frame
	void Update () {
		currentScene = SceneManager.GetActiveScene();
		sceneName = currentScene.name;
	}

    void OnGUI()
    {
        if (sceneName == "MainMenu")
        {

            //This displays a Button on the screen at position (20,30), width 150 and height 50. The button’s text reads the last parameter. Press this for the SceneManager to load the Scene.
            if (GUI.Button(new Rect(Screen.width / 2 - 85, Screen.height / 2, 170, 40), "Level 2"))
            {
                //The SceneManager loads your new Scene as a single Scene (not overlapping). This is Single mode.
                SceneManager.LoadScene("Level02", LoadSceneMode.Single);
            }

            //Whereas pressing this Button loads the Additive Scene.
            if (GUI.Button(new Rect(Screen.width / 2 - 85, Screen.height / 2 - 50, 170, 40), "Level 1"))
            {
                //SceneManager loads your new Scene as an extra Scene (overlapping the other). This is Additive mode.
                SceneManager.LoadScene("Level01", LoadSceneMode.Single);
            }

			//Whereas pressing this Button loads the Additive Scene.
			if (GUI.Button(new Rect(Screen.width / 2 - 85, Screen.height / 2 + 50, 170, 40), "Level 3"))
			{
				//SceneManager loads your new Scene as an extra Scene (overlapping the other). This is Additive mode.
				SceneManager.LoadScene("Level03", LoadSceneMode.Single);
            }

			if (GUI.Button(new Rect(Screen.width / 2 - 85, Screen.height / 2 + 100, 170, 40), "Level 4"))
			{
				//SceneManager loads your new Scene as an extra Scene (overlapping the other). This is Additive mode.
				SceneManager.LoadScene("Level04", LoadSceneMode.Single);
            }

            if (GUI.Button(new Rect(Screen.width / 2 - 85, Screen.height / 2 + 150, 170, 40), "Make your own!"))
            {
                //SceneManager loads your new Scene as an extra Scene (overlapping the other). This is Additive mode.
                SceneManager.LoadScene("BridgeDesign", LoadSceneMode.Single);
            }
        }
        //if (sceneName == "BridgeDesign")
        //{
        //   canvasBD.SetActive(true);
        //   canvasDefault.SetActive(false);
        //}

    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }

}
