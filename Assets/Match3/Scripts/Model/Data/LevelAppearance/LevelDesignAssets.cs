using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelDesignAssets", menuName = "Match3/LevelDesignAssets")]
public class LevelDesignAssets : ScriptableObject
{
    public LevelDesignType type;
    public Sprite cellBackground;

    [Header("Border")]
    public Sprite horizontalTop;
    public Sprite horizontalBottom;
    public Sprite verticalLeft;
    public Sprite verticalRigth;
    public Sprite inTopLeftCorner;
    public Sprite inTopRightCorner;
    public Sprite inBottomLeftCorner;
    public Sprite inBottomRightCorner;
    public Sprite outTopLeftCorner;
    public Sprite outTopRightCorner;
    public Sprite outBottomLeftCorner;
    public Sprite outBottomRightCorner;
}
