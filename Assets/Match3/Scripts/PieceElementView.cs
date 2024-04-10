using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;
using UnityEngine.EventSystems;
using System;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;

namespace Match3 
{

    public abstract class PieceElementView : BaseElementView, IPointerDownHandler, IPointerUpHandler
    {
        [SerializeField] private float _movementDuration = 0.15f;

        private bool _mouseDown;
        private Vector2 _firstMousePosition;
        private Vector2 _tileSize;

        private void Awake()
        {
            _tileSize = GetComponent<RectTransform>().sizeDelta;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            _mouseDown = true;

            _firstMousePosition = new Vector2(transform.position.x, transform.position.y);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            _mouseDown = false;
        }

        private void Update()
        {
            if (_mouseDown)
            {
                var currentDistance = Vector2.Distance(_firstMousePosition, Input.mousePosition);
                if ((Input.GetMouseButtonUp(0) && currentDistance > _tileSize.x / 2) || currentDistance > _tileSize.x)
                {
                    _mouseDown = false;

                    GetDirection();
                }
            }
        }
        private void GetDirection()
        {
            Vector2 lastMousePosition = Input.mousePosition;

            var direction = (_firstMousePosition - lastMousePosition).normalized;

            if (direction.y > 0 && Mathf.Abs(direction.y) > Mathf.Abs(direction.x))
            {
                LevelEvents.RaiseOnElementMove(this, PieceMoveDireciton.Down);
            }
            else if (direction.y < 0 && Mathf.Abs(direction.y) > Mathf.Abs(direction.x))
            {
                LevelEvents.RaiseOnElementMove(this, PieceMoveDireciton.Up);
            }
            else if (direction.x > 0 && Mathf.Abs(direction.y) < Mathf.Abs(direction.x))
            {
                LevelEvents.RaiseOnElementMove(this, PieceMoveDireciton.Left);
            }
            else if (direction.x < 0 && Mathf.Abs(direction.y) < Mathf.Abs(direction.x))
            {
                LevelEvents.RaiseOnElementMove(this, PieceMoveDireciton.Right);
            }
        }

        

        /*
        public UniTask MoveToParentTile()
        {
            return transform.DOLocalMove(_parentCell.WorldPosition, 0.15f).SetEase(Ease.Linear).AsyncWaitForCompletion().AsUniTask();
        }

        public UniTask MoveToParent(Vector3[] path)
        {

            Sequence sequence = DOTween.Sequence();
            sequence.Append(transform.DOLocalPath(path, _movementDuration * 2, PathType.Linear).SetEase(Ease.InSine));
            //sequence.Append(transform.DOLocalMove(_parentTile.tileView.transform.localPosition, _movementDuration * 2).SetSpeedBased().SetEase(Ease.InSine));
            //sequence.Append(transform.DOScaleX(1.1f, 0.08f)).Join(transform.DOScaleY(0.95f, 0.08f)).Join(transform.DOLocalMoveY(_parentTile.tileView.transform.localPosition.y - 0.5f, 0.08f));
            //sequence.Append(transform.DOScaleX(1.0f, 0.09f)).Join(transform.DOScaleY(1.0f, 0.09f)).Join(transform.DOLocalMoveY(_parentTile.tileView.transform.localPosition.y, 0.09f));
            return sequence.AsyncWaitForCompletion().AsUniTask();
        }

        public override UniTask HideAndDestroy()
        {
            return transform.DOScale(Vector3.zero, 0.15f).SetEase(Ease.Linear).AsyncWaitForCompletion().AsUniTask();
        }
        */
    }
}


