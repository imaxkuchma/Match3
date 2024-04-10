using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Match3
{
    [CreateAssetMenu(fileName = "ElementsDataColletion", menuName = "Match3/ElementsDataColletion")]
    public class ElementsDataColletion : ScriptableObject
    {
        private static ElementsDataColletion _instance;
        public static ElementsDataColletion Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = Resources.Load<ElementsDataColletion>("Match3/Elements/ElementsDataColletion");
                }
                return _instance;
            }
        }

        public List<ElementData> elements;

        public List<ElementData> GetElementsDataByType(ElementType type)
        {
            var element = elements.FindAll(e => e.type == type);
            return element;
        }
    }
}

