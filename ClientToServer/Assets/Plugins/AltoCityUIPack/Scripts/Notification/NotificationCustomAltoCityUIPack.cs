using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using DG.Tweening;
using Plugins.AltoCityUIPack.Scripts.Dropdown;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Plugins.AltoCityUIPack.Scripts.Notification
{
    public class NotificationCustomAltoCityUIPack : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
    {
        #region Fields

        [SerializeField] private List<NotificationItem> _notificationItems = new List<NotificationItem>();
        [SerializeField] private NotificationTypeAltoCityPackUI _currentNotificationType;
        private NotificationItem _currentNotification;
        [SerializeField] private RectTransform _rootRectTransform;
        [SerializeField] private CanvasGroup _currentCanvasGroup;

        // Icon
        [SerializeField] private Image _iconImage;

        // Title
        [SerializeField] private string _title = "Notification Title";
        [SerializeField] private TextMeshProUGUI _titleText;
        [SerializeField] private TMP_FontAsset _fontTitle;

        // Description
        [SerializeField] private string _description = "Notification description";
        [SerializeField] private TextMeshProUGUI _descriptionText;
        [SerializeField] private TMP_FontAsset _fontDescription;

        //Background
        [SerializeField] private Image _backgroundImage;
        [SerializeField] private Sprite _backgroundSprite;
        [SerializeField, Range(0, 40)] public float _pixelsPerUnitMultiplierBackground = 1;
        
        //Title Background
        [SerializeField] private Image _backgroundTitleImage;
        [SerializeField] private Sprite _backgroundTitleSprite;
        [SerializeField, Range(0, 40)] public float _pixelsPerUnitMultiplierBackgroundTitle = 1;

        // Settings
        [SerializeField] private bool _enableTimer = true;
        [SerializeField, Range(0.05f, 10)] private float _timer = 3f;
        [SerializeField] private StartBehaviour _startBehaviour = StartBehaviour.Disable;
        [SerializeField] private CloseBehaviour _closeBehaviour = CloseBehaviour.Disable;
        [SerializeField] private bool _isCloseBehaviourClick;
        
        //Sound
        [SerializeField] private bool _enableButtonSounds;
        [SerializeField] private bool _useFocusedSound;
        [SerializeField] private bool _useClickSound;
        [SerializeField] private AudioClip _focusedSound;
        [SerializeField] private AudioClip _clickSound;
        
        //Animation
        [SerializeField] private bool _enableFocusedAnimation;
        [SerializeField, Range(0.1f, 2)] private float _sizeFocused;
        [SerializeField, Range(0.05f, 1)] private float _durationAnimationOnPointerEnter;
        [SerializeField] private bool _enableChangeFocusedColor;

        [SerializeField] private AnimationOpenNotification _animationOpenNotification;
        [SerializeField, Range(-4000, 4000)] private float _startOpenPositionAnimation;
        [SerializeField] private float _endOpenPositionAnimation;
        [SerializeField, Range(0, 5)] private float _durationAnimationOpen;

        // Events
        [SerializeField] private UnityEvent _onOpen;
        [SerializeField] private UnityEvent _onClose;
        [SerializeField] private UnityEvent _onClick;
        
        private bool _isOn;
        private const float _disableAlphaCanvasGroup = 0;
        private const float _enableAlphaCanvasGroup = 1;

        public enum StartBehaviour { None, Disable }
        public enum CloseBehaviour { None, Disable, Destroy }
        
        public enum AnimationOpenNotification { Horizontal, Vertical }

        #region Properties

        public bool IsOn => _isOn;
        public UnityEvent OnOpen => _onOpen;
        public UnityEvent OnClose => _onClose;
        public UnityEvent OnClick => _onClick;
        public List<NotificationItem> NotificationItems => _notificationItems;
        
        private bool IsHorizontal => _animationOpenNotification == AnimationOpenNotification.Horizontal;

        #endregion

        #endregion

        #region Use

        public void SetTitle(string title)
        {
            _title = title;
            UpdateTitle();
        }

        public void SetDescription(string description)
        {
            _description = description;
            UpdateDescription();
        }

        public void SetTypeNotification(NotificationTypeAltoCityPackUI notificationTypeAltoCityPackUI)
        {
            _currentNotificationType = notificationTypeAltoCityPackUI;
            UpdateNotificationItem();
        }

        public void Open()
        {
            OpenNotification();
        }

        public void SetCloseBehaviour(CloseBehaviour closeBehaviour)
        {
            _closeBehaviour = closeBehaviour;
        }

        public void SetEnableTimer(bool state)
        {
            _enableTimer = state;
        }

        #endregion

        private void OnDisable()
        {
            UpdateUI();
            SetScaleBackground(1, _durationAnimationOnPointerEnter);
            gameObject.transform.DOKill();
        }

        private void Awake()
        {
            if (_startBehaviour == StartBehaviour.Disable)
            {
                _currentCanvasGroup.alpha = _disableAlphaCanvasGroup;
            }
        }

        public void UpdateUI(bool isUpdateNotificationItem = true)
        {
            UpdateIcon();
            UpdateTitle();
            UpdateDescription();
            UpdateBackground();
            UpdateBackgroundTitle();
            
            if (!isUpdateNotificationItem)
            {
                return;
            }
            
            UpdateNotificationItem();
        }

        private void Click()
        {
            _onClick?.Invoke();

            if (_enableButtonSounds && _useClickSound) 
            {
                _clickSound.PlayUISound();
            }

            if (_isCloseBehaviourClick)
            {
                DisableNotification();
            }
        }

        private async void OpenNotification()
        {
            if (_isOn)
            {
                return;
            }

            await Task.Delay(100);
            
            Vector3 position = gameObject.transform.localPosition;
            
            _endOpenPositionAnimation = IsHorizontal ? position.x : position.y;

            gameObject.transform.localPosition = new Vector3(IsHorizontal ? _startOpenPositionAnimation : position.x, IsHorizontal ? position.y : _startOpenPositionAnimation, position.z);

            _currentCanvasGroup.alpha = _enableAlphaCanvasGroup;

            SetMoveNotification(_endOpenPositionAnimation);
            
            _onOpen?.Invoke();
            
            if (_enableTimer)
            {
                StartCoroutine(nameof(StartTimer));
            }
            
            _isOn = true;
        }
        
        private void CloseNotification()
        {
            _onClose?.Invoke();
            if (!_isOn)
            {
                return;
            }
            
            SetMoveNotification(_startOpenPositionAnimation);
            
            _isOn = false;
        }

        #region Updates

        private void UpdateIcon()
        {
            if (!_iconImage || _currentNotification == null)
            {
                return;
            }
            
            _iconImage.sprite = _currentNotification.icon;
            _iconImage.color = _currentNotification.colorIcon;
            _iconImage.transform.localScale = new Vector3(_currentNotification.sizeIcon, _currentNotification.sizeIcon, _currentNotification.sizeIcon);
        }
        
        private void UpdateTitle()
        {
            if (!_titleText || _currentNotification == null)
            {
                return;
            }
            
            _titleText.text = _title;
            
            if (_fontTitle)
            {
                _titleText.font = _fontTitle;
            }
            
            _titleText.color = _currentNotification.colorTitle;
            _titleText.fontSize = _currentNotification.sizeTitle;
            
            UpdateLayout();
        }
        
        private void UpdateDescription()
        {
            if (!_descriptionText || _currentNotification == null)
            {
                return;
            }
            
            _descriptionText.text = _description;
            
            if (_fontDescription)
            {
                _descriptionText.font = _fontDescription;   
            }
            
            _descriptionText.fontSize = _currentNotification.sizeDescription;
            _descriptionText.color = _currentNotification.colorDescription;
            
            UpdateLayout();
        }

        private void UpdateBackground()
        {
            if (!_backgroundImage || _currentNotification == null)
            {
                return;
            }
            
            _backgroundImage.sprite = _backgroundSprite;
            _backgroundImage.color = _currentNotification.colorBackground;
            _backgroundImage.pixelsPerUnitMultiplier = _pixelsPerUnitMultiplierBackground;
            
            UpdateLayout();
        }
        
        private void UpdateBackgroundTitle()
        {
            if (!_backgroundTitleImage || _currentNotification == null)
            {
                return;
            }
            
            _backgroundTitleImage.sprite = _backgroundTitleSprite;
            _backgroundTitleImage.color = _currentNotification.colorBackgroundTitle;
            _backgroundImage.pixelsPerUnitMultiplier = _pixelsPerUnitMultiplierBackgroundTitle;
        }

        private void UpdateNotificationItem()
        {
            foreach (var item in _notificationItems)
            {
                if (item.notificationType == _currentNotificationType)
                {
                    _currentNotification = item;
                    UpdateUI(false);
                    return;
                }

                _currentNotification = null;
            }
        }
        
        private void UpdateLayout()
        {
            if (!_rootRectTransform)
            {
                return;
            }
            
            LayoutRebuilder.ForceRebuildLayoutImmediate(_rootRectTransform.GetComponent<RectTransform>());
        }

        #endregion

        #region Interface

        public void OnPointerClick(PointerEventData eventData)
        {
            Click();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            SetScaleBackground(_sizeFocused, _durationAnimationOnPointerEnter);
            
            if (_enableButtonSounds && _useFocusedSound)
            {
                _focusedSound.PlayUISound();
            }

            if (!_enableChangeFocusedColor)
            {
                return;
            }
            
            if (_iconImage)
            {
                _iconImage.color = _currentNotification.focusedColorIcon;
            }

            if (_titleText)
            {
                _titleText.color = _currentNotification.focusedColorTitle;
            }

            if (_descriptionText)
            {
                _descriptionText.color = _currentNotification.focusedColorDescription;
            }

            if (_backgroundImage)
            {
                _backgroundImage.color = _currentNotification.focusedColorBackground;
            }
            
            if (_backgroundTitleImage)
            {
                _backgroundTitleImage.color = _currentNotification.focusedColorBackgroundTitle;
            }
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            UpdateUI();
            SetScaleBackground(1, _durationAnimationOnPointerEnter);
        }

        #endregion

        #region Animation

        private void SetScaleBackground(float size, float speed)
        {
            if (!_enableFocusedAnimation || !_backgroundImage)
            {
                return;
            }
            
            gameObject.transform.DOScale(size, speed).SetEase(Ease.Linear);
        }

        private void SetMoveNotification(float endDistance)
        {
            SetMove(endDistance, _durationAnimationOpen);
        }
        
        private void SetMoveNotificationWithTimer(float endDistance, float duration)
        {
            SetMove(endDistance, duration);
        }

        private void SetMove(float endDistance, float duration)
        {
            if (IsHorizontal)
            {
                gameObject.transform.DOLocalMoveX(endDistance, duration).SetEase(Ease.Linear);   
            }
            else
            {
                gameObject.transform.DOLocalMoveY(endDistance, duration).SetEase(Ease.Linear);
            }
        }
        
        private IEnumerator StartTimer()
        {
            yield return new WaitForSeconds(_timer);
            CloseNotification();
            StartCoroutine(nameof(CloseDisableNotification));
        }
        
        private IEnumerator CloseDisableNotification()
        {
            yield return new WaitForSeconds(_durationAnimationOpen);
            DisableNotification();
        }

        private void DisableNotification()
        {
            if (_closeBehaviour == CloseBehaviour.Disable)
            {
                gameObject.SetActive(false);
                _isOn = false;
            }
            else if (_closeBehaviour == CloseBehaviour.Destroy)
            {
                Destroy(gameObject);
            }
        }

        #endregion
    }
}
