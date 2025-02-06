using System;
using UnityEngine;
using UnityEngine.Events;
using TMPro;
using Plugins.AltoCityUIPack.Scripts.Button;

namespace Plugins.AltoCityUIPack.Scripts.Dropdown
{
    #region Dropdown
    
    [Serializable]
    public class Item
    {
        #region Field

        public string itemName = "Dropdown Item";
        public Sprite itemIcon;
        
        [Range(0, 30)] public float sizeIcon = 1.8f;
        [Range(0, 70)] public float sizeText = 32;
        public UIButtonManagerCustom.Padding padding = new UIButtonManagerCustom.Padding();
        public float spacing;
        public float height = 40;
        public TextAlignmentOptions textAlignment;
        
        public int itemIndex;
        public UnityEvent onItemSelection = new UnityEvent();
        
        public string dataItem;

        private Color _defaultColorBackground;
        private Color _selectColorBackground;
        private bool _enableColorsBackground;
        
        private Color _defaultColorText;
        private Color _selectColorText;
        private bool _enableColorsText;

        private Sprite _spriteItemBackground;
        private float _pixelsItemBackground;

        private TMP_FontAsset _itemsFontText;

        #region Properties

        public Color DefaultColorBackground => _defaultColorBackground;
        public Color SelectColorBackground => _selectColorBackground;
        public bool EnableColorsBackground => _enableColorsBackground;
        
        public Color DefaultColorText => _defaultColorText;
        public Color SelectColorText => _selectColorText;
        public bool EnableColorsText => _enableColorsText;
        public TMP_FontAsset ItemsFontText => _itemsFontText;
        public Sprite SpriteItemBackground => _spriteItemBackground;
        
        public float PixelsItemBackground => _pixelsItemBackground;

        #endregion

        #endregion

        public void SetIndex(int index)
        {
            itemIndex = index;
        }

        public void SetColor(Color defaultColor, Color selectColor)
        {
            _defaultColorBackground = defaultColor;
            _selectColorBackground = selectColor;
            _enableColorsBackground = true;
        }

        public void SetColorText(Color defaultColorText, Color selectColorText)
        {
            _defaultColorText = defaultColorText;
            _selectColorText = selectColorText;
            _enableColorsText = true;
        }

        public void SetFontText(TMP_FontAsset fontText)
        {
            _itemsFontText = fontText;
        }

        public void SetSpriteItem(Sprite sprite, float pixelsBackgroundItem)
        {
            _spriteItemBackground = sprite;
            _pixelsItemBackground = pixelsBackgroundItem;
        }
    }
    
    [Serializable]
    public class DropdownEvent : UnityEvent<int> { }
    
    #endregion

    #region Notification
    
    [Serializable]
    public class NotificationItem
    {
        public NotificationTypeAltoCityPackUI notificationType;
        
        public Sprite icon;
        public Color colorIcon = Color.white;
        [Range(0.05f, 70)] public float sizeIcon = 1;
        public Color focusedColorIcon = Color.white;
        
        public Color colorTitle = Color.white;
        [Range(1, 100)] public float sizeTitle = 20;
        public Color focusedColorTitle = Color.white;

        public Color colorDescription = Color.white;
        [Range(1, 100)] public float sizeDescription = 20;
        public Color focusedColorDescription = Color.white;

        public Color colorBackground = Color.white;
        public Color focusedColorBackground = Color.white;

        public Color colorBackgroundTitle = Color.white;
        public Color focusedColorBackgroundTitle = Color.white;

    }

    public enum NotificationTypeAltoCityPackUI
    {
        DEFAULT,
        ERROR
    }

    #endregion

    #region Tooltip

    [Serializable]
    public class TooltipItem
    {
        public TooltipTypeAltoCityPackUI tooltipType;

        public Color colorTitle = Color.white;
        [Range(1, 100)] public float sizeTitle = 20;

        public Color colorDescription = Color.white;
        [Range(1, 100)] public float sizeDescription = 20;

        public Sprite backgroundSprite;
        public Color backgroundColor = Color.white;
        [SerializeField, Range(0, 40)] public float _pixelsPerUnitMultiplier = 15;

        public TMP_FontAsset _allFont { get; private set; }
        public TextAlignmentOptions _allTextAlignment { get; private set; }

        public void SetSettingsText(TMP_FontAsset font, TextAlignmentOptions alignmen)
        {
            _allFont = font;
            _allTextAlignment = alignmen;
        }
    }
    
    public enum TooltipTypeAltoCityPackUI
    {
        DEFAULT
    }

    public enum PositionTooltipEnums
    {
        LEFTTOP,
        RIGHTTOP,
        LEFTBOTTTOM,
        RIGHTBOTTOM,
        BOTTOM,
        TOP,
        MIDDLE
    }

    #endregion

    #region Gradient

    public enum Type
    {
        Horizontal,
        Vertical,
        Diamond
    }

    public enum Blend
    {
        Override,
        Add,
        Multiply
    }

    #endregion
}