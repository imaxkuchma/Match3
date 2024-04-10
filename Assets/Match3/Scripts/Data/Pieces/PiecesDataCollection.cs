using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Match3/PiecesDataCollection", fileName = "PiecesDataCollection")]
public class PiecesDataCollection : ScriptableObject
{
    [SerializeField] private List<Sprite> _piecesList;

    public PieceAssetData GetRandomPieceData()
    {
        var randomPieceId = Random.Range(0, _piecesList.Count);
        return new PieceAssetData(randomPieceId, _piecesList[randomPieceId]);
    }
    public PieceAssetData GetPieceDataByIndex(int index)
    {
        return new PieceAssetData(index, _piecesList[index]);
    }
}

public struct PieceAssetData
{
    public PieceAssetData(int id, Sprite sprite)
    {
        this.id = id;
        this.sprite = sprite;
    }
    public int id;
    public Sprite sprite;
}