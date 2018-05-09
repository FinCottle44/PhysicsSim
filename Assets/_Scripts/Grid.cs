using UnityEngine;

public class Grid : MonoBehaviour
{
    [SerializeField]
    public camInit camScript;
    public GameObject parent;

    private float size = 1f;

    private void Start()
    {
        DrawGrid();
    }

    private void Update()
    {

    }

    public Vector3 GetNearestPointOnGrid(Vector3 position)
    {
        position -= transform.position;

        int xCount = Mathf.RoundToInt(position.x / size);
        int yCount = Mathf.RoundToInt(position.y / size);
        int zCount = Mathf.RoundToInt(position.z / size);

        Vector3 result = new Vector3(
            (float)xCount * size,
            (float)yCount * size,
            (float)zCount * size);

        result += transform.position;

        return result;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        for (float y = 0; y < 40; y += size)
        {
            for (float z = -20; z < 20; z += size)
            {
                var point = GetNearestPointOnGrid(new Vector3(0f, y, z));
                //Gizmos.DrawSphere(point, 0.1f);
            }
        }
    }

    void DrawGrid()
    {
        for (float y = 0; y < 37; y += size)
        {
            for (float z = -17; z < 17; z += size)
            {
                var point = GetNearestPointOnGrid(new Vector3(0f, y, z));
                GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                sphere.tag = "GridSphere";
                sphere.transform.position = point;
                sphere.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
                if (z == 0)
                {
                    sphere.GetComponent<Renderer>().material.color = Color.blue;
                }
                else
                {
                    sphere.GetComponent<Renderer>().material.color = Color.black;
                }
                sphere.GetComponent<Collider>().enabled = false;
                sphere.transform.SetParent(parent.transform);   
            }
        }
    }

    public void DestroyGrid()
    {
        GameObject[] grid = GameObject.FindGameObjectsWithTag("GridSphere");
        for (int i = 0; i < grid.Length; i++)
        {
            GameObject block = grid[i];
            Destroy(block);
        }
    }
}