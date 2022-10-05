using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(EditorHelper))]
public class InspectorButton : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        EditorHelper script = (EditorHelper)target;


        GUILayout.BeginHorizontal("box");
        if (GUILayout.Button("Transition Out"))
        {
            script.TransitionOut();
        }
        else if (GUILayout.Button("Transition In"))
        {
            script.TransitionIn();
        }
        GUILayout.EndHorizontal();


        GUILayout.BeginHorizontal("box");
        if (GUILayout.Button("Load Settings"))
        {
            script.LoadSettings();
        }
        else if (GUILayout.Button("Load MainMenu"))
        {
            script.LoadMainMenu();
        }
        GUILayout.EndHorizontal();
    }

}