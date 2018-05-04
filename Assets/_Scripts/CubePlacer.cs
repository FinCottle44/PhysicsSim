using UnityEngine;

public class CubePlacer : MonoBehaviour
{
    public Vector3 startPos;
    public Vector3 endPos;
    public int clickNum;

    private Grid grid;
    private GameObject midCube;
    private GameObject firstCube;

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
        var finalPosition = grid.GetNearestPointOnGrid(clickPoint);
        if (clickNum == 1)
        {
            startPos = finalPosition;
            //GameObject.CreatePrimitive(PrimitiveType.Cube).transform.position = finalPosition;
            firstCube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            firstCube.transform.position = new Vector3(-3, finalPosition.y, finalPosition.z);
            clickNum = 2;
        }
        else if (clickNum == 2)
        {
            endPos = finalPosition;
            //GameObject.CreatePrimitive(PrimitiveType.Cube).transform.position = finalPosition;
            firstCube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            firstCube.transform.position = new Vector3(-3, finalPosition.y, finalPosition.z);

            Vector3 mid = startPos + (endPos - startPos) / 2;
            midCube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            midCube.transform.position = new Vector3(-3, mid.y, mid.z);
            float changeY = Mathf.Abs(endPos.y - startPos.y);
            float changeZ = Mathf.Abs(endPos.z - startPos.z);
            float difYZ = changeY / changeZ;
            float angle = Mathf.Atan(difYZ);
            double result = RadianToDegree(angle);

            Debug.Log("y: " + changeY + " z: " + changeZ);
            Debug.Log(difYZ);
            Debug.Log(angle);
            Debug.Log((float)result);

            float xScale = Mathf.Abs(endPos.x - startPos.x);
            float yScale = Mathf.Abs(endPos.y - startPos.y);
            float zScale = Mathf.Abs(endPos.z - startPos.z);

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
                zScale = 1;
            }
            midCube.transform.localScale = new Vector3(xScale, yScale, zScale);
            midCube.transform.Rotate(new Vector3((float)result, 0, 0));
            Debug.Log(midCube.transform.rotation);

            clickNum = 1;

        }
    }

    double RadianToDegree(double angle)
    {
        return angle * (180.0 / Mathf.PI);
    }
}