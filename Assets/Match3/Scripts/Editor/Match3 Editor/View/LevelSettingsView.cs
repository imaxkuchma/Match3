using UnityEditor;
using UnityEngine.UIElements;

namespace Match3.Match3Editor
{
    public class LevelSettingsView : VisualElement
    {   
        public IntegerField levelWidth;
        public IntegerField levelHeight;
        public Button resizeLevelButton;
        public EnumField goalType;
        public Toggle useStepLimit;
        public IntegerField stepLimit;

        public LevelSettingsView()
        {
            var visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/Match3/UIToolkit/LevelSettings.uxml");
            visualTree.CloneTree(this);

            levelWidth = this.Q<IntegerField>("level-general-settings__width");
            levelHeight = this.Q<IntegerField>("level-general-settings__height");
            resizeLevelButton = this.Q<Button>("level-general-settings__resize-button");
            goalType = this.Q<EnumField>("level-general-settings__goal-type");
            useStepLimit = this.Q<Toggle>("level-general-settings__use-step-limit");
            stepLimit = this.Q<IntegerField>("level-general-settings__step-limit");
        }
    }
}
