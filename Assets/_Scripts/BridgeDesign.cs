using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeDesign : MonoBehaviour {
    public GameObject particle;
    public Texture crosshair;
    public Camera cam;
    public Vector2 screenPos;
    public camInit camScript;
    public GameObject canvasBD;
    public GameObject canvasDefault;
    public Grid gridScript;

    bool isClicking;

    // Use this for initialization
    void Start () {
        cam = GetComponent<Camera>();
        isClicking = false;
    }
	
	// Update is called once per frame
	void Update () {
        Draw ();
	}

    void Draw()
    {
        if (Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray))
                isClicking = true;
                screenPos = Input.mousePosition;
                //Cursor.visible = false;
        }
        else if (Input.GetMouseButton(0) == false)
        {
            isClicking = false;
        }
    }

    void placeSupport()
    {
        //find start point, end point, middle point (end - start), and make cube satisfy all points
    }


    void OnGUI()
    {

        if (isClicking)
        {
            //GUI.DrawTexture(new Rect(Input.mousePosition, new Vector2(100, 100)), crosshair);
            GUI.DrawTexture(new Rect(Input.mousePosition.x - 10, Screen.height - Input.mousePosition.y - 10, 25, 25), crosshair);
            //click = false;
        }
    }

    public void Go()
    {
        camScript.editing = false;
        gridScript.DestroyGrid();
        canvasBD.SetActive(false);
        canvasDefault.SetActive(true);
    }

    public void Clear()
    {
        GameObject[] structure = GameObject.FindGameObjectsWithTag("Structure");
        for (int i = 0; i < structure.Length; i++)
        {
            GameObject block = structure[i];
            Destroy(block);
        }
    }

    public void Edit()
    {
        camScript.editing = true;
        camScript.SelectCam();
    }
}
