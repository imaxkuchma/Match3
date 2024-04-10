using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BorderDataCollection", menuName = "Match3/BorderDataCollection")]
public class BorderDataCollection : ScriptableObject
{
    [SerializeField] private List<BorderObjectItem> _sprites;

    public List<BorderObjectItem> Sprites => _sprites;

    public GameObject GetBorderByType(BorderSpriteType type)
    {
        return _sprites.Find(b => b.type == type).border;
    }
}

[System.Serializable]
public class BorderObjectItem 
{
    public GameObject border;
    public BorderSpriteType type;
}

public enum BorderSpriteType
{
    HorizontalTop,
    HorizontalBottom,
    VerticalLeft,
    VerticalRigth,
    InTopLeftCorner,
    InTopRightCorner,
    InBottomLeftCorner,
    InBottomRightCorner,
    OutTopLeftCorner,
    OutTopRightCorner,
    OutBottomLeftCorner,
    OutBottomRightCorner
}