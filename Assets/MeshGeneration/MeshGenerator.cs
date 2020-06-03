using System.Collections;
using System.Collections.Generic;
using UnityEngine.Rendering;
using UnityEngine;

[ExecuteInEditMode]
public class MeshGenerator : MonoBehaviour
{
    public Material material;
    public int cubeConfig = 0;

    public void GenerateSingleCube()
    {
        Vector3[] edgePositions = {
            new Vector3(0.5f, 0, 1),
            new Vector3(1, 0, 0.5f),
            new Vector3(0.5f, 0, 0),
            new Vector3(0, 0, 0.5f),
            new Vector3(0.5f, 1, 1),
            new Vector3(1, 1, 0.5f),
            new Vector3(0.5f, 1, 0),
            new Vector3(0, 1, 0.5f),
            new Vector3(0, 0.5f, 1),
            new Vector3(1, 0.5f, 1),
            new Vector3(1, 0.5f, 0),
            new Vector3(0, 0.5f, 0)
        };

        List<Vector3> verts = new List<Vector3>();
        List<int> tris = new List<int>();

        for(int i = 0; i < 5; i++)
        {   
            int edge1 = TriangulationTable.triangulation[cubeConfig, i*3];
            int edge2 = TriangulationTable.triangulation[cubeConfig, i*3+1];
            int edge3 = TriangulationTable.triangulation[cubeConfig, i*3+2];
            if(edge1 >= 0 && edge2 >= 0 && edge3 >= 0)
            {
                tris.Add(edge1);
                tris.Add(edge2);
                tris.Add(edge3);
            }
        }

        GenerateMesh_CPU(edgePositions, tris.ToArray(), material);
    }

    public void GenerateTerrain()
    {
        Vector3[] verts = {
            new Vector3(0, 0, 0),
            new Vector3(1, 0, 0),
            new Vector3(0, 1, 0),
            new Vector3(0, 0, 1)
        };

        int[] tris = {
            0, 2, 1,
            0, 1, 3,
            0, 3, 2,
            1, 2, 3
        };

        GenerateMesh_CPU(verts, tris, material);
    }

    public void GenerateMesh_CPU(Vector3[] vertices, int[] triangles, Material material)
    {    
        Mesh mesh = new Mesh();
        mesh.indexFormat = IndexFormat.UInt32;
        GetComponent<MeshFilter>().mesh = mesh;
        mesh.Clear();
    
        mesh.vertices = vertices;
        mesh.triangles = triangles;

        mesh.RecalculateNormals();
        mesh.RecalculateBounds();
        mesh.Optimize();

        GetComponent<MeshRenderer>().material = material;
    }
}
