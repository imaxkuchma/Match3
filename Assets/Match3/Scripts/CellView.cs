using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CellView : MonoBehaviour
{
    [SerializeField] private Image _background;

    public void Init(Sprite sprite)
    {
        _background.sprite = sprite;
    }
}
