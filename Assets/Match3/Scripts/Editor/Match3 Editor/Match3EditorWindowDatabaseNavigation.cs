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
        private DatabaseNavigationView _databaseNavigationView;

        private void CreateDatabaseNavigationView()
        {
            if (_databaseNavigationView == null)
            {
                var generalSettingsPanel = rootVisualElement.Q<VisualElement>("body__general-settings-panel");

                _databaseNavigationView = new DatabaseNavigationView();

                var groupPanel = new GroupPanelView("Navigation");
                groupPanel.AddContent(_databaseNavigationView);
                generalSettingsPanel.Add(groupPanel);
                                
                _databaseNavigationView.firstLevelButton.clicked += () => DatabaseNavigation(DatabaseNavigationType.FirstLevel);
                _databaseNavigationView.prevLevelButton.clicked += () => DatabaseNavigation(DatabaseNavigationType.PreviousLevel);

                _databaseNavigationView.levelNumberField.RegisterCallback<KeyDownEvent>((evt) =>
                {
                    if (evt.keyCode == KeyCode.Return || evt.keyCode == KeyCode.KeypadEnter)
                    {
                        DatabaseNavigation(DatabaseNavigationType.LevelNumber, _databaseNavigationView.levelNumberField.value);
                    }
                });
                _databaseNavigationView.levelNumberField.RegisterCallback<FocusOutEvent>((evn) =>
                {
                    _databaseNavigationView.levelNumberField.value = _database.currentLevelIndex;
                });

                _databaseNavigationView.nextLevelButton.clicked += () => DatabaseNavigation(DatabaseNavigationType.NextLevel);
                _databaseNavigationView.lastLevelButton.clicked += () => DatabaseNavigation(DatabaseNavigationType.LastLevel);


                _databaseNavigationView.createLevelButton.clicked += () =>
                {
                    _database.CreateLevel();

                    EditorUtility.SetDirty(_database);

                    UpdateData();
                };

                _databaseNavigationView.deleteLevelButton.clicked += () =>
                {
                    _database.DeleteLevel();

                    EditorUtility.SetDirty(_database);

                    UpdateData();
                };
            }
        }

        private void UpdateDatabaseNavigationView()
        {
            _databaseNavigationView.levelNumberField.value = _database.currentLevelIndex + 1;
            _databaseNavigationView.deleteLevelButton.SetEnabled(_database.levels.Count>1);
        }

        private enum DatabaseNavigationType
        {
            LevelNumber,
            FirstLevel,
            PreviousLevel,
            NextLevel,
            LastLevel
        }
    }
}
