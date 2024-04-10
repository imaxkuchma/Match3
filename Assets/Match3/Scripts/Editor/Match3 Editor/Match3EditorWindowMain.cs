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
        private const string _version = "1.0";

        private Match3Database _database = null;
        
        public void OnEnable()
        {
            var root = this.rootVisualElement;
            var visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/Match3/UIToolkit/Match3EditorWindow.uxml");
            visualTree.CloneTree(root);

            var titleLabel = root.Q<Label>("header__title-label");
            titleLabel.text = $"Match3 Editor v{_version}";

            OnSelectionChange();      
        }

        
        private void OnSelectionChange()
        {
            SelectObject(Selection.activeObject);
        }

        private void SelectObject(UnityEngine.Object activeObject)
        {
            if (activeObject == null) return;

            var newDatabase = activeObject as Match3Database;
            if (newDatabase)
            {
                var databaseID = (_database != null) ? _database.GetInstanceID() : -1;
                var newDatabaseID = newDatabase.GetInstanceID();
                var needToReset = (_database != null) && (databaseID != newDatabaseID);
                _database = newDatabase;

                if (needToReset)
                {
                    
                }
                else
                {
                    
                }

                if(_database.levels.Count == 0)
                {
                    _database.CreateLevel();
                }

                PrepareViews();

                DatabaseNavigation(DatabaseNavigationType.LevelNumber, _database.currentLevelIndex);    

                Repaint();
            }
        }

        private void PrepareViews()
        {



            CreateDatabaseNavigationView();
            CreateLevelSettingsView();

            var elementListController = new ElementListController();
            elementListController.InitializeElementList();
            elementListController.OnElementSelect += OnElementSelect;

            var groupPanel = new GroupPanelView("Elements");
            groupPanel.AddContent(elementListController.view);

            var generalSettingsPanel = rootVisualElement.Q<VisualElement>("body__general-settings-panel");
            generalSettingsPanel.Add(groupPanel);

            CreateLevelBoardView();
            
        }

        private void OnElementSelect(ElementData obj)
        {
            Debug.Log(obj.elementName);
        }

        private void DatabaseNavigation(DatabaseNavigationType navigationType, int levelNumber = -1)
        {
            if (_database.levels.Count == 0) return;

            switch (navigationType)
            {
                case DatabaseNavigationType.FirstLevel:
                    _database.currentLevelIndex = 0;
                    break;
                case DatabaseNavigationType.PreviousLevel:
                    _database.currentLevelIndex--;
                    break;
                case DatabaseNavigationType.LevelNumber:
                    if (levelNumber - 1 > 0 && levelNumber - 1 <= _database.GetLastLevelIndex())
                    {
                        _database.currentLevelIndex = levelNumber - 1;
                    }
                    break;
                case DatabaseNavigationType.NextLevel:
                    _database.currentLevelIndex++;
                    break;
                case DatabaseNavigationType.LastLevel:
                    _database.currentLevelIndex = _database.GetLastLevelIndex();
                    break;
            }

            if (_database.currentLevelIndex < 0)
            {
                _database.currentLevelIndex = 0;
            }
            else if (_database.currentLevelIndex > _database.GetLastLevelIndex())
            {
                _database.currentLevelIndex = _database.GetLastLevelIndex();
            }

            EditorUtility.SetDirty(_database);

            UpdateData();
        }

        private void UpdateData()
        {
            UpdateDatabaseNavigationView();
            UpdateLevelBoardView();
            UpdateLevelSettingsView();
        }
    }
}
