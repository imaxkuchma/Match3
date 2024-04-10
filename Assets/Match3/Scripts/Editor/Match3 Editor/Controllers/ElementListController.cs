using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace Match3.Match3Editor
{
    public class ElementListController
    {
        public VisualElement view;
        private VisualTreeAsset _elementListTemplate;
        private VisualTreeAsset _elementListEntryTemplate;
        private VisualElement _elementListView;
        private ListView _elementList;

        private List<ElementData> _allElements;


        public event Action<ElementData> OnElementSelect;

        public ElementListController()
        {
            _elementListTemplate = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/Match3/UIToolkit/ElementList.uxml");
            _elementListEntryTemplate = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/Match3/UIToolkit/ElementListEntry.uxml");
        }

        public void InitializeElementList()
        {
            EnumerateAllElements();

            view = _elementListTemplate.Instantiate();

            _elementList = view.Q<ListView>("element-list");

            FillElementList();

            _elementList.selectionChanged += OnElementChanged;
        }

        private void OnElementChanged(IEnumerable<object> obj)
        {
           
            var selectedElement = _elementList.selectedItem as ElementData;

            if (selectedElement == null) return;

            OnElementSelect?.Invoke(selectedElement);
        }

        private void EnumerateAllElements()
        {
            _allElements = new List<ElementData>();
            _allElements.AddRange(ElementsDataColletion.Instance.elements);
        }

        private void FillElementList()
        {
            _elementList.makeItem = () =>
            {
                var newListEntry = _elementListEntryTemplate.Instantiate();
                var newListEntryLogic = new ElementListEntryController();
                newListEntry.userData = newListEntryLogic;
                newListEntryLogic.SetVisualElement(newListEntry);
                return newListEntry;
            };

            _elementList.bindItem = (item, index) =>
            {
                (item.userData as ElementListEntryController).SetCharacterData(_allElements[index]);
            };


            _elementList.fixedItemHeight = 45;

            _elementList.itemsSource = _allElements;
        }
    }
}

