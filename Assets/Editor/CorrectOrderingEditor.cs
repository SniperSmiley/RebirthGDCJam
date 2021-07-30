using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(CorrectRenderingOrder))]
public class CorrectOrderingEditor : Editor
{
    public override void OnInspectorGUI() {
        DrawDefaultInspector();

        CorrectRenderingOrder myScript = (CorrectRenderingOrder)target;

        if (GUILayout.Button("Update Sort Order")) {
            myScript.UpdateOrderFunc();
        }

    }
}
