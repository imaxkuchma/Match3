
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;

namespace Match3
{
    public class BoardFiller : MonoBehaviour
    {
        [SerializeField] private PiecesDataCollection _pieceDataCollection;
        [SerializeField] private BaseElementView _pieceViewPrefab;
        [SerializeField] private Transform _piecesParent;

        public async UniTask FillBoard(IBoardData board, bool playMode = false)
        {
            if (!playMode)
            {
                for (var y = 0; y < board.Height; y++)
                {
                    for (var x = 0; x < board.Width; x++)
                    {
                        if (board.Cells[x, y] != null)
                        {
                            var elementAssetData = _pieceDataCollection.GetRandomPieceData();

                            board.Cells[x, y].PieceData = new PieceData();
                            board.Cells[x, y].PieceData.id = elementAssetData.id;
                            board.Cells[x, y].PieceData.View = Instantiate(_pieceViewPrefab, _piecesParent);
                            board.Cells[x, y].PieceData.View.Init(elementAssetData.sprite);
                            board.Cells[x, y].PieceData.View.transform.localPosition = board.Cells[x, y].WorldPosition;
                        }
                    }
                }
            }
            else
            {
                List<UniTask> _fillTask = new List<UniTask>();

                for (var rowIndex = board.Height - 1; rowIndex >= 0; rowIndex--)
                {
                    for (var columnIndex = board.Width - 1; columnIndex >= 0; columnIndex--)
                    {
                        if (board.Cells[columnIndex, rowIndex] != null && board.Cells[columnIndex, rowIndex].PieceData != null)
                        {
                            Vector3[] path;
                            CellData targetCellData;
                            if(CanFallDownPiece(board, columnIndex, rowIndex, out path, out targetCellData))
                            {

                            }
                        }
                    }
                }

                await UniTask.WhenAll(_fillTask.ToArray());
            }
        }


        
        private bool CanFallDownPiece(IBoardData board, int curColumnIndex,  int curRowIndex, out Vector3[] path, out CellData targetCellData)
        {
            path = null;
            targetCellData = null;
            bool canMove = false;


            return canMove;
        }     

        private bool CanMoveDown(IBoardData board, int columnIndex, int rowIndex)
        {
            return true;
        }

        private bool CanMoveDiagonally(IBoardData board)
        {
            return true;
        }
    }
}
