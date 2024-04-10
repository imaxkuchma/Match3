using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Match3
{
    [System.Serializable]
    public class LevelGoalElement
    {
        public Element element;
        public int quantity;

        public LevelGoalElement(Element element, int quantity)
        {
            this.element = element;
            this.quantity = quantity;
        }
    }
}

