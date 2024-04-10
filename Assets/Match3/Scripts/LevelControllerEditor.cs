using Match3;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

#if UNITY_EDITOR
[CustomEditor(typeof(LevelController))]
public class LevelControllerEditor : Editor
{
    
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        LevelController generator = (LevelController)target;
        if (GUILayout.Button("Build Level"))
        {
            generator.BuildLevel();
        }

    }
}
#endif
