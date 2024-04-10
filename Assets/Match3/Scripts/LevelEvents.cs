using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Match3
{
    public static class LevelEvents
    {
        public static event Action<BaseElementView, PieceMoveDireciton> OnElementMove;

        public static void RaiseOnElementMove(BaseElementView element, PieceMoveDireciton moveDireciton)
        {
            OnElementMove?.Invoke(element, moveDireciton);
        }
    }
}

