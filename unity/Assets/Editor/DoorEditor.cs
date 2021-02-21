using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Door))]
public class DoorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        var door = target as Door;
        DrawDefaultInspector();

        if(GUILayout.Button("Pick opened"))
        {
            var objTransform = door.transform;
            door.openLocation = objTransform.position;
            door.openRotation = objTransform.rotation;
        }
        if(GUILayout.Button("Pick closed"))
        {
            var objTransform = door.transform;
            door.closeLocation = objTransform.position;
            door.closeRotation = objTransform.rotation;
        }
        if (GUILayout.Button("Set default audio"))
        {
            door.openSound = (AudioClip)AssetDatabase.LoadAssetAtPath("Assets/sounds/фурнитура/дверь_откр.ogg", typeof(AudioClip));
            door.closeSound = (AudioClip)AssetDatabase.LoadAssetAtPath("Assets/sounds/фурнитура/дверь_закр.ogg", typeof(AudioClip));
        }
    }

}
