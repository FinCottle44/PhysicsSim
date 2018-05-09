using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic; // generic collection types (list in this case)

public class CubePlacerBackup : MonoBehaviour
{
    public Vector3 startPos;
    public Vector3 endPos;
    public int clickNum;
    public Dropdown materialDrop;
    public CustomStructureConfig structureConf;
    public camInit camScript;
    public List<Vector3> history;

    private Grid grid;
    private GameObject poleBack;
    private GameObject poleFront;
    private GameObject roadCube;
    private GameObject firstCube;
    private GameObject cubeClickBack;
    private GameObject cubeClickFront;
    private float cubeClickScale = 0.75f;
    private float cubeClickScaleY = 0.3f;
    private Collider col;
    private bool OverUI;
    private bool overlap;
    private bool midCreate;
    private bool poleExists;

    private void Awake()
    {
        grid = FindObjectOfType<Grid>();
    }

    private void Start()
    {
        clickNum = 1;
        overlap = false;
        midCreate = false;
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
                if (history.Count > 0)
                {
                    JointCheck(startPos);
                }
                if (overlap == false && midCreate == false)
                {
                    history.Add(startPos);
                    cubeClickBack = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
                    cubeClickBack.transform.position = new Vector3(-2.7f, finalPosition.y, finalPosition.z);
                    cubeClickBack.transform.Rotate(0, 0, 90);
                    cubeClickBack.transform.localScale = new Vector3(cubeClickScale, cubeClickScaleY, cubeClickScale);
                    cubeClickBack.GetComponent<Renderer>().material.color = Color.green;
                    cubeClickBack.AddComponent(typeof(CustomStructureConfig));
                    cubeClickBack.tag = "Structure";
                    cubeClickBack.name = "Pivot";

                    cubeClickFront = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
                    cubeClickFront.transform.position = new Vector3(2.7f, finalPosition.y, finalPosition.z);
                    cubeClickFront.transform.Rotate(0, 0, 90);
                    cubeClickFront.transform.localScale = new Vector3(cubeClickScale, cubeClickScaleY, cubeClickScale);
                    cubeClickFront.GetComponent<Renderer>().material.color = Color.green;
                    cubeClickFront.AddComponent(typeof(CustomStructureConfig));
                    cubeClickFront.tag = "Structure";
                    cubeClickFront.name = "Pivot";
                    clickNum = 2;
                }
            }
            else if (clickNum == 2)
            {
                endPos = finalPosition;
                if (history.Count > 0)
                {
                    JointCheck(endPos);
                }
                if (endPos != history[history.Count - 1])
                {
                    history.Add(endPos);
                    if (overlap == false)
                    {
                        cubeClickBack = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
                        cubeClickBack.transform.position = new Vector3(-2.7f, finalPosition.y, finalPosition.z);
                        cubeClickBack.transform.Rotate(0, 0, 90);
                        cubeClickBack.transform.localScale = new Vector3(cubeClickScale, cubeClickScaleY, cubeClickScale);
                        cubeClickBack.GetComponent<Renderer>().material.color = Color.blue;
                        cubeClickBack.AddComponent(typeof(CustomStructureConfig));
                        cubeClickBack.tag = "Structure";
                        cubeClickBack.name = "Pivot";

                        cubeClickFront = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
                        cubeClickFront.transform.position = new Vector3(2.7f, finalPosition.y, finalPosition.z);
                        cubeClickFront.transform.Rotate(0, 0, 90);
                        cubeClickFront.transform.localScale = new Vector3(cubeClickScale, cubeClickScaleY, cubeClickScale);
                        cubeClickFront.GetComponent<Renderer>().material.color = Color.blue;
                        cubeClickFront.AddComponent(typeof(CustomStructureConfig));
                        cubeClickFront.tag = "Structure";
                        cubeClickFront.name = "Pivot";
                    }
                    Vector3 mid = startPos + (endPos - startPos) / 2;
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
                        poleBack.name = "steelBack";
                        poleBack.transform.position = new Vector3(-2.7f, mid.y, mid.z);
                        poleBack.transform.localScale = new Vector3(0.5f, 0.5f, root + 0.5f);
                        poleBack.transform.Rotate(new Vector3(-(float)result, 0, 0));
                        poleBack.tag = "Structure";
                        //poleBack.AddComponent(typeof(Rigidbody));
                        poleBack.AddComponent(typeof(CustomStructureConfig));

                        poleFront = GameObject.CreatePrimitive(PrimitiveType.Cube);
                        poleFront.name = "steelFront";
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
                        //roadCube.AddComponent(typeof(CustomStructureConfig));

                        roadCube.AddComponent(typeof(Rigidbody));
                        Rigidbody rb = roadCube.GetComponent<Rigidbody>();
                        rb.useGravity = false;
                    }
                    clickNum = 1;
                }
                else
                {
                    clickNum = 1;
                }
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

    void JointCheck(Vector3 position) //used to add horizontal supports and joint positions   
    {
        for (int i = 0; i < history.Count; i++)
        {
            Vector3 checkPos = history[i];
            if (checkPos.y == position.y && checkPos.z == position.z)
            {
                overlap = true;
                if (checkPos.y != 20)
                {
                    PoleCheck(new Vector3(0, position.y, position.z));
                    //horizontal support
                    if (poleExists == false)
                    {
                        GameObject pole = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
                        pole.transform.Rotate(0, 0, 90);
                        pole.transform.localScale = new Vector3(0.5f, 2.4f, 0.5f);
                        pole.transform.position = new Vector3(0, position.y, position.z); //has to be 0 on x axis
                        pole.tag = "Structure";
                        pole.name = "Horizontal Pole";

                        Collider[] proxmityBlocks = Physics.OverlapSphere(pole.transform.position, 3f);

                        for (int k = 0; k < proxmityBlocks.Length; k++)
                        {
                            GameObject block = proxmityBlocks[k].gameObject;
                            if (block.name == "Pivot")
                            {
                                FixedJoint fj1 = pole.AddComponent<FixedJoint>();
                                fj1.connectedBody = block.AddComponent<Rigidbody>();
                                block.GetComponent<Rigidbody>().isKinematic = true;
                                block.GetComponent<Renderer>().material.color = Color.red;
                            }
                        }
                    }
                    //hinge joint config
                    //for (int j = 0; j < proxmityBlocks.Length; j++)
                    //{
                    //    GameObject block = proxmityBlocks[j].gameObject;
                    //    HingeJoint hj = block.AddComponent<HingeJoint>();
                    //    hj.axis = new Vector3(0f, 0f, 1f);
                    //    hj.connectedBody = proxmityBlocks[j].GetComponent<Rigidbody>();

                    //}

                }
                if (clickNum == 1)
                {
                    midCreate = true;
                    overlap = false;
                    clickNum = 2;
                }
                else if (clickNum == 2)
                {
                    midCreate = false;
                    overlap = false;
                    clickNum = 1;
                }
            }
        }
    }
    public void Undo()
    {
        Vector3 latestVector = history[history.Count - 1];
        //Debug.Log(latestVector);
        GameObject[] structure = GameObject.FindGameObjectsWithTag("Structure");
        for (int i = 0; i < structure.Length; i++)
        {
            GameObject block = structure[i];
            if (block.transform.position.y == latestVector.y && block.transform.position.z == latestVector.z)
            {
                Destroy(block);
            }
        }
        if (clickNum == 1)
        {
            clickNum = 2;
        }
        else if (clickNum == 2)
        {
            clickNum = 1;
        }
    }

    void PoleCheck(Vector3 pos)
    {
        Vector3 rPos = grid.GetNearestPointOnGrid(pos);
        Collider[] poles = Physics.OverlapBox(pos, new Vector3(1f, 0.5f, 0.5f));
        if (poles.Length == 0)
        {

        }

        Debug.Log("poles " + poles.Length);
    }

    void AddHorizontal()
    {

    }
}