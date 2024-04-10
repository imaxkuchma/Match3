using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace Match3.Match3Editor
{
    public partial class Match3EditorWindow : EditorWindow
    {

        /*

        private void CanResize(VisualElement element)
        {
           
           var parent = element.parent;
            var resizer = new VisualElement();
            resizer.style.position = Position.Absolute;
            resizer.style.top = element.localBound.yMin;
            EditorGUIUtility.AddCursorRect(new Rect(0, 0, position.width, position.height), MouseCursor.ResizeHorizontal);
            int left = (int)element.style.left.value.value + (int)element.style.width.value.value - (int)10 / 2;
            Debug.Log(left);
            resizer.style.left = left;
            resizer.style.height = (int)element.localBound.height;
            resizer.style.width = 10;
            resizer.AddToClassList("resize-horizontal");
            EditorGUIUtility.AddCursorRect(new Rect(20, 20, 140, 40), MouseCursor.ResizeHorizontal);
            parent.Add(resizer);

            element.parent.RegisterCallback<MouseMoveEvent>((env) => 
            {
                if(env.mousePosition.x >= element.localBound.left+ element.localBound.width - 5 
                && env.mousePosition.x <= element.localBound.left + element.localBound.width + 5)
                {
                    element.style.cursor = new StyleCursor();
                }
                else
                {

                }
            });

            

            
    
        }*/
    }
}
