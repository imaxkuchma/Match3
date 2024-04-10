using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace Match3.Match3Editor
{
    public class ElementListEntryController : MonoBehaviour
    {
        private VisualElement _elementImage;
        private Label _elementName;

        public void SetVisualElement(VisualElement visualElement)
        {
            _elementImage = visualElement.Q<VisualElement>("element-entry__image");
            _elementName = visualElement.Q<Label>("element-entry__label");
        }

        public void SetCharacterData(ElementData elementData)
        {
            _elementImage.style.backgroundImage = elementData.sprite.texture;
            _elementName.text = elementData.elementName;
        }
    }
}

