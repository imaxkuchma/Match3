using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace Match3.Match3Editor
{
    public class GroupPanelView : VisualElement
    {
        private Label _title;
        private VisualElement _header;
        private VisualElement _contentContainer;

        public GroupPanelView(string windowTitle)
        {
            var visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/Match3/UIToolkit/GroupPanel.uxml");
            visualTree.CloneTree(this);

            _header = this.Q<VisualElement>("group-panel__header");
            _title = this.Q<Label>("group-panel__header__title");
            _contentContainer = this.Q<VisualElement>("group-panel__container");

            style.position = Position.Relative;

            _title.text = windowTitle;
        }

        public void AddContent(VisualElement content)
        {
            _contentContainer.Add(content);
        }

    }
}
