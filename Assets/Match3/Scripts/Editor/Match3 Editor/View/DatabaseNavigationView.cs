using UnityEditor;
using UnityEngine.UIElements;

namespace Match3.Match3Editor
{
    public class DatabaseNavigationView : VisualElement
    {
        public Button firstLevelButton;
        public Button prevLevelButton;
        public IntegerField levelNumberField;
        public Button nextLevelButton;
        public Button lastLevelButton;
        public Button createLevelButton;
        public Button deleteLevelButton;

        public DatabaseNavigationView()
        {         
            var visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/Match3/UIToolkit/DatabaseNavigation.uxml");
            visualTree.CloneTree(this);

            firstLevelButton = this.Q<Button>("navigation__first-level-button");
            prevLevelButton = this.Q<Button>("navigation__prev-level-button");
            levelNumberField = this.Q<IntegerField>("navigation__level-number-field");
            nextLevelButton = this.Q<Button>("navigation__next-level-button");
            lastLevelButton = this.Q<Button>("navigation__last-level-button");
            createLevelButton = this.Q<Button>("edit__create-level");
            deleteLevelButton = this.Q<Button>("edit__delete-level");
        }     
    }
}
