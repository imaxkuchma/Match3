using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Match3
{
    [CreateAssetMenu(fileName = "ElementData", menuName = "Match3/Elements/ElementData")]
    public class ElementData : ScriptableObject
    {
        public ElementType type;
        public string elementName;
        public string description;
        public ElementSize size;
        public Sprite sprite;
        public bool isMoveable; // ���������
        public bool isBlocking; // �����������
        public bool isDestructible; // �����������
        public bool isObstructive; // ��������������
        public bool isReproductive; // ����������������

        public ObstructiveType obstructiveType;

        public ElementData producingElement;
    }

    [System.Serializable]
    public struct ElementSize
    {
        public int height;
        public int width;
    }
}
