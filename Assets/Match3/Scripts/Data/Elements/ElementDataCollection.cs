using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Match3/ElementDataCollection", fileName = "ElementDataCollection")]
public class ElementDataCollection : ScriptableObject
{
    [SerializeField] private List<Sprite> _elementList;

    public ElementAssetData GetRandomElementData()
    {
        var randomElementId = Random.Range(0, _elementList.Count);
        return new ElementAssetData(randomElementId, _elementList[randomElementId]);
    }
    public ElementAssetData GetElementDataByIndex(int index)
    {
        return new ElementAssetData(index, _elementList[index]);
    }
}

public struct ElementAssetData 
{
    public ElementAssetData(int id, Sprite sprite)
    {
        this.id = id;
        this.sprite = sprite;
    }
    public int id;
    public Sprite sprite;
}

