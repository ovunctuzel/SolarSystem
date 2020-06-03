using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class MeshGeneratorEditor : MonoBehaviour
{
    [CustomEditor(typeof(MeshGenerator))]
    public class TilemapPatcherEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
        
            MeshGenerator myScript = (MeshGenerator)target;
            if(GUILayout.Button("Generate Terrain"))
            {
                myScript.GenerateTerrain();
            }
            if(GUILayout.Button("Single Cube"))
            {
                myScript.GenerateSingleCube();
            }
        }
    }

}
