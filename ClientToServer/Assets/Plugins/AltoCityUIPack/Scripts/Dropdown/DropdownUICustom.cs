using System;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Plugins.AltoCityUIPack.Scripts.Dropdown
{
    public class DropdownUICustom : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler
    {
        #region Fields

        [SerializeField] private Color _backgroundMainColor;
        [SerializeField] private Image _backgroundImage;
        [SerializeField, Range(0, 40)] public float _pixelsPerUnitMultiplier = 1;
        [SerializeField] private Sprite _backgroundSprite;
        [SerializeField] private bool _customBackground;
        
        [SerializeField, Range(0, 10)] private float _sizeIcon;
        [SerializeField, Range(0, 70)] private float _sizeText;
        [SerializeField] private TextAlignmentOptions _alignmentOptions;
        [SerializeField] private Color _textColor;
        [SerializeField] private TextMeshProUGUI selectedText;
        [SerializeField] private TMP_FontAsset _fontText;
        
        [SerializeField] private List<Item> items = new List<Item>();

        [SerializeField] private Image _backgroundItemsImage;
        [SerializeField] private Sprite _backgroundItemsSprite;
        [SerializeField, Range(0, 40)] private float _pixelsBackgroundItems;
        [SerializeField] private Color _colorBackgroundItems;
        [SerializeField] private Color _colorItemsDefault;
        [SerializeField] private Color _colorItemsSelected;
        [SerializeField] private bool _changeBackgroundItemsColor;
        [SerializeField] private TMP_FontAsset _fontTextItems;
        [SerializeField] private Sprite _backgroundItemSprite;
        [SerializeField, Range(0, 40)] private float _pixelsBackgroundItem;
        
        [SerializeField] private Color _textItemsSelectedColor;
        [SerializeField] private Color _textItemsDefaultColor;
        [SerializeField] private bool _changeTextItemsColor;
        [SerializeField] private int selectedItemIndex = 0;
        
        [SerializeField] private Image selectedImage;
        [SerializeField] private VerticalLayoutGroup itemParent;
        [SerializeField] private DropdownItemAltoCityUIPack itemObject;
        [SerializeField] private CanvasGroup listCG;

        [SerializeField] private List<DropdownUICustom> _disabledDropdowns;
        [SerializeField] private bool isInteractable = true;
        [SerializeField] private bool enableIcon = true;
        [SerializeField] private bool enableDropdownSounds;
        [SerializeField, Range(-50, 50)] private int itemPaddingTop = 8;
        [SerializeField, Range(-50, 50)] private int itemPaddingBottom = 8;
        [SerializeField, Range(-50, 50)] private int itemPaddingLeft = 8;
        [SerializeField, Range(-50, 50)] private int itemPaddingRight = 25;
        [SerializeField, Range(-50, 50)] private int itemSpacing = 8;
        
        //Trigger
        [SerializeField] private EventTrigger _eventTrigger;
        
        //Animate
        [SerializeField, Range(0, 5)] private float _durationOpenAnimateDropdown = 0.7f;
        [SerializeField, Range(0, 5)] private float _durationCloseAnimateDropdown = 0.4f;
        [SerializeField] private Sprite _animationOpenDropdown;
        [SerializeField] private Sprite _animationCloseDropdown;
        [SerializeField] private bool _enableChangeImageOpenDropdown;

        private List<DropdownItemAltoCityUIPack> _currentItemsObjects =new List<DropdownItemAltoCityUIPack>();

        public AudioClip hoverSound;
        public AudioClip clickSound;
        
        [HideInInspector] public bool _isOn;
        EventTrigger triggerEvent;
        Sprite imageHelper;
        string textHelper;
        
        
        public DropdownEvent onValueChanged;


        #region Properties

        public List<Item> Items => items;
        public int SelectedItemIndex => selectedItemIndex;

        #endregion
        
        #endregion

        #region Use
        
        public void UpdateCurrentItemName(string newName)
        {
            items[selectedItemIndex].itemName = newName;
            if (selectedText)
            {
                selectedText.SetText(newName);    
            }
            
            UpdateUI();
        }

        public void SetInteractable(bool state)
        {
            isInteractable = state;
        }
        
        public void CreateDropdownItem(Item item, Action<string> clickItem = null)
        {
            if (!itemParent || !itemObject)
            {
                return;
            }

            SetSelectedInfo(item);
            item.SetIndex(items.Count);
            items.Add(item);
            DropdownItemAltoCityUIPack dropdownItem = CreateDropdownItem();
            dropdownItem.Init(item, this, clickItem);
        }

        public void SetDropdownName(string itemName, bool playSound = true)
        {
            foreach (Item item in items)
            {
                if (item.itemName != itemName)
                {
                    continue;
                }

                SetDropdownIndex(item.itemIndex, playSound);
                return;
            }
        }

        public void ClearAllItems()
        {
            foreach (DropdownItemAltoCityUIPack child in  itemParent.GetComponentsInChildren<DropdownItemAltoCityUIPack>())
            {
                Destroy(child.gameObject);
            }
            
            items.Clear();
        }
        
        public string GetSelectedItem()
        {
            return items[selectedItemIndex].itemName;
        }
        
        public void RemoveItem(string itemTitle)
        {
            var item = items.Find(x => x.itemName == itemTitle);

            if (item == null)
            {
                return;
            }
            
            DropdownItemAltoCityUIPack itemObject = _currentItemsObjects.Find(x => x.ItemIndex == item.itemIndex);

            if (itemObject == null)
            {
                return;
            }
            
            _currentItemsObjects.Remove(itemObject);
            items.Remove(item);
            Destroy(itemObject.gameObject);
        }

        #endregion

        public void OnEnable()
        {
            listCG.alpha = 0;
            listCG.interactable = false;
            listCG.blocksRaycasts = false;

            UpdateUI();
        }

        public void UpdateUI()
        {
            UpdateItemLayout();
            UpdateMainPanel();
            UpdateDropdownItems();
            SetSelectIndex();
            UpdateBackground();
            UpdateBackgroundsItems();
        }

        private void UpdateMainPanel()
        {
            if (!selectedImage || !selectedText)
            {
                return;
            }

            selectedText.alignment = _alignmentOptions;
            selectedText.fontSize = _sizeText;
            selectedText.color = _textColor;
            if (_fontText)
            {
                selectedText.font = _fontText;
            }
            selectedImage.transform.localScale = new Vector3(_sizeIcon, _sizeIcon, _sizeIcon);
        }

        private void UpdateBackground()
        {
            if (!_backgroundImage)
            {
                return;
            }
            
            _backgroundImage.color = _backgroundMainColor;
            
            if (_customBackground)
            {
                _backgroundImage.sprite = _backgroundSprite;
                _backgroundImage.pixelsPerUnitMultiplier = _pixelsPerUnitMultiplier;
            }
        }

        private void UpdateBackgroundsItems()
        {
            if (!_backgroundItemsImage)
            {
                return;
            }

            _backgroundItemsImage.sprite = _backgroundItemsSprite;
            _backgroundItemsImage.pixelsPerUnitMultiplier = _pixelsBackgroundItems;
            _backgroundItemsImage.color = _colorBackgroundItems;
        }
        
        public void SetDropdownIndex(int itemIndex, bool playSound = true, bool disableValueChange = false)
        {
            if (itemIndex < items.Count && items[itemIndex] == null)
            {
                return;
            }
            
            if (selectedImage)
            {
                bool enable = enableIcon && items[itemIndex].itemIcon;
                
                selectedImage.gameObject.SetActive(enable);
                if (enable)
                {
                    selectedImage.sprite = items[itemIndex].itemIcon;
                }
            }
            
            if (selectedText != null)
            {
                selectedText.text = items[itemIndex].itemName;
            }

            if (enableDropdownSounds && playSound)
            {
                clickSound.PlayUISound();
            }
            
            selectedItemIndex = itemIndex;
            
            if (disableValueChange)
            {
                return;
            }
            
            items[selectedItemIndex].onItemSelection?.Invoke();
            CloseDropdownPanel();
            
            onValueChanged?.Invoke(selectedItemIndex);
        }

        private void UpdateDropdownItems()
        {
            if (!itemParent)
            {
               return; 
            }
            
            if (items.Count > itemParent.transform.childCount)
            {
                for (int index = itemParent.transform.childCount; index < items.Count; index++)
                {
                    items[index].SetIndex(index);
                    CreateDropdownItem();
                }
            }

            if (items.Count < itemParent.transform.childCount)
            {
                RemoveExcessItem();
            }
            
            for (int index = 0; index < items.Count; index++)
            {
                items[index].SetIndex(index);
            }
            
            for (int index = 0; index < itemParent.transform.childCount; index++)
            {
                if (itemParent.GetComponentsInChildren<DropdownItemAltoCityUIPack>()[index])
                {
                    DropdownItemAltoCityUIPack child = itemParent.GetComponentsInChildren<DropdownItemAltoCityUIPack>()[index];
                    for (int i = 0; i < items.Count; i++)
                    {
                        if (items[i].itemIndex == index)
                        {
                            SetSelectedInfo(items[i]);
                            child.Init(items[i], this);
                        }
                    }
                }
            }
        }

        private void RemoveExcessItem()
        {
            foreach (var dropdownItemAltoCityUIPack in itemParent.transform.GetComponentsInChildren<DropdownItemAltoCityUIPack>())
            {
                if (items.Find(x => x.itemIndex == dropdownItemAltoCityUIPack.ItemIndex) == null)
                {
                    _currentItemsObjects.Remove(dropdownItemAltoCityUIPack);
#if !UNITY_EDITOR
                    Destroy(dropdownItemAltoCityUIPack);
#endif
                }
            }
        }

        private void SetSelectIndex()
        {
            if (items is { Count : <= 0 })
            {
                return;
            }
            
            selectedImage.gameObject.SetActive(items[selectedItemIndex].itemIcon && enableIcon);
            selectedImage.sprite = items[selectedItemIndex].itemIcon;

            if (!selectedText)
            {
                return;
            }
            
            selectedText.text = items[selectedItemIndex].itemName;
        }

        private DropdownItemAltoCityUIPack CreateDropdownItem()
        {
            DropdownItemAltoCityUIPack dropdownItemAltoCityUIPack = Instantiate(itemObject, new Vector3(0, 0, 0), Quaternion.identity, itemParent.transform);
            _currentItemsObjects.Add(dropdownItemAltoCityUIPack);
            return dropdownItemAltoCityUIPack;
        }

        private void SetSelectedInfo(Item item)
        {
            if (_changeBackgroundItemsColor)
            {
                SetColor(item);
            }

            if (_changeTextItemsColor)
            {
                SetText(item);
            }

            if (_backgroundItemSprite)
            {
                SetSpriteBackgroundItem(item);
            }
        }

        private void SetColor(Item item)
        {
            item.SetColor(_colorItemsDefault, _colorItemsSelected);
        }
        
        private void SetText(Item item)
        {
            item.SetColorText(_textItemsDefaultColor, _textItemsSelectedColor);
            item.SetFontText(_fontTextItems);
        }

        private void SetSpriteBackgroundItem(Item item)
        {
            item.SetSpriteItem(_backgroundItemSprite, _pixelsBackgroundItem);
        }
        
        public void UpdateItemLayout()
        {
            if (!itemParent)
            {
                return;
            }
            
            itemParent.spacing = itemSpacing;
            itemParent.padding.top = itemPaddingTop;
            itemParent.padding.bottom = itemPaddingBottom;
            itemParent.padding.left = itemPaddingLeft;
            itemParent.padding.right = itemPaddingRight;
        }
        
        public void OnPointerClick(PointerEventData eventData)
        {
            if (!isInteractable)
            {
                return;
            }

            if (enableDropdownSounds)
            {
                if(clickSound)
                {
                    clickSound.PlayUISound();
                }
            }

            AnimateOpenOrCloseDropdownPanel();
        }

        private void AnimationChangeImageOpenDropdown(bool state)
        {
            if (!_enableChangeImageOpenDropdown)
            {
                return;
            }
            
            _backgroundImage.sprite = state ? _animationOpenDropdown : _animationCloseDropdown;
        }

        private void AnimateOpenOrCloseDropdownPanel()
        {
            _isOn = !_isOn;
            AnimateCanvasGroup();
            AnimationChangeImageOpenDropdown(_isOn);
        }

        public void CloseDropdownPanel()
        {
            _isOn = false;
            AnimateCanvasGroup();
            AnimationChangeImageOpenDropdown(false);
        }

        public void OpenDropdownPanel()
        {
            _isOn = true;
            AnimateCanvasGroup();
            AnimationChangeImageOpenDropdown(true);
        }

        private void AnimateCanvasGroup()
        {
            if (_eventTrigger)
            {
                _eventTrigger.gameObject.SetActive(_isOn);   
            }

            listCG.DOFade(Convert.ToInt32(_isOn), _isOn ? _durationOpenAnimateDropdown : _durationCloseAnimateDropdown);
            listCG.blocksRaycasts = _isOn;
            listCG.interactable = _isOn;

            if (_isOn)
            {
                foreach (var dropdown in _disabledDropdowns)
                {
                    dropdown.CloseDropdownPanel();
                }
            }
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (!isInteractable)
            {
                return;
            }

            if (enableDropdownSounds)
            {
                if (hoverSound)
                {
                    hoverSound.PlayUISound();
                }
            }
        }
    }
}
