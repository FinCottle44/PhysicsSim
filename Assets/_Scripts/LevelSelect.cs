using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelect : MonoBehaviour {
    public int level;
    Scene currentScene;
    string sceneName;

    // Use this for initialization
    void Start () {
        currentScene = SceneManager.GetActiveScene();
        sceneName = currentScene.name;
    }
	
	// Update is called once per frame
	void Update () {

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
        }
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }

}
