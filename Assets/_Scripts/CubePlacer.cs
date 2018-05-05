using UnityEngine;
using UnityEngine.UI;

public class CubePlacer : MonoBehaviour
{
    public Vector3 startPos;
    public Vector3 endPos;
    public int clickNum;
    public Dropdown materialDrop;
    public CustomStructureConfig structureConf;
    public camInit camScript;

    private Grid grid;
    private GameObject poleBack;
    private GameObject poleFront;
    private GameObject roadCube;
    private GameObject firstCube;
    private GameObject cubeClickBack;
    private GameObject cubeClickFront;
    private float cubeClickScale = 0.75f;
    private float cubeClickScaleY = 0.3f;

    Collider col;
    bool OverUI;

    private void Awake()
    {
        grid = FindObjectOfType<Grid>();
    }

    private void Start()
    {
        clickNum = 1;
    }

    private void Update()
    {
        UICheck();
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hitInfo;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hitInfo))
            {
                PlaceCubeNear(hitInfo.point);
            }
        }
    }

    private void PlaceCubeNear(Vector3 clickPoint)
    {
        if (camScript.editing == true && OverUI == false)
        {
            var finalPosition = grid.GetNearestPointOnGrid(clickPoint);
            if (clickNum == 1)
            {
                startPos = finalPosition;
                //GameObject.CreatePrimitive(PrimitiveType.Cylinder).transform.position = finalPosition;
                cubeClickBack = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
                cubeClickBack.transform.position = new Vector3(-2.7f, finalPosition.y, finalPosition.z);
                cubeClickBack.transform.Rotate(0, 0, 90);
                cubeClickBack.transform.localScale = new Vector3(cubeClickScale, cubeClickScaleY, cubeClickScale);
                cubeClickBack.GetComponent<Renderer>().material.color = Color.gray;
                cubeClickBack.tag = "Structure";

                cubeClickFront = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
                cubeClickFront.transform.position = new Vector3(2.7f, finalPosition.y, finalPosition.z);
                cubeClickFront.transform.Rotate(0, 0, 90);
                cubeClickFront.transform.localScale = new Vector3(cubeClickScale, cubeClickScaleY, cubeClickScale);
                cubeClickFront.GetComponent<Renderer>().material.color = Color.gray;
                cubeClickFront.tag = "Structure";
                clickNum = 2;
            }
            else if (clickNum == 2)
            {
                endPos = finalPosition;
                //GameObject.CreatePrimitive(PrimitiveType.Cylinder).transform.position = finalPosition;
                cubeClickBack = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
                cubeClickBack.transform.position = new Vector3(-2.7f, finalPosition.y, finalPosition.z);
                cubeClickBack.transform.Rotate(0, 0, 90);
                cubeClickBack.transform.localScale = new Vector3(cubeClickScale, cubeClickScaleY, cubeClickScale);
                cubeClickBack.GetComponent<Renderer>().material.color = Color.gray;
                cubeClickBack.tag = "Structure";

                cubeClickFront = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
                cubeClickFront.transform.position = new Vector3(2.7f, finalPosition.y, finalPosition.z);
                cubeClickFront.transform.Rotate(0, 0, 90);
                cubeClickFront.transform.localScale = new Vector3(cubeClickScale, cubeClickScaleY, cubeClickScale);
                cubeClickFront.GetComponent<Renderer>().material.color = Color.gray;
                cubeClickFront.tag = "Structure";

                Vector3 mid = startPos + (endPos - startPos) / 2;
                //poleBack = GameObject.CreatePrimitive(PrimitiveType.Cube);
                //poleBack.transform.position = new Vector3(-3, mid.y, mid.z);
                float changeY = endPos.y - startPos.y;
                float changeZ = endPos.z - startPos.z;
                float difYZ = changeY / changeZ;
                float angle = Mathf.Atan(difYZ);
                double result = RadianToDegree(angle);

                float xScale = Mathf.Abs(endPos.x - startPos.x);
                float yScale = Mathf.Abs(endPos.y - startPos.y);
                float zScale = Mathf.Abs(endPos.z - startPos.z);
                float zScale2 = Mathf.Abs((zScale * zScale) + (yScale * yScale));
                float root = Mathf.Sqrt(zScale2);

                if (xScale == 0)
                {
                    xScale = 1;
                }
                if (yScale == 0)
                {
                    yScale = 1;
                }
                if (zScale == 0)
                {
                    zScale = yScale;
                }

                if (materialDrop.value == 0) //steel
                {
                    poleBack = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    poleBack.transform.position = new Vector3(-2.7f, mid.y, mid.z);
                    poleBack.transform.localScale = new Vector3(0.5f, 0.5f, root + 0.5f);
                    poleBack.transform.Rotate(new Vector3(-(float)result, 0, 0));
                    poleBack.tag = "Structure";
                    //poleBack.AddComponent(typeof(Rigidbody));
                    poleBack.AddComponent(typeof(CustomStructureConfig));

                    poleFront = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    poleFront.transform.position = new Vector3(2.7f, mid.y, mid.z);
                    poleFront.transform.localScale = new Vector3(0.5f, 0.5f, root + 0.5f);
                    poleFront.transform.Rotate(new Vector3(-(float)result, 0, 0));
                    poleFront.tag = "Structure";
                    //poleFront.AddComponent(typeof(Rigidbody));
                    poleFront.AddComponent(typeof(CustomStructureConfig));
                }
                else if (materialDrop.value == 1) //road
                {
                    roadCube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    roadCube.transform.position = new Vector3(0, mid.y, mid.z);
                    roadCube.transform.localScale = new Vector3(5.4f, 0.5f, root + 0.5f);
                    roadCube.transform.Rotate(new Vector3(-(float)result, 0, 0));
                    roadCube.GetComponent<Renderer>().material.color = Color.black;
                    roadCube.tag = "Structure";
                    roadCube.AddComponent(typeof(CustomStructureConfig));

                    //roadCube.AddComponent(typeof(Rigidbody));
                    //Rigidbody rb = roadCube.GetComponent<Rigidbody>();
                    //rb.useGravity = false;
                }


                clickNum = 1;
            }
            //Destroy(firstCube);
            //Destroy(cubeClickBack);
        }
    }

    double RadianToDegree(double angle)
    {
        return angle * (180.0 / Mathf.PI);
    }

    void UICheck()
    {
        OverUI = UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject();
    }
}