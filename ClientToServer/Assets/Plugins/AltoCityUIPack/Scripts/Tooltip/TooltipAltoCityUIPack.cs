using Plugins.AltoCityUIPack.Scripts.Dropdown;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Plugins.AltoCityUIPack.Scripts.Tooltip
{
    public class TooltipAltoCityUIPack : MonoBehaviour
    {
        [SerializeField] private TMP_Text _titleText;
        [SerializeField] private TMP_Text _descriptionText;

        [SerializeField] private Image _backgroundImage;
        [SerializeField] private RectTransform _rootTooltip;

        public void SetText(string description, string title = "")
        {
            if (string.IsNullOrEmpty(title))
            {
                _titleText.gameObject.SetActive(false);
            }
            else
            {
                _titleText.gameObject.SetActive(true);
                _titleText.text = title;
            }

            _descriptionText.text = description;
        }

        public void InitTooltip(TooltipItem item, float maxWidthToolTip)
        {
            _rootTooltip.sizeDelta = new Vector2(maxWidthToolTip, 0);
            UpdateTitle(item.colorTitle, item.sizeTitle, item._allFont, item._allTextAlignment);
            UpdateDescription(item.colorDescription, item.sizeDescription, item._allFont, item._allTextAlignment);
            UpdateBackground(item.backgroundSprite, item.backgroundColor, item._pixelsPerUnitMultiplier);
        }

        #region Updates

        private void UpdateTitle(Color colorTitle, float sizeTitle, TMP_FontAsset font, TextAlignmentOptions alignment)
        {
            if (!_titleText)
            {
                return;
            }

            _titleText.color = colorTitle;
            _titleText.alignment = alignment;
            _titleText.fontSize = sizeTitle;
            _titleText.font = font;
        }
        
        private void UpdateDescription(Color colorDescription, float sizeDescription, TMP_FontAsset font, TextAlignmentOptions alignment)
        {
            if (!_titleText)
            {
                return;
            }

            _descriptionText.color = colorDescription;
            _descriptionText.alignment = alignment;
            _descriptionText.fontSize = sizeDescription;
            _descriptionText.font = font;
        }

        private void UpdateBackground(Sprite backgroundSprite, Color backgroundColor, float _pixelsPerUnitMultiplier)
        {
            _backgroundImage.sprite = backgroundSprite;
            _backgroundImage.color = backgroundColor;
            _backgroundImage.pixelsPerUnitMultiplier = _pixelsPerUnitMultiplier;
        }

        #endregion
    }
}
