using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace Match3.Match3Editor
{
    [CustomEditor(typeof(Match3Database), true)]
    public class Match3DatabaseEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            DrawControlPanel();
        }

        private void DrawControlPanel()
        {
            if (GUILayout.Button("Edit"))
            {
                Match3EditorWindow.OpenMatch3EditorWindow();
            }
        }
    }
}


