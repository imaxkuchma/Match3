using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Match3
{
    public class MatchChecker : MonoBehaviour
    {
        [SerializeField] private int _matchCount = 3;
        public List<MatchGroup> CheckMatch(IBoardData board, ICellData CellData = null)
        {
            var horizontalMatchGroups = CheckMatchHorizontal(board);
            var verticalMatchGroups = CheckMatchVertical(board);

            var multiGroupList = new List<List<List<ICellData>>>();

            for (var y = 0; y < board.Height; y++)
            {
                for (var x = 0; x < board.Width; x++)
                {
                    if (board.Cells[x, y] != null && board.Cells[x, y].PieceData != null)
                    {
                        var horizontalMatchGroup = horizontalMatchGroups.Find(g => g.Contains(board.Cells[x, y]));
                        var verticalMatchGroup = verticalMatchGroups.Find(g => g.Contains(board.Cells[x, y]));

                        if (horizontalMatchGroup != null && verticalMatchGroup != null)
                        {
                            List<List<ICellData>> groupList = null;

                            groupList = multiGroupList.Find(g => g.Contains(horizontalMatchGroup));
                            if (groupList != null)
                            {
                                groupList.Add(verticalMatchGroup);
                            }
                            else
                            {
                                groupList = multiGroupList.Find(g => g.Contains(verticalMatchGroup));
                                if (groupList != null)
                                {
                                    groupList.Add(horizontalMatchGroup);
                                }
                            }

                            if (groupList == null)
                            {
                                var group = new List<List<ICellData>>();
                                group.Add(horizontalMatchGroup);
                                group.Add(verticalMatchGroup);
                                multiGroupList.Add(group);
                            }
                        }
                    }
                }
            }

            for (var i = 0; i < horizontalMatchGroups.Count; i++)
            {
                var group = multiGroupList.Find(g => g.Contains(horizontalMatchGroups[i]));
                if (group == null)
                {
                    group = new List<List<ICellData>>();
                    group.Add(horizontalMatchGroups[i]);
                    multiGroupList.Add(group);
                }
            }

            for (var i = 0; i < verticalMatchGroups.Count; i++)
            {
                var group = multiGroupList.Find(g => g.Contains(verticalMatchGroups[i]));
                if (group == null)
                {
                    group = new List<List<ICellData>>();
                    group.Add(verticalMatchGroups[i]);
                    multiGroupList.Add(group);
                }
            }

            var matchGroupList = new List<MatchGroup>();

            foreach (List<List<ICellData>> group in multiGroupList)
            {
                var matchGroup = new MatchGroup();
                matchGroup.isMultiGroup = group.Count > 1;
                foreach (List<ICellData> multiGroup in group)
                {
                    foreach (ICellData cell in multiGroup)
                    {
                        if (!matchGroup.cellsList.Contains(cell))
                        {
                            matchGroup.cellsList.Add(cell);
                        }
                        else
                        {
                            matchGroup.sharedCellsList.Add(cell);
                        }
                    }
                }
                matchGroup.elementId = matchGroup.cellsList[0].PieceData.id;
                matchGroupList.Add(matchGroup);
            }

            return matchGroupList;
        }

        private List<List<ICellData>> CheckMatchHorizontal(IBoardData board)
        {
            var matchGroupList = new List<List<ICellData>>();

            for (var y = 0; y < board.Height; y++)
            {
                PieceData lastPieceData = null;
                var tempMatchGroup = new List<ICellData>();

                for (var x = 0; x < board.Width; x++)
                {

                    if (board.Cells[x, y] == null)
                    {
                        lastPieceData = null;
                        if (tempMatchGroup.Count >= _matchCount)
                        {
                            matchGroupList.Add(tempMatchGroup);
                        }
                        tempMatchGroup = new List<ICellData>();

                        continue;
                    }

                    if (board.Cells[x, y].PieceData == null)
                    {
                        lastPieceData = null;
                        if (tempMatchGroup.Count >= _matchCount)
                        {
                            matchGroupList.Add(tempMatchGroup);

                        }
                        tempMatchGroup = new List<ICellData>();

                        continue;
                    }

                    if (board.Cells[x, y].PieceData == lastPieceData)
                    {
                        tempMatchGroup.Add(board.Cells[x, y]);
                    }
                    else
                    {
                        if (tempMatchGroup.Count >= _matchCount)
                        {
                            matchGroupList.Add(tempMatchGroup);
                        }
                        tempMatchGroup = new List<ICellData>();

                        lastPieceData = board.Cells[x, y].PieceData;
                        tempMatchGroup.Add(board.Cells[x, y]);
                    }

                    if (x == board.Width - 1 && tempMatchGroup.Count >= _matchCount)
                    {
                        matchGroupList.Add(tempMatchGroup);
                    }
                }
            }

            return matchGroupList;
        }

        private List<List<ICellData>> CheckMatchVertical(IBoardData board)
        {
            var matchGroupList = new List<List<ICellData>>();

            for (var x = 0; x < board.Width; x++)
            {
                PieceData lastPieceData = null;
                var tempMatchGroup = new List<ICellData>();

                for (var y = 0; y < board.Height; y++)
                {

                    if (board.Cells[x, y] == null)
                    {
                        lastPieceData = null;
                        if (tempMatchGroup.Count >= _matchCount)
                        {
                            matchGroupList.Add(tempMatchGroup);
                        }
                        tempMatchGroup = new List<ICellData>();

                        continue;
                    }

                    if (board.Cells[x, y].PieceData == null)
                    {
                        lastPieceData = null;
                        if (tempMatchGroup.Count >= _matchCount)
                        {
                            matchGroupList.Add(tempMatchGroup);

                        }
                        tempMatchGroup = new List<ICellData>();
                        continue;
                    }

                    if (board.Cells[x, y].PieceData == lastPieceData)
                    {
                        tempMatchGroup.Add(board.Cells[x, y]);
                    }
                    else
                    {
                        if (tempMatchGroup.Count >= _matchCount)
                        {
                            matchGroupList.Add(tempMatchGroup);

                        }
                        tempMatchGroup = new List<ICellData>();

                        lastPieceData = board.Cells[x, y].PieceData;
                        tempMatchGroup.Add(board.Cells[x, y]);
                    }

                    if (y == board.Height - 1 && tempMatchGroup.Count >= _matchCount)
                    {
                        matchGroupList.Add(tempMatchGroup);
                        tempMatchGroup = new List<ICellData>();
                    }

                }
            }

            return matchGroupList;
        }
    }


    public class MatchGroup
    {       
        public int elementId;
        public bool isMultiGroup;
        public List<ICellData> cellsList;
        public List<ICellData> sharedCellsList;

        public MatchGroup()
        {
            elementId = -1;
            isMultiGroup = false;
            cellsList = new List<ICellData>();
            sharedCellsList = new List<ICellData>();
        }
    }
}

