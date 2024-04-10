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
        private LevelBoardGridView _levelBoardGridView;

        private const int _cellSizeX = 50;
        private const int _cellSizeY = 50;

        private BoardCellPair[,] _levelBoard;

        private void CreateLevelBoardView()
        {
            if(_levelBoardGridView == null)
            {
                var boardContainer = rootVisualElement.Q<VisualElement>("body__level-borad-container");

                

                _levelBoardGridView = new LevelBoardGridView();
               /* boardContainer.RegisterCallback<MouseMoveEvent>((evn) => 
                {
                    Debug.Log(evn.mousePosition);
                    UnityEngine.Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
                });*/

                boardContainer.Add(_levelBoardGridView);
            }         
        }

        public void UpdateLevelBoardView()
        {
            var level = _database.CurrentLevel;

            _levelBoardGridView.Clear();

            var levelDesignData = LevelDesignCollection.Instance.GetlevelDesignByType(level.designType);

            _levelBoard = new BoardCellPair[level.width, level.height];

            var cellIndex = 0;
            for (var y = 0; y < level.height; y++)
            {
                var gridRow = new BoardGridRowView(_levelBoardGridView);

                for (var x = 0; x < level.width; x++)
                {
                    var cellActive = level.board[cellIndex].active;
                    var gridCell = new BoardGridCellView(gridRow, cellIndex, new Vector2(_cellSizeX, _cellSizeY), levelDesignData.cellBackground);
                    gridCell.SetActive(cellActive);
                    gridCell.RegisterCallback<ClickEvent>(OnBoardGridCellClick);

                    _levelBoard[x, y] = new BoardCellPair(gridCell, cellActive);
                    
                    cellIndex++;
                }
            }
        }

        private void OnBoardGridCellClick(ClickEvent evt)
        {
            var cellView = evt.target as BoardGridCellView;

            bool active = !_database.CurrentLevel.board[cellView.cellIndex].active;

            _database.CurrentLevel.board[cellView.cellIndex].active = active;

            cellView.SetActive(active);

            Debug.Log($"{cellView.localBound.left} - {cellView.localBound.top}");

            EditorUtility.SetDirty(_database.CurrentLevel);
        }

        private class BoardGridRowView : VisualElement
        {
            public BoardGridRowView(VisualElement grid)
            {
                name = "level-board-grid__row";
                AddToClassList("level-board-grid__row");
                grid.Add(this);
            }
        }

        private class BoardGridCellView : VisualElement
        {
            public int cellIndex;
            private Texture2D _background;

            public BoardGridCellView(VisualElement row, int cellIndex, Vector2 size, Sprite background)
            {
                this.cellIndex = cellIndex;
                name = "level-board-grid__cell";
                style.width = (int)size.x;
                style.height = (int)size.y;
                _background = background.texture;
                row.Add(this);
            }

            public void SetActive(bool active)
            {
                if (active)
                {
                    RemoveFromClassList("level-board-grid__cell--disabled");
                    AddToClassList("level-board-grid__cell--enabled");
                    SetBackground(_background);
                }
                else
                {
                    RemoveFromClassList("level-board-grid__cell--enabled");
                    AddToClassList("level-board-grid__cell--disabled");
                    SetBackground(null);
                }
            }

            private void SetBackground(Texture2D texture)
            {
                style.backgroundImage = texture;
            }
        }

        private struct BoardCellPair
        {
            public BoardGridCellView cell;
            public bool active;
            public BoardCellPair(BoardGridCellView cell, bool active)
            {
                this.cell = cell;
                this.active = active;
            }
        }


    }
}
