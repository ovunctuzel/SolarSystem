using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ScalarFieldEditor : MonoBehaviour
{
    [CustomEditor(typeof(ScalarField))]
    public class TilemapPatcherEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
        
            ScalarField myScript = (ScalarField)target;
            if(GUILayout.Button("Create Chunk CPU"))
            {
                myScript.CreateChunk(Vector3.zero);
            }
            if(GUILayout.Button("Create Chunk GPU (Optimized)"))
            {
                myScript.ClearTerrain();
                myScript.CreateChunkGPU_Optimized(Vector3.zero);
            }
            if(GUILayout.Button("Clear World"))
            {
                myScript.ClearTerrain();
            }
        }
    }

}
