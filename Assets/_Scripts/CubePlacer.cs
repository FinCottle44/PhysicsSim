using UnityEngine;

public class CubePlacer : MonoBehaviour
{
    public Vector3 startPos;
    public Vector3 endPos;
    public int clickNum;

    private Grid grid;
    private GameObject midCube;

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
            GameObject.CreatePrimitive(PrimitiveType.Cube).transform.position = finalPosition;
            clickNum = 2;
        }
        else if (clickNum == 2)
        {
            endPos = finalPosition;
            GameObject.CreatePrimitive(PrimitiveType.Cube).transform.position = finalPosition;
            Vector3 mid = startPos + (endPos - startPos) / 2;
            midCube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            midCube.transform.position = mid;
            //midCube.transform.localScale = endPos - startPos;
            float xScale = Mathf.Abs(endPos.x - startPos.x);

            if (xScale == 0)
            {
                xScale = 1;
            }
            midCube.transform.localScale = new Vector3(xScale, Mathf.Abs(endPos.y - startPos.y), Mathf.Abs(endPos.z - startPos.z));
            clickNum = 1;
        }

    }
}