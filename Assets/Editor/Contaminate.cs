using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.UIElements;

[CustomEditor(typeof(WaterContamination))]
public class WaterContaminationEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        WaterContamination script = (WaterContamination)target;
        if (GUILayout.Button("Contaminate Water"))
        {
            if (Application.isPlaying)
            {
                script.ContaminateWater(); // replace 1 with the amount you want
            }
            else
            {
                Debug.LogWarning("You can only contaminate water in play mode.");
            }
        }
    }
}