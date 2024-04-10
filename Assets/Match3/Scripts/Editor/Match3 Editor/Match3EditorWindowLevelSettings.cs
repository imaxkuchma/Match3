using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace Match3.Match3Editor
{
    public partial class Match3EditorWindow : EditorWindow
    {
        private const int _maxLevelBoardWidth = 12;
        private const int _maxLevelBoardHeight = 12;

        private LevelSettingsView _levelSettingsView;

        private void CreateLevelSettingsView()
        {
            if (_levelSettingsView == null)
            {
                var generalSettingsPanel = rootVisualElement.Q<VisualElement>("body__general-settings-panel");    

                _levelSettingsView = new LevelSettingsView();

                var groupPanel = new GroupPanelView("Level Settings");
                groupPanel.AddContent(_levelSettingsView);
                generalSettingsPanel.Add(groupPanel);

                /********************************************************************/
                _levelSettingsView.levelWidth.RegisterValueChangedCallback<int>((evn) =>
                {

                    var valueWidth = 0;
                    if(evn.newValue < 1)
                    {
                        valueWidth = 1;
                    }
                    else if(evn.newValue > _maxLevelBoardWidth)
                    {
                        valueWidth = _maxLevelBoardWidth;
                    }
                    else
                    {
                        valueWidth = evn.newValue;
                    }
                    _levelSettingsView.levelWidth.value = valueWidth;
                });

                /********************************************************************/

                _levelSettingsView.levelHeight.RegisterValueChangedCallback<int>((evn) =>
                {

                    var valueHeight = 0;
                    if (evn.newValue < 1)
                    {
                        valueHeight = 1;
                    }
                    else if (evn.newValue > _maxLevelBoardHeight)
                    {
                        valueHeight = _maxLevelBoardHeight;
                    }
                    else
                    {
                        valueHeight = evn.newValue;
                    }
                    _levelSettingsView.levelHeight.value = valueHeight;
                });

                /********************************************************************/
                _levelSettingsView.resizeLevelButton.clicked += () =>
                {
                    _database.CurrentLevel.ResizeLevelBoard(_levelSettingsView.levelWidth.value, _levelSettingsView.levelHeight.value);

                    EditorUtility.SetDirty(_database.CurrentLevel);

                    UpdateLevelBoardView();
                };
                /********************************************************************/
                _levelSettingsView.goalType.RegisterValueChangedCallback((evn) =>
                {     
                    _database.CurrentLevel.goalType = (LevelGoalType)evn.newValue;

                    EditorUtility.SetDirty(_database.CurrentLevel);
                }); 
                 /********************************************************************/
                 _levelSettingsView.useStepLimit.RegisterCallback<ChangeEvent<bool>>((evn) =>
                {
                    _database.CurrentLevel.useStepLimit = evn.newValue;

                    _levelSettingsView.stepLimit.SetEnabled(evn.newValue);

                    EditorUtility.SetDirty(_database.CurrentLevel);
                });
                /********************************************************************/

                _levelSettingsView.stepLimit.RegisterCallback<KeyDownEvent>((evt) =>
                {
                    if (evt.keyCode == KeyCode.Return || evt.keyCode == KeyCode.KeypadEnter)
                    {
                        _database.CurrentLevel.stepLimit = _levelSettingsView.stepLimit.value;

                        EditorUtility.SetDirty(_database.CurrentLevel);
                    }
                });
                _levelSettingsView.levelHeight.RegisterCallback<FocusOutEvent>((evn) =>
                {
                    _levelSettingsView.stepLimit.value = _database.CurrentLevel.stepLimit;
                });
            }
        }

        private void UpdateLevelSettingsView()
        {
            _levelSettingsView.levelWidth.value = _database.CurrentLevel.width;
            _levelSettingsView.levelHeight.value = _database.CurrentLevel.height;
            _levelSettingsView.goalType.value = _database.CurrentLevel.goalType;
            _levelSettingsView.useStepLimit.value = _database.CurrentLevel.useStepLimit;
            _levelSettingsView.stepLimit.SetEnabled(_database.CurrentLevel.useStepLimit);
            _levelSettingsView.stepLimit.value = _database.CurrentLevel.stepLimit;            
        }
    }
}
