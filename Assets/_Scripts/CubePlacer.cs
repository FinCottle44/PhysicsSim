using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic; // generic collection types (list in this case)

public class CubePlacer : MonoBehaviour
{
    public Vector3 startPos;
    public Vector3 endPos;
    public int clickNum;
    public Dropdown materialDrop;
    public CustomStructureConfig structureConf;
    public camInit camScript;
    public List<Vector3> history;
    public float FJBreakForce;
    public RadiusDraw RadiusDraw;

    private Grid grid;
    private GameObject poleBack;
    private GameObject poleFront;
    private GameObject roadCube;
    private GameObject cubeClickBack;
    private GameObject cubeClickFront;
    private float cubeClickScale = 0.75f;
    private float cubeClickScaleY = 0.3f;
    private Collider col;
    private bool OverUI;
    private bool overlap;
    private bool midCreate;

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
        //if (Input.GetMouseButtonUp(0))
        //{
        //    RaycastHit hitInfo;
        //    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        //    if (Physics.Raycast(ray, out hitInfo))
        //    {
        //        PlaceCubeNear(hitInfo.point);
        //    }
        //}
        KinematicCheck();
    }

    private void PlaceCubeNear(Vector3 clickPoint)
    {
        if (camScript.editing == true && OverUI == false)
        {
            var finalPosition = grid.GetNearestPointOnGrid(clickPoint);
            if (clickNum == 1)
            {
                startPos = finalPosition;
                RadiusDraw.CreateEmpty();
                RadiusDraw.CreatePoints();
                if (history.Count > 0)
                {
                    JointCheck(startPos);
                }
                if (overlap == false && midCreate == false)
                {
                    history.Add(startPos);
                    cubeClickBack = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
                    cubeClickBack.transform.position = new Vector3(-2.8f, finalPosition.y, finalPosition.z);
                    cubeClickBack.transform.Rotate(0, 0, 90);
                    cubeClickBack.transform.localScale = new Vector3(cubeClickScale, cubeClickScaleY, cubeClickScale);
                    cubeClickBack.GetComponent<Renderer>().material.color = Color.green;
                    cubeClickBack.AddComponent(typeof(CustomStructureConfig));
                    cubeClickBack.GetComponent<CapsuleCollider>().isTrigger = true;
                    cubeClickBack.tag = "Structure";
                    cubeClickBack.name = "Pivot";
                    GroundCheck(startPos, cubeClickBack);

                    cubeClickFront = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
                    cubeClickFront.transform.position = new Vector3(2.8f, finalPosition.y, finalPosition.z);
                    cubeClickFront.transform.Rotate(0, 0, 90);
                    cubeClickFront.transform.localScale = new Vector3(cubeClickScale, cubeClickScaleY, cubeClickScale);
                    cubeClickFront.GetComponent<Renderer>().material.color = Color.green;
                    cubeClickFront.AddComponent(typeof(CustomStructureConfig));
                    cubeClickFront.GetComponent<CapsuleCollider>().isTrigger = true;
                    cubeClickFront.tag = "Structure";
                    cubeClickFront.name = "Pivot";
                    GroundCheck(startPos, cubeClickFront);
                    clickNum = 2;
                }
            }
            else if (clickNum == 2)
            {
                RadiusDraw.CreateEmpty();
                RadiusDraw.CreatePoints();
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
                        cubeClickBack.transform.position = new Vector3(-2.8f, finalPosition.y, finalPosition.z);
                        cubeClickBack.transform.Rotate(0, 0, 90);
                        cubeClickBack.transform.localScale = new Vector3(cubeClickScale, cubeClickScaleY, cubeClickScale);
                        cubeClickBack.GetComponent<Renderer>().material.color = Color.blue;
                        cubeClickBack.AddComponent(typeof(CustomStructureConfig));
                        cubeClickBack.GetComponent<CapsuleCollider>().isTrigger = true;
                        cubeClickBack.tag = "Structure";
                        cubeClickBack.name = "Pivot";
                        GroundCheck(endPos, cubeClickBack);

                        cubeClickFront = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
                        cubeClickFront.transform.position = new Vector3(2.8f, finalPosition.y, finalPosition.z);
                        cubeClickFront.transform.Rotate(0, 0, 90);
                        cubeClickFront.transform.localScale = new Vector3(cubeClickScale, cubeClickScaleY, cubeClickScale);
                        cubeClickFront.GetComponent<Renderer>().material.color = Color.blue;
                        cubeClickFront.AddComponent(typeof(CustomStructureConfig));
                        cubeClickFront.GetComponent<CapsuleCollider>().isTrigger = true;
                        cubeClickFront.tag = "Structure";
                        cubeClickFront.name = "Pivot";
                        GroundCheck(endPos, cubeClickFront);
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
                        GameObject go = poleBack;

                        go.name = "steelBack";
                        go.transform.position = new Vector3(-2.8f, mid.y, mid.z);
                        go.transform.localScale = new Vector3(0.5f, 0.5f, root + 0.5f);
                        go.transform.Rotate(new Vector3(-(float)result, 0, 0));
                        go.tag = "Structure";
                        go.AddComponent(typeof(Rigidbody));
                        go.AddComponent(typeof(CustomStructureConfig));
                        Rigidbody rb = go.GetComponent<Rigidbody>();
                        rb.isKinematic = true;
                        FindNearPivots(go.transform.position, go.transform.localScale, go);

                        poleFront = GameObject.CreatePrimitive(PrimitiveType.Cube);
                        go = poleFront; //(GameObject go = blahblablah)

                        go.name = "steelFront";
                        go.transform.position = new Vector3(2.8f, mid.y, mid.z);
                        go.transform.localScale = new Vector3(0.5f, 0.5f, root + 0.5f);
                        go.transform.Rotate(new Vector3(-(float)result, 0, 0));
                        go.tag = "Structure";
                        go.AddComponent(typeof(Rigidbody));
                        go.AddComponent(typeof(CustomStructureConfig));
                        rb = go.GetComponent<Rigidbody>();
                        rb.isKinematic = true;
                        FindNearPivots(go.transform.position, go.transform.localScale, go);
                    }
                    else if (materialDrop.value == 1) //road
                    {
                        roadCube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                        GameObject go = roadCube;

                        go.name = "Road";
                        go.transform.position = new Vector3(0, mid.y, mid.z);
                        go.transform.localScale = new Vector3(5.4f, 0.5f, root + 0.5f);
                        go.transform.Rotate(new Vector3(-(float)result, 0, 0));
                        go.GetComponent<Renderer>().material.color = Color.black;
                        go.tag = "Structure";
                        go.AddComponent(typeof(Rigidbody));
                        go.AddComponent(typeof(CustomStructureConfig));
                        Rigidbody rb = go.GetComponent<Rigidbody>();
                        rb.isKinematic = true;
                        FindNearPivots(go.transform.position, go.transform.localScale, go);
                    }
                    clickNum = 1;
                }
                else
                {
                    clickNum = 1;
                }
            }
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
                    //horizontal support
                    PoleCheck(new Vector3(0, position.y, position.z));
                    

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
        Collider[] poles = Physics.OverlapBox(rPos, new Vector3(1f, 0.5f, 0.5f));
        if (poles.Length == 0)
        {
            AddHorizontal(pos);
        }
    }

    void AddHorizontal(Vector3 position)
    {
        GameObject pole = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        pole.transform.Rotate(0, 0, 90);
        pole.transform.localScale = new Vector3(0.5f, 2.5f, 0.5f);
        pole.transform.position = new Vector3(0, position.y, position.z); //has to be 0 on x axis
        pole.tag = "Structure";
        pole.name = "Horizontal Pole";
        pole.AddComponent(typeof(Rigidbody));

        Collider[] proxmityBlocks = Physics.OverlapSphere(pole.transform.position, pole.transform.localScale.y + 1);

        for (int k = 0; k < proxmityBlocks.Length; k++)
        {
            GameObject block = proxmityBlocks[k].gameObject;
            if (block.name == "Pivot")
            {
                FixedJoint fj1 = pole.AddComponent<FixedJoint>();
                fj1.connectedBody = block.GetComponent<Rigidbody>();
                block.GetComponent<Renderer>().material.color = Color.red;
            }
        }
    }

    void FindNearPivots(Vector3 pos, Vector3 scale, GameObject block)
    {
        //GameObject[] structure = GameObject.FindGameObjectsWithTag("Structure");
        Collider[] pivots = Physics.OverlapBox(pos, scale);
        if (block.name == "Road")
        {
            pivots = Physics.OverlapBox(pos, scale);
        }
        else if (block.name.Contains("steel"))
        {
            float length = block.transform.localScale.z / 2;
            pivots = Physics.OverlapSphere(pos, length);
        }
        Rigidbody rb = block.GetComponent<Rigidbody>();
        for (int i = 0; i < pivots.Length; i++)
        {
            if (pivots[i].name == "Pivot")
            {
                GameObject pivot = pivots[i].gameObject;
                //FixedJoint fj = pivot.AddComponent<FixedJoint>();
                //fj.connectedBody = block.GetComponent<Rigidbody>();
                HingeJoint hj = pivot.AddComponent<HingeJoint>();
                hj.connectedBody = block.GetComponent<Rigidbody>();
                hj.axis = new Vector3(0, 1, 0);
                hj.useLimits = true;
            }
        }
        
    }

    void GroundCheck(Vector3 pos, GameObject pivot)
    {
        Vector3 GroundPosL = new Vector3(0, 20, -15);
        Vector3 GroundPosR = new Vector3(0, 20, 15);
        GameObject ground = null;
        if (pos.y == GroundPosL.y && pos.z == GroundPosL.z)
        {
            ground = GameObject.Find("Ground L");
        }
        else if (pos.y == GroundPosR.y && pos.z == GroundPosR.z)
        {
            ground = GameObject.Find("Ground R");
        }
        if ((pos.y == GroundPosL.y && pos.z == GroundPosL.z) || (pos.y == GroundPosR.y && pos.z == GroundPosR.z)) //check if connected 2 ground
        {
            //FixedJoint fj = pivot.AddComponent<FixedJoint>();
            //fj.connectedBody = ground.GetComponent<Rigidbody>();

            HingeJoint hj = pivot.AddComponent<HingeJoint>();
            hj.connectedBody = ground.GetComponent<Rigidbody>();
            hj.axis = new Vector3(0, 1, 0);
        }
    }

    void KinematicCheck()
    {
        GameObject[] gos = GameObject.FindGameObjectsWithTag("Structure");
        if (!camScript.editing)
        {
            for (int i = 0; i < gos.Length; i++)
            {
                Rigidbody rb = gos[i].GetComponent<Rigidbody>();
                if (gos[i].name != "Pivot")
                {
                    rb.isKinematic = false;
                }
            }
        }
    }
}