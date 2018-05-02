using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeDesign : MonoBehaviour {
    public GameObject particle;
    public Texture crosshair;
    public Camera cam;
    public Vector2 screenPos;

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
            GUI.DrawTexture(new Rect(Input.mousePosition.x - 25, Screen.height - Input.mousePosition.y - 25, 50, 50), crosshair);
            //click = false;
        }
    }
}
