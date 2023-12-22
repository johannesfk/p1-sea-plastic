using System;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

[CustomEditor(typeof(HexCell))]
public class HexCellEditor : Editor
{
    public override void OnInspectorGUI()
    {

        DrawDefaultInspector();
        HexCell script = (HexCell)target;

        EditorGUILayout.LabelField("Neighbors", EditorStyles.boldLabel);


        for (int i = 0; i < script.neighbors.Length; i++)
        {
            script.neighbors[i] = (HexCell)EditorGUILayout.ObjectField(((HexDirection)i).ToString(), script.neighbors[i], typeof(HexCell), true);
        }
    }

}