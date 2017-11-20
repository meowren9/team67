using UnityEngine;
using UnityEditor;
[CustomEditor(typeof(UpdateSettings))]
public class UpdateSettingsEditor : Editor
{
 public override void OnInspectorGUI()
 {
        UpdateSettings UpdateSettingsBeingInspected = target as UpdateSettings;
        base.OnInspectorGUI();
        if (GUILayout.Button("Update Colors"))
        {
           UpdateSettingsBeingInspected.ChangeColors();
        }
        if (GUILayout.Button("Update SmokeSettings"))
        {
           UpdateSettingsBeingInspected.SmokeSettings();
        }
 }


}

