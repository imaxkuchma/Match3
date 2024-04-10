using UnityEngine;
using UnityEngine.UI;

namespace Match3
{
    public interface IBaseElementView
    {
        public void Init(Sprite sprite);
    }

    public abstract class BaseElementView : MonoBehaviour, IBaseElementView
    {
        [SerializeField] protected Image _icon;

        public void Init(Sprite sprite)
        {
            _icon.sprite = sprite;
        }


    }
}

