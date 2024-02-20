using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashlight_POV : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Mesh mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;

        Vector3 origin = Vector3.zero;

        float fov = 90f;
        int rayCount = 2;
        float currentAngle = 0f;
        float angleIncrease = fov / rayCount;
        float viewDistance = 50f;


        Vector3[] vertices = new Vector3[rayCount +1 +1];
        Vector2[] uv = new Vector2[vertices.Length];
        int[] triangles = new int[rayCount * 3];

        vertices[0] = origin;
        int vertexIndex = 1;
        int triangleIndex = 0;


        for (int i = 0; i <= rayCount; i++)
        {
            Vector3 vertex = origin + GetVectorFromAngle(currentAngle) * viewDistance;
            vertices[vertexIndex] = vertex;

            if (i > 0)
            {
            triangles[triangleIndex + 0] = 0;
            triangles[triangleIndex + 1] = vertexIndex - 1;
            triangles[triangleIndex + 2] = vertexIndex; 
            triangleIndex += 3;
            }
            vertexIndex++;

            currentAngle -= angleIncrease;
        }

        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.triangles = triangles;

        
    }

    public static Vector3 GetVectorFromAngle(float currentAngle)
    {
        float angleRad = currentAngle * (Mathf.PI / 180f);
        return new Vector3(Mathf.Cos(angleRad), Mathf.Sin(angleRad));

    }

}
