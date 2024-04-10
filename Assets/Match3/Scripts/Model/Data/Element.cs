using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Match3 
{
    [System.Serializable]
    public class Element
    {
        public ElementType type;

        public Element(ElementType elementType)
        {
            this.type = elementType;
        }
    }
}