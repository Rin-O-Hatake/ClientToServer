using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;

namespace Plugins.AltoCityUIPack.Scripts.Switch
{
    public class SwitchCustomAltoCityUIPack : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler
    {
        #region Fields
        
        //Toggle
        [SerializeField] private Image _toggle;
        [SerializeField] private Color _toggleColor;
        [SerializeField] private Sprite _toggleSprite;
        
        // Animation
        [SerializeField] private Image _toggleIsOn;
        [SerializeField] private Image _toggleIsOff;
        [SerializeField, Range(0, 10)] private float _duration = 1;
        [SerializeField] private Color _colorBackgroundIsOn;
        [SerializeField] private Color _colorBackgroundIsOff;
        
        //Switch 
        [SerializeField] private CanvasGroup _cgSwitch;
        [SerializeField] private Image _backgroundImage;
        [SerializeField] private Sprite _backgroundSprite;
        [SerializeField, Range(0, 100)] private float _pixelsPerUnitImage = 28f;
        
        //Interactable
        [SerializeField] private bool _isInteractable = true;
        
        // Events
        [SerializeField] public SwitchEvent onValueChanged = new();
        [SerializeField] private UnityEvent OnEvents;
        [SerializeField] private UnityEvent OffEvents;
        
        // Settings
        [SerializeField] private bool isOn;
        [SerializeField] private bool enableSwitchSounds;
        [SerializeField] private bool useHoverSound = true;
        [SerializeField] private bool useClickSound = true;

        // Audio
        [SerializeField] private AudioClip hoverSound;
        [SerializeField] private AudioClip clickSound;

        [System.Serializable]
        public class SwitchEvent : UnityEvent<bool> { }

        private bool _isInitialized;

        #region Properties

        public bool IsOn => isOn;
        public SwitchEvent OnValueChanged => onValueChanged;

        #endregion
        
        #endregion
        
        public void UpdateUI()
        {
            UpdateInteractable();
            UpdateToggle();
            UpdateBackground();
        }

        #region Use

        public void SetInteractable(bool state)
        {
            _isInteractable = state;
            UpdateInteractable();
        }
        
        public void Toggle(bool state, bool useEvent = true)
        {
            if (state)
            {
                SetOn(useEvent);
            }
            else
            {
                SetOff(useEvent);
            }
        }

        #endregion

        private void OnEnable()
        {
            if (_isInitialized)
            {
                UpdateUI();
            }

            if (!_toggleIsOn || !_toggleIsOff)
            {
                return;
            }
            
            _toggleIsOn.gameObject.SetActive(false);
            _toggleIsOff.gameObject.SetActive(false);
        }
        
        private void OnDisable()
        {
            _toggle.transform.DOKill();
        }

        public void SetOn(bool useEvents = true)
        {
            isOn = true;
            AnimateSwitch();
            if (!useEvents)
            {
                return;
            }

            OnEvents?.Invoke();
            onValueChanged.Invoke(true);
        }

        public void SetOff(bool useEvents = true)
        {
            isOn = false;
            AnimateSwitch();
            if (!useEvents)
            {
                return;
            }

            OffEvents?.Invoke();
            onValueChanged.Invoke(false);
        }

        private void AnimateSwitch()
        {
            if (!_toggleIsOn || !_toggleIsOff || !_toggle)
            {
                return;
            }
            
            float xLocation = isOn ? _toggleIsOn.transform.localPosition.x : _toggleIsOff.transform.localPosition.x;
            Color color = isOn ? _colorBackgroundIsOn : _colorBackgroundIsOff;
            
            _toggle.transform.DOLocalMoveX(xLocation, _duration);

            if (!_backgroundImage)
            {
                return;
            }
            
            _backgroundImage.DOColor(color, _duration);
        }

        #region OnPointer

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (!enableSwitchSounds || !useHoverSound || !_isInteractable)
            {
                return;
            }
            
            hoverSound.PlayUISound();
        }
        
        public void OnPointerClick(PointerEventData eventData)
        {
            isOn = !isOn;
            Toggle(isOn);
            if (clickSound && useClickSound && enableSwitchSounds)
            {
                clickSound.PlayUISound();
            }
        }

        #endregion

        #region UpdateEditor

        private void UpdateInteractable()
        {
            if (!_cgSwitch)
            {
                return;
            }
            
            _cgSwitch.interactable = _isInteractable;
            _cgSwitch.blocksRaycasts = _isInteractable;
        }

        private void UpdateToggle()
        {
            if (!_toggle || !_toggleSprite)
            {
                return;
            }

            SetColorAndSpriteImage(_toggle, _toggleColor, _toggleSprite);
            if (!_toggleIsOff || !_toggleIsOn)
            {
                return;
            }
            
            SetColorAndSpriteImage(_toggleIsOff, _toggleColor, _toggleSprite);
            SetColorAndSpriteImage(_toggleIsOn, _toggleColor, _toggleSprite); 
        }

        private void SetColorAndSpriteImage(Image image, Color color, Sprite sprite)
        {
            image.color = color;
            image.sprite = sprite;
        }

        private void UpdateBackground()
        {
            if (!_backgroundImage)
            {
                return;
            }
            
            _backgroundImage.color = _colorBackgroundIsOff;
            _backgroundImage.pixelsPerUnitMultiplier = _pixelsPerUnitImage;
            _backgroundImage.sprite = _backgroundSprite;
        }

        #endregion
    }
}