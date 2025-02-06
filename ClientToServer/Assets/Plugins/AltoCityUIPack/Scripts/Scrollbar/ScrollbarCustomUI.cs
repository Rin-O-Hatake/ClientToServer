using UnityEngine;
using UnityEngine.UI;

namespace Plugins.AltoCityUIPack.Scripts.Scrollbar
{
    public class ScrollbarCustomUI : MonoBehaviour
    {
        #region Field

        [SerializeField] private Color _scrollbarBackgroundColor;
        [SerializeField] private Color _scrollbarHandleColor;
        [SerializeField] private Image _scrollbarBackgroundImage;
        [SerializeField] private Image _scrollbarHandleImage;
        
        [SerializeField] private UnityEngine.UI.Scrollbar _currentScrollbar;
        [SerializeField] private Color _normalColorScrollbar;
        
        public ScrollRect _scrollRect;
        public ScrollRect.MovementType _movementType;
        [Range(0,200)] public float _scrollSensitivity;
        
        public int latestTabIndex = 0;

        #endregion

        public void UpdateUI()
        {
            UpdateImagesScrollbar();
            
            if (!_scrollRect)
            {
                return;
            }
            
            _scrollRect.movementType = _movementType;
            _scrollRect.scrollSensitivity = _scrollSensitivity;

            if (!_currentScrollbar)
            {
                return;
            }

            ColorBlock colorBlock = _currentScrollbar.colors;
            colorBlock.normalColor = _normalColorScrollbar;
            _currentScrollbar.colors = colorBlock;
        }

        private void UpdateImagesScrollbar()
        {
            if (!_scrollbarBackgroundImage || !_scrollbarHandleImage)
            {
                return;
            }
            
            _scrollbarBackgroundImage.color = _scrollbarBackgroundColor;
            _scrollbarHandleImage.color = _scrollbarHandleColor;
        }
    }
}
