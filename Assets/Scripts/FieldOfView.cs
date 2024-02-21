using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FieldOfView : MonoBehaviour
{
    public float fov;
    public int rayCount;
    public float viewDistance;


    public void Start()
    {
        Mesh mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;

        //fov = 90f;
        //viewDistance = 50f;
        //rayCount = 50;

        Vector3 origin = Vector3.zero;
        float angle = 0f;
        float angleIncrease = fov / rayCount;
        


        Vector3[] vertices = new Vector3[rayCount+ 1 + 1];
        Vector2[] uv = new Vector2[vertices.Length];
        int[] triangles = new int[rayCount * 3];

        vertices[0] = origin;

        int vertexIndex = 1;
        int triangleIndex = 0;

        for(int i = 0; i<=rayCount; i++)
        {
            Vector3 vertex = origin + GetVectorFromAngle(angle) * viewDistance;
            vertices[vertexIndex] = vertex;
            
            if( i > 0 )
            {
                triangles[triangleIndex + 0] = 0;
                triangles[triangleIndex + 1] = vertexIndex - 1;
                triangles[triangleIndex + 2] = vertexIndex;

                triangleIndex += 3;
            }

            vertexIndex++;


            angle -= angleIncrease;
        }

        triangles[0] = 0;
        triangles[1] = 1;
        triangles[2] = 2;

        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.triangles = triangles;
    }

    static Vector3 GetVectorFromAngle(float angle)
    {
        float angleRad = angle * (Mathf.PI / 180f);
        return new Vector3(Mathf.Cos(angleRad), Mathf.Sin(angleRad));
    }



}
