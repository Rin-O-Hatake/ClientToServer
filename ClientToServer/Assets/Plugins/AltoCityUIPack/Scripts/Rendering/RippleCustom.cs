using DG.Tweening;
using UnityEngine;
using Color = UnityEngine.Color;
using Image = UnityEngine.UI.Image;


namespace Plugins.AltoCityUIPack.Scripts.Rendering
{
    public class RippleCustom : MonoBehaviour
    {
        #region Fields
        
        private float _duration;
        private float _maxSize;
        [SerializeField] private Image _image;
        [SerializeField] private CanvasGroup _panelCanvasGroup;
        
        #endregion

        public void Init(Color startColor, float maxSize, float duration)
        {
            _image.color = startColor;
            _maxSize = maxSize;
            _duration = duration;
        }

        private void OnDestroy()
        {
            _panelCanvasGroup.DOKill();
            _image.transform.DOKill();
        }

        public void RippleAnimation(Vector2 position)
        {
            ResetAnimation();
            transform.position = position;
            _panelCanvasGroup.DOFade(0, _duration);
            _image.transform.DOScale(_maxSize, _duration);
        }

        private void ResetAnimation()
        {
            _panelCanvasGroup.DOComplete();
            _image.transform.DOComplete();
            _panelCanvasGroup.alpha = 1;
            _image.transform.localScale = new Vector3(1, 1, 1);
        }
    }
}