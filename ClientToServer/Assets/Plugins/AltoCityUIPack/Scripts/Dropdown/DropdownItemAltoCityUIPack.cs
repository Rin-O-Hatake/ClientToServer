using System;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Plugins.AltoCityUIPack.Scripts.Dropdown
{
    public class DropdownItemAltoCityUIPack : MonoBehaviour,
        IPointerExitHandler, IPointerEnterHandler, IPointerClickHandler
    {
        #region Fields

        [Title("[Dropdown Item Properties]")]
        [SerializeField] private Image _itemIcon;
        [SerializeField] private GameObject _rootIcon;
        
        [SerializeField] private Image _backgroundImage;
        [SerializeField] private Color _normalColor;
        [SerializeField] private Color _selectedColor;
        
        [SerializeField] private Color _normalColorText;
        [SerializeField] private Color _selectedColorText;
        [SerializeField] private TMP_Text _itemName;

        [Space(10.0f)] 
        [SerializeField] private HorizontalLayoutGroup _horizontalLayout;
        [SerializeField] private RectTransform _rectTransform;
        
        [Title("[READ-ONLY]")]
        [SerializeField, ReadOnly] private DropdownUICustom _dropdownUICustom;
        [SerializeField, ReadOnly] private Item _currentItem;

        private Action<string> _clickItem;

        #region Properties

        public int ItemIndex => _currentItem.itemIndex;

        #endregion

        #endregion

        public void Init(Item item, DropdownUICustom dropdownUICustom, Action<string> clickItem = null)
        {
            if (!_itemName)
            {
                return;
            }

            if (clickItem != null)
            {
                _clickItem = clickItem;    
            }
            
            _itemName.text = item.itemName;
            _itemName.fontSize = item.sizeText;
            _itemName.alignment = item.textAlignment;
            
            if (!_rootIcon)
            {
                return;
            }
            
            _rootIcon.gameObject.SetActive(item.itemIcon);
            if (item.itemIcon && _itemIcon)
            {
                _itemIcon.transform.localScale = new Vector3(item.sizeIcon, item.sizeIcon, item.sizeIcon);
                _itemIcon.sprite = item.itemIcon;
            }

            if (_horizontalLayout && item.padding != null)
            {
                _horizontalLayout.padding = new RectOffset(item.padding.left, item.padding.right, item.padding.top, item.padding.bottom);
                _horizontalLayout.spacing = item.spacing;
            }

            if (_rectTransform)
            {
                _rectTransform.sizeDelta = new Vector2(0, item.height);
            }

            if (item.EnableColorsBackground && _backgroundImage)
            {
                _normalColor = item.DefaultColorBackground;
                _selectedColor = item.SelectColorBackground;
                _backgroundImage.color = _normalColor;
                _backgroundImage.sprite = item.SpriteItemBackground;
                _backgroundImage.pixelsPerUnitMultiplier = item.PixelsItemBackground;
            }

            if (item.EnableColorsText && _itemIcon)
            {
                _normalColorText = item.DefaultColorText;
                _selectedColorText = item.SelectColorText;
                _itemName.color = _normalColorText;

                if (item.ItemsFontText != null)
                {
                    _itemName.font = item.ItemsFontText;   
                }

                _itemIcon.color = _normalColorText;
            }
            
            _dropdownUICustom = dropdownUICustom;
            _currentItem = item;
        }
        
        public void OnPointerEnter(PointerEventData eventData)
        {
            _itemName.color = _selectedColorText;
            _backgroundImage.color = _selectedColor;
            _itemIcon.color = _selectedColorText;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _itemName.color = _normalColorText;
            _backgroundImage.color = _normalColor;
            _itemIcon.color = _normalColorText;
        }
        
        public void OnPointerClick(PointerEventData eventData)
        {
            _dropdownUICustom.SetDropdownIndex(_currentItem.itemIndex);
            _clickItem?.Invoke(_currentItem.dataItem);
        }
    }
}