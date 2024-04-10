using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Match3
{

    public class BoardBuilder : MonoBehaviour
    {
        [SerializeField] private BorderDataCollection _borderDataCollection;
        [SerializeField] private CellView _cellViewPrefab;
        [SerializeField] private Transform _fieldParent;
        [SerializeField] private Transform _borderParent;


        //private CellData[,] _board.Cells;
        private List<GameObject> _cells;
        private List<GameObject> _borders;

        public IBoardData BuildBoard(LevelData levelFieldData)
        {
            //ClearBoard();

            return BuildField(levelFieldData);
        }

        private void ClearBoard(BoardData boardData)
        {
            if (boardData != null)
            {
                for (var y = 0; y < boardData.Height; y++)
                {
                    for (var x = 0; x < boardData.Width; x++)
                    {
                        /*if (boardData.Cells[x, y] != null && boardData.Cells[x, y].CellView != null)
                        {
                            Destroy(boardData.Cells[x, y].CellView.gameObject);
                            if(boardData.Cells[x, y].pieceData != null && boardData.Cells[x, y].pieceData.view != null)
                            {
                                Destroy(boardData.Cells[x, y].pieceData.view.gameObject);
                            }    
                        }*/
                    }
                }
            }

            if (_borders != null)
            {
                foreach (GameObject border in _borders)
                {
                    Destroy(border);
                }
            }
        }

        private IBoardData BuildField(LevelData levelData)
        {
            Vector2 tileSize = _cellViewPrefab.GetComponent<RectTransform>().sizeDelta;

            var width = levelData.width;
            var height = levelData.height;

            var board = new BoardData(width, height);

            var index = 0;
            for (var y = 0; y < height; y++)
            {
                for (var x = 0; x < width; x++)
                {
                    if (levelData.board[index].active)
                    {
                        var worldPosition = new Vector2((-width / 2 + x) * tileSize.x + tileSize.x / 2, (height / 2 - y) * tileSize.y - tileSize.y / 2);

                        var cellView = Instantiate(_cellViewPrefab, _fieldParent);
                        cellView.gameObject.transform.localPosition = worldPosition;

                        board.Cells[x, y] = new CellData(x, y, cellView, worldPosition);
                    }
                    index++;
                }
            }

            BuildBorder(board);

            return board;
        }

        #region BuildBorder

        private void BuildBorder(IBoardData board)
        {
            _borders = new List<GameObject>();

            Vector2 tileSize = Vector2.zero;

            //********************************************************************************************************************************************************************

            RectTransform verticalLeftBorderTransform1 = null;
            RectTransform verticalLeftBorderTransform2 = null;
            RectTransform verticalRightBorderTransform1 = null;
            RectTransform verticalRightBorderTransform2 = null;

            for (var x = 0; x < board.Width; x++)
            {
                verticalLeftBorderTransform1 = null;
                verticalLeftBorderTransform2 = null;
                verticalRightBorderTransform1 = null;
                verticalRightBorderTransform2 = null;

                for (var y = 0; y < board.Height; y++)
                {

                    if (board.Cells[x, y] != null)
                    {
                        if (tileSize == Vector2.zero)
                        {
                            tileSize = board.Cells[x, y].View.GetComponent<RectTransform>().sizeDelta;
                        }

                        if (x == 0)
                        {
                            if (verticalLeftBorderTransform1 == null)
                            {
                                var border = Instantiate(_borderDataCollection.GetBorderByType(BorderSpriteType.VerticalLeft), _borderParent);
                                verticalLeftBorderTransform1 = border.GetComponent<RectTransform>();
                                verticalLeftBorderTransform1.localPosition = board.Cells[x, y].WorldPosition + new Vector3(-tileSize.x / 2, tileSize.y / 2, 0);

                                verticalLeftBorderTransform1.sizeDelta = new Vector2(verticalLeftBorderTransform1.sizeDelta.x, tileSize.y);
                                _borders.Add(border);
                            }
                            else
                            {
                                verticalLeftBorderTransform1.sizeDelta += new Vector2(0, tileSize.y);
                            }


                        }

                        if (x > 0 && board.Cells[x - 1, y] == null)
                        {
                            if (verticalRightBorderTransform1 == null)
                            {
                                var border = Instantiate(_borderDataCollection.GetBorderByType(BorderSpriteType.VerticalLeft), _borderParent);
                                verticalRightBorderTransform1 = border.GetComponent<RectTransform>();
                                verticalRightBorderTransform1.localPosition = board.Cells[x, y].WorldPosition + new Vector3(-tileSize.x / 2, tileSize.y / 2, 0);

                                verticalRightBorderTransform1.sizeDelta = new Vector2(verticalRightBorderTransform1.sizeDelta.x, tileSize.y);
                                _borders.Add(border);
                            }
                            else
                            {
                                verticalRightBorderTransform1.sizeDelta += new Vector2(0, tileSize.y);
                            }
                        }
                        else
                        {
                            verticalRightBorderTransform1 = null;
                        }


                        if (x == board.Width - 1)
                        {
                            if (verticalLeftBorderTransform2 == null)
                            {
                                var border = Instantiate(_borderDataCollection.GetBorderByType(BorderSpriteType.VerticalRigth), _borderParent);
                                verticalLeftBorderTransform2 = border.GetComponent<RectTransform>();
                                verticalLeftBorderTransform2.localPosition = board.Cells[x, y].WorldPosition + new Vector3(tileSize.x / 2, tileSize.y / 2, 0);

                                verticalLeftBorderTransform2.sizeDelta = new Vector2(verticalLeftBorderTransform2.sizeDelta.x, tileSize.y);
                                _borders.Add(border);
                            }
                            else
                            {
                                verticalLeftBorderTransform2.sizeDelta += new Vector2(0, tileSize.y);
                            }
                        }

                        if (/*x > 0 && */x < board.Width - 1 && board.Cells[x + 1, y] == null)
                        {
                            if (verticalRightBorderTransform2 == null)
                            {
                                var border = Instantiate(_borderDataCollection.GetBorderByType(BorderSpriteType.VerticalRigth), _borderParent);
                                verticalRightBorderTransform2 = border.GetComponent<RectTransform>();
                                verticalRightBorderTransform2.localPosition = board.Cells[x, y].WorldPosition + new Vector3(tileSize.x / 2, tileSize.y / 2, 0);

                                verticalRightBorderTransform2.sizeDelta = new Vector2(verticalRightBorderTransform2.sizeDelta.x, tileSize.y);
                                _borders.Add(border);
                            }
                            else
                            {
                                verticalRightBorderTransform2.sizeDelta += new Vector2(0, tileSize.y);
                            }
                        }
                        else
                        {
                            verticalRightBorderTransform2 = null;
                        }
                    }
                    else
                    {
                        verticalLeftBorderTransform1 = null;
                        verticalLeftBorderTransform2 = null;
                        verticalRightBorderTransform1 = null;
                        verticalRightBorderTransform2 = null;
                    }
                }
            }

            //********************************************************************************************************************************************************************

            RectTransform horisontalTopBorderTransform1 = null;
            RectTransform horisontalBottomBorderTransform1 = null;
            RectTransform horisontalTopBorderTransform2 = null;
            RectTransform horisontalBottomBorderTransform2 = null;

            for (var y = 0; y < board.Height; y++)
            {

                horisontalTopBorderTransform1 = null;
                horisontalBottomBorderTransform1 = null;
                horisontalTopBorderTransform2 = null;
                horisontalBottomBorderTransform2 = null;

                for (var x = 0; x < board.Width; x++)
                {
                    if (board.Cells[x, y] != null)
                    {
                        if (tileSize == Vector2.zero)
                        {
                            tileSize = board.Cells[x, y].View.GetComponent<RectTransform>().sizeDelta;
                        }

                        if (y == 0)
                        {
                            if (horisontalTopBorderTransform1 == null)
                            {
                                var border = Instantiate(_borderDataCollection.GetBorderByType(BorderSpriteType.HorizontalTop), _borderParent);
                                horisontalTopBorderTransform1 = border.GetComponent<RectTransform>();
                                horisontalTopBorderTransform1.localPosition = board.Cells[x, y].WorldPosition + new Vector3(-tileSize.x / 2, tileSize.y / 2, 0);

                                horisontalTopBorderTransform1.sizeDelta = new Vector2(tileSize.x, horisontalTopBorderTransform1.sizeDelta.y);
                                _borders.Add(border);

                            }
                            else
                            {
                                horisontalTopBorderTransform1.sizeDelta += new Vector2(tileSize.x, 0);
                            }
                        }

                        if (y > 0 && board.Cells[x, y - 1] == null)
                        {
                            if (horisontalBottomBorderTransform1 == null)
                            {
                                var border = Instantiate(_borderDataCollection.GetBorderByType(BorderSpriteType.HorizontalTop), _borderParent);
                                horisontalBottomBorderTransform1 = border.GetComponent<RectTransform>();
                                horisontalBottomBorderTransform1.localPosition = board.Cells[x, y].WorldPosition + new Vector3(-tileSize.x / 2, tileSize.y / 2, 0);
                                horisontalBottomBorderTransform1.sizeDelta = new Vector2(tileSize.x, horisontalBottomBorderTransform1.sizeDelta.y);
                                _borders.Add(border);

                            }
                            else
                            {
                                horisontalBottomBorderTransform1.sizeDelta += new Vector2(tileSize.x, 0);
                            }
                        }
                        else
                        {
                            horisontalBottomBorderTransform1 = null;
                        }


                        if (/*y > 0 &&*/ y < board.Height - 1 && board.Cells[x, y + 1] == null)
                        {
                            if (horisontalTopBorderTransform2 == null)
                            {
                                var border = Instantiate(_borderDataCollection.GetBorderByType(BorderSpriteType.HorizontalBottom), _borderParent);
                                horisontalTopBorderTransform2 = border.GetComponent<RectTransform>();
                                horisontalTopBorderTransform2.localPosition = board.Cells[x, y].WorldPosition + new Vector3(-tileSize.x / 2, -tileSize.y / 2, 0);

                                horisontalTopBorderTransform2.sizeDelta = new Vector2(tileSize.x, horisontalTopBorderTransform2.sizeDelta.y);
                                _borders.Add(border);
                            }
                            else
                            {
                                horisontalTopBorderTransform2.sizeDelta += new Vector2(tileSize.x, 0);
                            }
                        }
                        else
                        {
                            horisontalTopBorderTransform2 = null;
                        }


                        if (y == board.Height - 1)
                        {
                            if (horisontalBottomBorderTransform2 == null)
                            {
                                var border = Instantiate(_borderDataCollection.GetBorderByType(BorderSpriteType.HorizontalBottom), _borderParent);
                                horisontalBottomBorderTransform2 = border.GetComponent<RectTransform>();
                                horisontalBottomBorderTransform2.localPosition = board.Cells[x, y].WorldPosition + new Vector3(-tileSize.x / 2, -tileSize.y / 2, 0);

                                horisontalBottomBorderTransform2.sizeDelta = new Vector2(tileSize.x, horisontalBottomBorderTransform2.sizeDelta.y);
                                _borders.Add(border);
                            }
                            else
                            {
                                horisontalBottomBorderTransform2.sizeDelta += new Vector2(tileSize.x, 0);
                            }
                        }
                    }
                    else
                    {
                        horisontalTopBorderTransform1 = null;
                        horisontalBottomBorderTransform1 = null;
                        horisontalTopBorderTransform2 = null;
                        horisontalBottomBorderTransform2 = null;
                    }
                }
            }

            //********************************************************************************************************************************************************************

            for (var y = 0; y < board.Height; y++)
            {
                for (var x = 0; x < board.Width; x++)
                {
                    if (board.Cells[x, y] != null)
                    {
                        if ((x == 0 && y == 0) ||
                            (x > 0 && y == 0 && board.Cells[x - 1, y] == null) ||
                            (x == 0 && y > 0 && board.Cells[x, y - 1] == null) ||
                            (x > 0 && y > 0 && board.Cells[x - 1, y] == null && board.Cells[x, y - 1] == null && board.Cells[x - 1, y - 1] == null))
                        {
                            var border = Instantiate(_borderDataCollection.GetBorderByType(BorderSpriteType.InTopLeftCorner), _borderParent);
                            verticalRightBorderTransform2 = border.GetComponent<RectTransform>();
                            verticalRightBorderTransform2.localPosition = board.Cells[x, y].WorldPosition + new Vector3(-tileSize.x / 2 + 1.6f, tileSize.y / 2, 0);
                            _borders.Add(border);
                        }

                        /*********/

                        if ((x == board.Width - 1 && y == 0) ||
                            (y == 0 && x < board.Width - 1 && board.Cells[x + 1, y] == null) ||
                            (x == board.Width - 1 && y > 0 && board.Cells[x, y - 1] == null) ||
                            (x > 0 && x < board.Width - 1 && y > 0 && board.Cells[x + 1, y] == null && board.Cells[x, y - 1] == null && board.Cells[x + 1, y - 1] == null))
                        {
                            var border = Instantiate(_borderDataCollection.GetBorderByType(BorderSpriteType.InTopRightCorner), _borderParent);
                            verticalRightBorderTransform2 = border.GetComponent<RectTransform>();
                            verticalRightBorderTransform2.localPosition = board.Cells[x, y].WorldPosition + new Vector3(tileSize.x / 2 - 1.6f, tileSize.y / 2, 0);
                            _borders.Add(border);
                        }

                        /*********/

                        if ((x == 0 && y == board.Height - 1) ||
                            (x > 0 && y == board.Height - 1 && board.Cells[x - 1, y] == null) ||
                            (x == 0 && y < board.Height - 1 && board.Cells[x, y + 1] == null) ||
                            (x > 0 && y < board.Height - 1 && board.Cells[x - 1, y] == null && board.Cells[x, y + 1] == null && board.Cells[x - 1, y + 1] == null))
                        {
                            var border = Instantiate(_borderDataCollection.GetBorderByType(BorderSpriteType.InBottomLeftCorner), _borderParent);
                            verticalRightBorderTransform2 = border.GetComponent<RectTransform>();
                            verticalRightBorderTransform2.localPosition = board.Cells[x, y].WorldPosition + new Vector3(-tileSize.x / 2 + 1.6f, -tileSize.y / 2, 0);
                            _borders.Add(border);
                        }

                        /*********/

                        if ((x == board.Width - 1 && y == board.Width - 1) ||
                            (x < board.Width - 1 && y == board.Width - 1 && board.Cells[x + 1, y] == null) ||
                            (x == board.Width - 1 && y < board.Width - 1 && board.Cells[x, y + 1] == null) ||
                            (x < board.Width - 1 && y < board.Width - 1 && board.Cells[x + 1, y] == null && board.Cells[x, y + 1] == null && board.Cells[x + 1, y + 1] == null))
                        {
                            var border = Instantiate(_borderDataCollection.GetBorderByType(BorderSpriteType.InBottomRightCorner), _borderParent);
                            verticalRightBorderTransform2 = border.GetComponent<RectTransform>();
                            verticalRightBorderTransform2.localPosition = board.Cells[x, y].WorldPosition + new Vector3(tileSize.x / 2 - 1.6f, -tileSize.y / 2, 0);
                            _borders.Add(border);
                        }

                        /******************************************************************************/

                        if (x > 0 && y < board.Height - 1 && board.Cells[x - 1, y + 1] != null && board.Cells[x, y + 1] == null)
                        {
                            var border = Instantiate(_borderDataCollection.GetBorderByType(BorderSpriteType.OutTopLeftCorner), _borderParent);
                            verticalRightBorderTransform2 = border.GetComponent<RectTransform>();
                            verticalRightBorderTransform2.localPosition = board.Cells[x, y].WorldPosition + new Vector3(-tileSize.x / 2 - 0.5f, -tileSize.y / 2 - 1.5f, 0);
                            _borders.Add(border);
                        }

                        if (x < board.Width - 1 && y < board.Height - 1 && board.Cells[x, y + 1] == null && board.Cells[x + 1, y + 1] != null)
                        {
                            var border = Instantiate(_borderDataCollection.GetBorderByType(BorderSpriteType.OutTopRightCorner), _borderParent);
                            verticalRightBorderTransform2 = border.GetComponent<RectTransform>();
                            verticalRightBorderTransform2.localPosition = board.Cells[x, y].WorldPosition + new Vector3(tileSize.x / 2 + 0.5f, -tileSize.y / 2 - 1.5f, 0);
                            _borders.Add(border);
                        }

                        if (x < board.Width - 1 && y > 0 && board.Cells[x, y - 1] == null && board.Cells[x + 1, y - 1] != null)
                        {
                            var border = Instantiate(_borderDataCollection.GetBorderByType(BorderSpriteType.OutBottomRightCorner), _borderParent);
                            verticalRightBorderTransform2 = border.GetComponent<RectTransform>();
                            verticalRightBorderTransform2.localPosition = board.Cells[x, y].WorldPosition + new Vector3(tileSize.x / 2 + 0.5f, tileSize.y / 2 + 1.5f, 0);
                            _borders.Add(border);
                        }

                        if (x > 0 && y > 0 && board.Cells[x, y - 1] == null && board.Cells[x - 1, y - 1] != null)
                        {
                            var border = Instantiate(_borderDataCollection.GetBorderByType(BorderSpriteType.OutBottomLeftCorner), _borderParent);
                            verticalRightBorderTransform2 = border.GetComponent<RectTransform>();
                            verticalRightBorderTransform2.localPosition = board.Cells[x, y].WorldPosition + new Vector3(-tileSize.x / 2 - 0.5f, tileSize.y / 2 + 1.5f, 0);
                            _borders.Add(border);
                        }
                    }
                }
            }
        }
    }
    #endregion


    public abstract class BaseElementData
    {
        public abstract ElementType elementType { get; }
        public BaseElementView View { get; set; }
    }

    public class PieceData: BaseElementData
    {
        public int id;
        public override ElementType elementType => ElementType.Piece;

        public static bool operator ==(PieceData data1, PieceData data2)
        {

            if ((object)data1 == null || (object)data2 == null)
                return false;

            return data1.id == data2.id;
        }

        public static bool operator !=(PieceData data1, PieceData data2)
        {
            if ((object)data1 == null || (object)data2 == null)
                return true;

            return data1.id != data2.id;
        }
    }

    public interface ICellData
    {
        int PosX { get; }
        int PosY { get; }
        public CellView View { get; }
        public bool IsHasPiece { get; }
        public PieceData PieceData { get; set; }
        public Vector3 WorldPosition { get; }
    }

    public class CellData : ICellData
    {
        public int PosX { get; private set; }
        public int PosY { get; private set; }
        public CellView View { get; private set; }
        public Vector3 WorldPosition { get; private set; }
        public PieceData PieceData { get; set; }
        public bool IsHasPiece => PieceData != null;

        public CellData(int boardPosX, int boardPosY, CellView view, Vector3 worldPosition)
        {
            PosX = boardPosX;
            PosY = boardPosY;
            View = view;
            WorldPosition = worldPosition;
        }
    }

    public interface IBoardData 
    {
        int Width { get; }
        int Height { get; }
        ICellData[,] Cells { get; }
        bool IsBoardRange(int columnIndex, int rowIndex);
        ICellData GetCellData(int columnIndex, int rowIndex);
        ICellData GetNeighboringCell(int currentColumnIndex, int currentRowIndex, PieceMoveDireciton direction);
        ICellData GetCellDataByElement(BaseElementView element);
    }

    public class BoardData : IBoardData
    {
        private int _width;
        private int _height;
        private ICellData[,] _cells;

        public BoardData(int width, int height)
        {
            _width = width;
            _height = height;

            _cells = new CellData[width, height];
        }

        public int Width => _width;
        public int Height => _height;
        public ICellData[,] Cells => _cells;

        public bool IsBoardRange(int columnIndex, int rowIndex)
        {
            return columnIndex >= 0 && columnIndex < _width && rowIndex >= 0 && rowIndex < _height;
        }

        public ICellData GetCellData(int columnIndex, int rowIndex)
        {
            return _cells[columnIndex, rowIndex];
        }

        public ICellData GetNeighboringCell(int currentColumnIndex, int currentRowIndex, PieceMoveDireciton direction)
        {
            ICellData cellData = null;

            if (direction == PieceMoveDireciton.Up)
            {
                if(IsBoardRange(currentColumnIndex, currentRowIndex - 1))
                {
                    cellData = GetCellData(currentColumnIndex, currentRowIndex - 1);
                }
            }
            else if (direction == PieceMoveDireciton.Down)
            {
                if(IsBoardRange(currentColumnIndex, currentRowIndex + 1))
                {
                    cellData = GetCellData(currentColumnIndex, currentRowIndex + 1);
                }
            }
            else if (direction == PieceMoveDireciton.Left)
            {
                if (IsBoardRange(currentColumnIndex - 1, currentRowIndex))
                {
                    cellData = GetCellData(currentColumnIndex - 1, currentRowIndex);
                }
            }
            else if (direction == PieceMoveDireciton.Right)
            {
                if (IsBoardRange(currentColumnIndex + 1, currentRowIndex))
                {
                    cellData = GetCellData(currentColumnIndex + 1, currentRowIndex);
                }
            }

            return cellData;
        }

        public ICellData GetCellDataByElement(BaseElementView element)
        {
            for (var rowIndex = 0; rowIndex < _width; rowIndex++)
            {
                for (var columnIndex = 0; columnIndex < _width; columnIndex++)
                {
                    if (_cells[columnIndex, rowIndex].PieceData.View == element)
                    {
                        return _cells[columnIndex, rowIndex];
                    }
                }
            }

            return null;
        }
    }
}
