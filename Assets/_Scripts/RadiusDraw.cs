using UnityEngine;
using System.Collections;

[RequireComponent(typeof(LineRenderer))]
public class RadiusDraw : MonoBehaviour
{
    [Range(0, 60)]
    public int segments = 60;
    [Range(0, 5)]
    public float xradius = 5;
    [Range(0, 5)]
    public float yradius = 5;
    LineRenderer line;

    public GameObject empty;
    public CubePlacer cubeplacer;
    public Grid grid;

    bool clicked;

    public void CreatePoints()
    {
        float x;
        float y;
        float z;

        float angle = 30f;

        for (int i = 0; i < (segments + 1); i++)
        {
            x = Mathf.Sin(Mathf.Deg2Rad * angle) * xradius;
            z = Mathf.Cos(Mathf.Deg2Rad * angle) * yradius;


            line.SetPosition(i, new Vector3(0, x, z));

            angle += (360f / segments);
        }
    }

    public void CreateEmpty()
    {
        empty = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        empty.GetComponent<Renderer>().enabled = false;
        empty.GetComponent<Collider>().enabled = false;
        empty.name = "Empty (RadiusDraw)";
        empty.tag = "empty";
        if (cubeplacer.clickNum == 1)
        {
            empty.transform.position = grid.GetNearestPointOnGrid(cubeplacer.startPos);
        }
        else if (cubeplacer.clickNum == 2)
        {
            GameObject[] empties = GameObject.FindGameObjectsWithTag("empty");
            for (int i = 0; i < empties.Length; i++)
            {
                Destroy(empties[i]);
            }
        }
        
        line = empty.AddComponent<LineRenderer>();
        line.SetVertexCount(segments + 1);
        line.useWorldSpace = false;
        line.material = new Material(Shader.Find("Particles/Additive"));
        line.startWidth = 0.1f;
        line.startColor = Color.white;
        line.endColor = Color.white;
    }
}