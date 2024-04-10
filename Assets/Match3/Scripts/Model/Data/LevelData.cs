using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Match3
{
    [CreateAssetMenu(fileName = "LevelData", menuName = "Match3/LevelData")]
    public class LevelData : ScriptableObject
    {
        public LevelDesignType designType;

        public int width  = 10;
        public int height = 10;

        public BoardCell[] board;
        public List<BoardLayer> boardLayers;

        public LevelGoalType goalType = LevelGoalType.CollectElements;      

        //public List<Element> elements;    

        public List<LevelGoalElement> goalElements;

        public bool useStepLimit = false;

        public int stepLimit = 10;

        public LevelData()
        {
            board = new BoardCell[width * height];
            var cellIndex = 0;
            for(var y = 0; y< height; y++)
            {
                for(var x = 0; x < width; x++)
                {
                    board[cellIndex] = new BoardCell();

                    cellIndex++;
                }
            }

            boardLayers = new List<BoardLayer>();
            goalElements = new List<LevelGoalElement>();
        }

        
        public bool ResizeLevelBoard(int newWidth, int newHeight)
        {
            //if (newWidth == 0 || newWidth > 15 || newHeight == 0 || newHeight > 15) return false;

            var oldBoardMatrix = new BoardCell[width, height];

            var cellIndex = 0;
            for (var y = 0; y < height; y++)
            {
                for (var x = 0; x < width; x++)
                {
                    oldBoardMatrix[x,y] = board[cellIndex];

                    cellIndex++;
                }
            }

            var newBoardMatrix = new BoardCell[newWidth, newHeight];
            board = new BoardCell[newWidth * newHeight];

            cellIndex = 0;
            for (var y = 0; y < newHeight; y++)
            {
                for (var x = 0; x < newWidth; x++)
                {
                    //board[cellIndex] = new BoardCell();
                    var newX = x <= width-1 ? x : width - 1;
                    var newY = y <= height-1 ? y : height - 1;

                    if (newX < x || newY < y)
                    {
                        newBoardMatrix[x, y] = new BoardCell();
                    }
                    else
                    {
                        newBoardMatrix[x, y] = oldBoardMatrix[newX, newY];
                    }

                    board[cellIndex] = newBoardMatrix[x, y];
                    cellIndex++;
                }
            }

            width = newWidth;
            height = newHeight;
            return true;
        }
        
        
        public BoardLayer AddBoardLayer()
        {
            var lastLayerIndex = boardLayers.Count;
            var layer = new BoardLayer(lastLayerIndex, $"Layer_{lastLayerIndex+1}");
            boardLayers.Add(layer);
            var cellIndex = 0;
            for (var y = 0; y < height; y++)
            {
                for (var x = 0; x < width; x++)
                {
                    board[cellIndex].elements.Add(null);
                    cellIndex++;
                }
            }

            return layer;
        }
          
        public void RemoveBoardLayer(BoardLayer layer)
        {
            var layerIndex = boardLayers.IndexOf(layer);
            boardLayers.Remove(layer);

            var cellIndex = 0;
            for (var y = 0; y < height; y++)
            {
                for (var x = 0; x < width; x++)
                {
                    board[cellIndex].elements.RemoveAt(layerIndex);
                    cellIndex++;
                }
            }
        }        
    }

    [System.Serializable]
    public class BoardCell
    {
        public bool active;

        public List<Element> elements;

        public BoardCell()
        {
            active = true;
            elements = new List<Element>();
        }
    }

    [System.Serializable]
    public class BoardLayer
    {
        public int index;
        public string name;

        public BoardLayer(int index, string name)
        {
            this.index = index;
            this.name = name;
        }
    }
}

