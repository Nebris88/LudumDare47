using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(TileGrid))]
public class TileGridEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        if (GUILayout.Button("Initialize Grid"))
        {
            ((TileGrid)target).initializeGrid();
        }
    }
}
