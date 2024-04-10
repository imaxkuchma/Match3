using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Cysharp.Threading.Tasks;
using System.Threading;

namespace Match3
{
    public class LevelController : MonoBehaviour
    {
        [SerializeField] private Match3Database _database;

        [SerializeField] private int _levelIndex = 0;

        private BoardBuilder _boardBuilder;
        private BoardFiller _boardFiller;
        private MatchChecker _matchChecker;

        private IBoardData _board;

        private void Awake()
        {
            Application.targetFrameRate = 60;

            _boardBuilder = GetComponent<BoardBuilder>();
            _boardFiller = GetComponent<BoardFiller>();
            _matchChecker = GetComponent<MatchChecker>();

            LevelEvents.OnElementMove += OnElementMove;
        }

        private void OnDestroy()
        {
            LevelEvents.OnElementMove -= OnElementMove;
        }

        private void Start()
        {
            BuildLevel();
        }

        public void BuildLevel()
        {
            var currentLevel = _database.levels[_levelIndex];

            _board = _boardBuilder.BuildBoard(currentLevel);
#pragma warning disable 4014
            _boardFiller.FillBoard(_board);
#pragma warning restore 4014

            //_matchChecker.CheckMatch(_board);
        }


        private async void OnElementMove(BaseElementView senderElement, PieceMoveDireciton direction)
        {
            ICellData currentElement = _board.GetCellDataByElement(senderElement);
            ICellData neighboringCellData = _board.GetNeighboringCell(currentElement.PosX, currentElement.PosY, direction);

            if (neighboringCellData != null && neighboringCellData.IsHasPiece)
            {
                await Swap(currentElement, neighboringCellData);

                var resultCheck = await CheckMatch(_board, currentElement);

                if (!resultCheck)
                {
                    await Swap(neighboringCellData, currentElement);
                }
                else
                {
                    while (resultCheck)
                    {
                        await _boardFiller.FillBoard(_board, true);
                        resultCheck = await CheckMatch(_board, currentElement);
                    }
                }
            }
        }

        private async UniTask Swap(ICellData senderCellData, ICellData neighboringCellData, CancellationToken cancellationToken = default)
        {
            var senderElementData = senderCellData.PieceData;
            var neighboringElementData = neighboringCellData.PieceData;

            senderCellData.PieceData = neighboringElementData;
            neighboringCellData.PieceData = senderElementData;



            //var senderMoveTask = senderCellData.PieceData.View.MoveToParentTile();
            //var neighboringMoveTask = neighboringCellData.PieceData.View.MoveToParentTile();
            /*
            await DOTween.Sequence()
                .Join(senderCellData.PieceData.View.transform.DOLocalMove(senderCellData.WorldPosition, 2))
                .Join(senderCellData.PieceData.View.transform.DOLocalMove(senderCellData.WorldPosition, 2))
                .SetEase(Ease.Flash)
                .WithCancellation(cancellationToken);*/
        }

        private async UniTask<bool> CheckMatch(IBoardData board, ICellData CellData)
        {
            var matchGroups = _matchChecker.CheckMatch(board, CellData);

            if (matchGroups.Count > 0)
            {
                foreach (MatchGroup matchGroup in matchGroups)
                {
                    Debug.Log($"Element Id: {matchGroup.elementId}; IsMultiGroup {matchGroup.isMultiGroup}; Shared tile count: {matchGroup.sharedCellsList.Count}; Cells count: {matchGroup.cellsList.Count};");

                    foreach (CellData tile in matchGroup.sharedCellsList)
                    {

                    }

                    List<UniTask> hideTask = new List<UniTask>();
                    foreach (ICellData cell in matchGroup.cellsList)
                    {
                        if (!matchGroup.sharedCellsList.Contains(cell))
                        {
#pragma warning disable 4014
                            //cell.PieceData.View.HideAndDestroy();
#pragma warning restore 4014

                            cell.PieceData = null;
                        }
                    }
                    await UniTask.WhenAll(hideTask.ToArray());
                }
                return true;
            }
            else
            {
                return false;
            }
        }
    }

}



