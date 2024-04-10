using System;
using UnityEditor;
using UnityEngine.UIElements;

namespace Match3.Match3Editor
{
    public class LevelBoardGridView : VisualElement
    {
        public LevelBoardGridView()
        {
            var visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/Match3/UIToolkit/LevelBoardGrid.uxml");
            visualTree.CloneTree(this);
        }
    }
}
