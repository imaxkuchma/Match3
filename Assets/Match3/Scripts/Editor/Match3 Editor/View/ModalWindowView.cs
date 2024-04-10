using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace Match3.Match3Editor
{
    public class ModalWindowView : VisualElement
    {
        private Label _title;
        private VisualElement _header;
        private VisualElement _contentContainer;

        public ModalWindowView(string windowTitle, VisualElement parentWindow = null)
        {
            var visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/Match3/UIToolkit/ModalWindow.uxml");
            visualTree.CloneTree(this);

            _header = this.Q<VisualElement>("modal-window__header");
            _title = this.Q<Label>("modal-window__header__title");
            _contentContainer = this.Q<VisualElement>("modal-window__container");

            style.position = Position.Absolute;
            style.top = 0;
            style.left = 0;

            _title.text = windowTitle;

            if (parentWindow != null)
            {
                parentWindow.Add(this);
                new ModalWindowDragAndDrop(_header, parentWindow);
            }
        }

        public void AddContent(VisualElement content)
        {
            _contentContainer.Add(content);
        }

        private class ModalWindowDragAndDrop : PointerManipulator
        {
            public ModalWindowDragAndDrop(VisualElement target, VisualElement parent)
            {
                this.target = target;
                root = target.parent;
                this.parent = parent;
            }

            protected override void RegisterCallbacksOnTarget()
            {
                target.RegisterCallback<PointerDownEvent>(PointerDownHandler);
                target.RegisterCallback<PointerMoveEvent>(PointerMoveHandler);
                target.RegisterCallback<PointerUpEvent>(PointerUpHandler);
            }

            protected override void UnregisterCallbacksFromTarget()
            {
                target.UnregisterCallback<PointerDownEvent>(PointerDownHandler);
                target.UnregisterCallback<PointerMoveEvent>(PointerMoveHandler);
                target.UnregisterCallback<PointerUpEvent>(PointerUpHandler);
            }

            private Vector2 targetStartPosition { get; set; }

            private Vector3 pointerStartPosition { get; set; }

            private bool enabled { get; set; }

            private VisualElement root { get; }
            private VisualElement parent { get; }

            private void PointerDownHandler(PointerDownEvent evt)
            {
                targetStartPosition = root.transform.position;
                pointerStartPosition = evt.position;
                target.CapturePointer(evt.pointerId);
                enabled = true;
            }

            private void PointerMoveHandler(PointerMoveEvent evt)
            {
                if (enabled && target.HasPointerCapture(evt.pointerId))
                {
                    Vector3 pointerDelta = evt.position - pointerStartPosition;

                    root.transform.position = new Vector2(
                        Mathf.Clamp(targetStartPosition.x + pointerDelta.x, 0, parent.panel.visualTree.localBound.width - 300),
                        Mathf.Clamp(targetStartPosition.y + pointerDelta.y, 0, parent.panel.visualTree.localBound.height - 400));
                }
            }
            private void PointerUpHandler(PointerUpEvent evt)
            {
                if (enabled && target.HasPointerCapture(evt.pointerId))
                {
                    target.ReleasePointer(evt.pointerId);
                }
            }
        }
    }
}
