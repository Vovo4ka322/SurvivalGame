using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts.MenuComponents.ShopComponents.Common
{
    [RequireComponent(typeof(RawImage))]
    public class ImageScroller : MonoBehaviour
    {
        [SerializeField][Range(0, 10)] private float _scrollSpeed = 0.1f;
        [SerializeField][Range(-1, 1)] private float _xDirection = 1;
        [SerializeField][Range(-1, 1)] private float _yDirection = 1;

        private RawImage _image;

        private void Awake() => _image = GetComponent<RawImage>();

        private void Update()
            => _image.uvRect = new Rect(_image.uvRect.position + new Vector2(-_xDirection * _scrollSpeed, _yDirection * _scrollSpeed) * Time.deltaTime, _image.uvRect.size);
    }
}