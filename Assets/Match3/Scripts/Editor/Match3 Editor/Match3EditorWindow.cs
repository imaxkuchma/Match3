using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace Match3.Match3Editor
{
    public partial class Match3EditorWindow : EditorWindow
    {
        [MenuItem("Tools/Match3/Match3 Editor", false, -1)]
        public static void OpenMatch3EditorWindow()
        {
            var window = GetWindow<Match3EditorWindow>();
            window.titleContent = new GUIContent("Match3 Editor");
            window.minSize = new Vector2(640, 480);
            
        }
    }
}
