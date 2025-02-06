using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Plugins.AltoCityUIPack.Scripts.Dropdown;

namespace Plugins.AltoCityUIPack.Scripts.Tooltip
{
    public sealed class TooltipSystemCustomAltoCityPack : MonoBehaviour
    {
        #region Fields
        
        [SerializeField] private TooltipAltoCityUIPack _tooltipAltoCityUIPack;

        [SerializeField] private RectTransform _tooltipRectTransform;

        [SerializeField] private List<TooltipItem> _tooltipItems = new();
        [SerializeField] private TooltipTypeAltoCityPackUI _currentTypeTooltip;

        [SerializeField] private TextAlignmentOptions _allTextAlignment;
        [SerializeField] private TMP_FontAsset _allFont;

        [SerializeField, Range(1, 1000)] private float _maxWidthToolTip = 400;
        
        // ReSharper disable once InconsistentNaming
        public int LastTabIndex;

        private TooltipItem _currentTooltip;
        
        private static TooltipSystemCustomAltoCityPack _current;
        
        #endregion

        public void Awake()
        {
            if (_current)
            {
                Destroy(gameObject);
                return;
            }

            _current = this;
        }

        public static void Show(string description, TooltipTypeAltoCityPackUI type, PositionTooltipEnums positionTooltipEnums, string title = "")
        {
            if (_current._tooltipItems == null)
            {
                return;
            }
            
            _current._tooltipAltoCityUIPack.SetText(description, title);

            if (_current.GetTooltipItem(type) != _current._currentTooltip)
            {
                _current.UpdateTooltip(_current.GetTooltipItem(type));
            }

            OpenTooltip();

            void OpenTooltip()
            {
                Vector2 position = Input.mousePosition;

                _current._tooltipRectTransform.pivot = _current.GetPivot(positionTooltipEnums);
                _current._tooltipRectTransform.transform.position = position;
                
                _current._tooltipAltoCityUIPack.gameObject.SetActive(true);
            }
        }

        public static void Hide()
        {
            _current._tooltipAltoCityUIPack.gameObject.SetActive(false);
        }

        private Vector2 GetPivot(PositionTooltipEnums positionTooltipEnums)
        {
            switch (positionTooltipEnums)
            {
                case PositionTooltipEnums.LEFTTOP:
                    return new Vector2(1, 0);
                case PositionTooltipEnums.LEFTBOTTTOM:
                    return new Vector2(1, 1);
                case PositionTooltipEnums.RIGHTTOP:
                    return new Vector2(0, 0);
                case PositionTooltipEnums.RIGHTBOTTOM:
                    return new Vector2(0, 1);
                case PositionTooltipEnums.TOP:
                    return new Vector2(0.5f, 0);
                case PositionTooltipEnums.BOTTOM:
                    return new Vector2(0.5f, 1);
                case PositionTooltipEnums.MIDDLE:
                    return new Vector2(0.5f, 0.5f);
            }
            
            Debug.LogError("Exception: PositionTooltipEnums not find enum");
            return new Vector2(0, 0);
        }
        
#if UNITY_EDITOR
        
        public void UpdateUI()
        {
            if (!_current)
            {
                Awake();
            }
            
            UpdateTooltip(_current.GetTooltipItem(_currentTypeTooltip));
        }
        
#endif

        private void UpdateTooltip(TooltipItem tooltipItem)
        {
            if (!_current || tooltipItem == null)
            {
                return;
            }
            
            tooltipItem.SetSettingsText(_current._allFont, _current._allTextAlignment);
            _current._tooltipAltoCityUIPack.InitTooltip(tooltipItem, _current._maxWidthToolTip);
        }

        private TooltipItem GetTooltipItem(TooltipTypeAltoCityPackUI type)
        {
            foreach (var item in _tooltipItems)
            {
                if (item.tooltipType == type)
                {
                    return item;
                }
            }

            return null;
        }
    }
}
