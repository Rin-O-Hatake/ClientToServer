using System.Threading.Tasks;
using DG.Tweening;
using Plugins.AltoCityUIPack.Scripts.Rendering;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Plugins.AltoCityUIPack.Scripts.Button
{
    [ExecuteInEditMode]
    public class UIButtonManagerCustom : MonoBehaviour, IPointerClickHandler, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
    {
        #region Filed
        
        //Icon
        [SerializeField] private Sprite buttonIcon;
        [SerializeField, Range(0.1f, 200)] private float _iconScale = 1;
        [SerializeField] private Image _currentImage;
        [SerializeField] private Color _imageColor = Color.white;
        [SerializeField] private bool enableIcon;
        [SerializeField] private Padding _paddingImage;
        [SerializeField] private HorizontalLayoutGroup _horizontalLayoutImage;
        
        //Text
        [SerializeField] private bool enableText = true;
        [SerializeField] private TextMeshProUGUI _currentText;
        [SerializeField] private TMP_FontAsset _Font;
        [SerializeField] private TextAlignmentOptions _currentTextAlignment;
        [SerializeField] private Color _currentTextColor;
        [SerializeField] private string buttonText = "Button";
        [SerializeField, Range(0.1f, 200)] private float textSize = 24;
        
        //Paddings
        [SerializeField] private CanvasGroup normalCG;
        [SerializeField] private HorizontalLayoutGroup _infoLayout;
        [SerializeField] private Padding _paddingInfo;
        [SerializeField] private bool autoFitContent;
        [SerializeField] private Padding padding;
        [SerializeField, Range(0, 100)] public int spacing = 15;
        [SerializeField] private HorizontalLayoutGroup mainLayout;
        [SerializeField] private ContentSizeFitter mainFitter;
        [SerializeField] private ContentSizeFitter _infoFitter;
        
        //Background
        [SerializeField] private Image _currentBackground;
        [SerializeField] private Sprite _backgroundSprite;
        [SerializeField] private Color _backgroundColorNormal;
        [SerializeField] private Color _backgroundColorDisabled;
        [SerializeField] private Color _backgroundColorFocused;
        [SerializeField] private bool _customBackground;
        [SerializeField, Range(0, 40)] private float _pixelsPerUnitMultiplier = 15;
        [SerializeField] private bool _enableFocusedChangeColor;
        
        //Frame
        [SerializeField] private bool _enableFrame = false;
        [SerializeField] private Image _currentFrameImage;
        [SerializeField] private Sprite _frameSprite;
        [SerializeField] private Color _frameColor;
        [SerializeField, Range(0, 40)] private float _framePixelsPerUnitMultiplier = 15; 
        

        //Interactable
        [SerializeField, Range(0, 1)] private float _interactableAlpha = 0.4f;
        [SerializeField] private bool isIntractable = true;
        
        //Sound
        [SerializeField] private bool enableButtonSounds = false;
        [SerializeField] private bool useHoverSound = true;
        [SerializeField] private bool useClickSound = true;
        [SerializeField] private AudioClip hoverSound;
        [SerializeField] private AudioClip clickSound;
        
        //Ripple
        [SerializeField] private bool useRipple = true;
        [SerializeField] private RippleCustom ripple;
        
        //Events
        [SerializeField] private UnityEvent onClick = new UnityEvent();
        [SerializeField] private UnityEvent onHover = new UnityEvent();
        [SerializeField] private UnityEvent onLeave = new UnityEvent();
        [SerializeField] private UnityEvent onPressButton = new UnityEvent();
        [SerializeField] private bool _isOnPressButton = false;
        [SerializeField, Range(1, 1000)] private int _stepNextCallBack = 100;

        //Animation
        [SerializeField] private bool _enableAnimation;
        [SerializeField] private bool _enableAnimationOnPointerEnter;
        [SerializeField] private bool _enableAnimationOnPointerClick;
        [SerializeField, Range(0.05f, 5)] private float _durationAnimationOnPointerClick;
        [SerializeField, Range(0.05f, 5)] private float _durationAnimationOnPointerEnter;
        [SerializeField, Range(0.1f, 20)] private float _sizeAnimationOnPointerClick;
        [SerializeField, Range(0.1f, 20)] private float _sizeAnimationOnPointerEnter;
        
        [SerializeField, Range(0.1f, 5)] public float duration = 1f;
        [SerializeField, Range(0.5f, 25)] public float maxSize = 4f;
        [SerializeField] private Color startColor = new Color(1f, 1f, 1f, 0.2f);
        [SerializeField] private bool centered;
        
        UnityEngine.UI.Button targetButton;
        bool isPointerOn;
        bool waitingForDoubleClickInput;
        private bool _isSelected;

#if UNITY_EDITOR
        public int latestTabIndex = 0;
#endif

        #region Properties

        public UnityEvent OnClick => onClick;
        public UnityEvent OnHover => onHover;
        public UnityEvent OnLeave => onLeave;
        public string ButtonText => buttonText;
        public bool IsIntractable => isIntractable;

        #endregion
        
        #endregion
        
        [System.Serializable]
        public class Padding
        {
            public int left;
            public int right;
            public int top;
            public int bottom ;
        }

        #region Monobehavoir

        void OnEnable()
        {
            UpdateUI();
        }

        void OnDisable()
        {
            _currentBackground.transform.DOKill();
            
            if (isIntractable == false)
                return;
            
            ResetScale();
        }
        
        private void OnDestroy()
        {
            _currentBackground.transform.DOKill();
        }

        private async void Update()
        {
            if (!_isOnPressButton)
            {
                return;
            }

            await Task.Delay(_stepNextCallBack);
            
            if (Input.GetKey(KeyCode.Mouse0) && _isSelected)
            {
                onPressButton?.Invoke();
            }
        }

        #endregion

        public void UpdateUI()
        {
            if (mainFitter)
            {
                mainFitter.enabled = autoFitContent;
            }

            if (_infoFitter)
            {
                _infoFitter.enabled = autoFitContent;
            }

            if (_currentBackground)
            {
                _currentBackground.type = _customBackground ? Image.Type.Sliced : Image.Type.Simple;
            }

            UpdateText();
            UpdateImage();
            UpdateBackground();
            UpdateIntractable();
            UpdatePadding();
            UpdateFrame();
            
            if (ripple)
            {
                ripple.Init(startColor, maxSize, duration);
            }
        }

        #region Use

        public void SetBackground(Sprite sprite)
        {
            _backgroundSprite = sprite;
            UpdateBackground();
        }
        public void SetText(string text)
        {
            buttonText = text; UpdateText();
        }

        public void SetColorText(Color colorText)
        {
            _currentTextColor = colorText;
            UpdateText();
        }

        public void SetIcon(Sprite icon = null, float iconScale = -1)
        {
            buttonIcon = icon == null ? buttonIcon : icon;
            _iconScale = iconScale < 0 ? _iconScale : iconScale;
            UpdateImage();
        }

        public void SetIntractable(bool value)
        {
            isIntractable = value;
            UpdateIntractable();
        }

        public void SetColorIcon(Color color)
        {
            _imageColor = color;
            UpdateImage();
        }

        public void SetColorBackgroundNormal(Color color)
        {
            _backgroundColorNormal = color;
            UpdateBackground();
        }

        public void EnableImage(bool state)
        {
            enableIcon = state;
            UpdateImage();
        }

        public void EnableText(bool state)
        {
            enableText = state;
            UpdateText();
        }

        public void SetPaddingInfo(Padding paddingInfo)
        {
            _paddingInfo = paddingInfo;
            UpdatePadding();
        }

        #endregion

        public void CreateRipple(Vector2 pos)
        {
            if (ripple != null)
            {
                ripple.RippleAnimation(centered ? new Vector2(0f, 0f) : pos);
            }
        }
        
        public void SetScaleBackground(float size, float speed)
        {
            if (!_enableAnimation || !_currentBackground)
            {
                return;
            }
            
            _currentBackground.transform.DOScale(size, speed);
        }
        
        #region Interface

        public void OnPointerClick(PointerEventData eventData)
        {
            if (!isIntractable)
            {
                return;
            }
            
            onClick?.Invoke();
            
            if (_enableAnimationOnPointerClick)
            {
                SetScaleBackground(1f, _durationAnimationOnPointerClick * 1.2f);
            }
            
            if (enableButtonSounds && useClickSound)
            {
                clickSound.PlayUISound();
            }
        }
        
        public void OnPointerDown(PointerEventData eventData)
        {
            if (!isIntractable)
            {
                return;
            }
            
            if (_enableAnimationOnPointerClick)
            {
                SetScaleBackground(_sizeAnimationOnPointerClick, _durationAnimationOnPointerClick);
            }
            
            if (useRipple)
            {
#if ENABLE_LEGACY_INPUT_MANAGER
                CreateRipple(Input.mousePosition);
#elif ENABLE_INPUT_SYSTEM   
                CreateRipple(Mouse.current.position.ReadValue());
#endif
            }
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (!isIntractable)
            {
                return;   
            }
            
            _isSelected = true;
            
            if (_enableAnimationOnPointerEnter)
            {
                SetScaleBackground(_sizeAnimationOnPointerEnter, _durationAnimationOnPointerEnter);    
            }

            if (enableButtonSounds && useHoverSound)
            {
                hoverSound.PlayUISound();
            }
            
            if (_enableFocusedChangeColor)
            {
                _currentBackground.color = _backgroundColorFocused;
            }
            
            onHover.Invoke();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (!isIntractable)
            {
                return;
            }
            
            _isSelected = false;
            
            if (_enableAnimationOnPointerEnter)
            {
                ResetScale();    
            }
            
            _currentBackground.color = _backgroundColorNormal;
            
            onLeave.Invoke();
        }
        
        #endregion

        #region Updates

        private void UpdateText()
        {
            if (enableText)
            {
                if (_currentText)
                {
                    _currentText.gameObject.SetActive(true);
                    _currentText.text = buttonText;
                    _currentText.fontSize = textSize;
                    _currentText.font = _Font;
                    _currentText.alignment = _currentTextAlignment;
                    _currentText.color = _currentTextColor;
                    _currentText.raycastTarget = false;
                }
            }
            else
            {
                if (_currentText)
                {
                    _currentText.gameObject.SetActive(false);
                }
            }
        }
        
        private void UpdateImage()
        {
            if (enableIcon)
            {
                Vector3 tempScale = new Vector3(_iconScale, _iconScale, _iconScale);
                if (_currentImage)
                {
                    _currentImage.transform.gameObject.SetActive(true);
                    _currentImage.sprite = buttonIcon;
                    _currentImage.transform.localScale = tempScale;
                    _currentImage.color = _imageColor;
                    _currentImage.raycastTarget = false;

                    if (_horizontalLayoutImage)
                    {
                        _horizontalLayoutImage.padding = new RectOffset(_paddingImage.left, _paddingImage.right,
                            _paddingImage.top, _paddingImage.bottom);
                    }
                }
            }
            else
            {
                if (_currentImage)
                {
                    _currentImage.transform.gameObject.SetActive(false);
                }
            }
        }

        private void UpdateBackground()
        {
            if (!_currentBackground)
            {
                return;
            }

            if (_customBackground)
            {
                _currentBackground.sprite = _backgroundSprite;
                _currentBackground.pixelsPerUnitMultiplier = _pixelsPerUnitMultiplier;
            }
            
            UpdateBackgroundColor(isIntractable);
        }

        private void UpdatePadding()
        {
            if (mainLayout)
            {
                mainLayout.padding = new RectOffset(padding.left, padding.right, padding.top, padding.bottom);
            }

            if (_infoLayout)
            {
                _infoLayout.padding = new RectOffset(_paddingInfo.left, _paddingInfo.right, _paddingInfo.top, _paddingInfo.bottom);
                _infoLayout.spacing = spacing;
            }
        }
        
        private void UpdateIntractable() 
        {
            if (!_currentBackground || !normalCG)
            {
                return;
            }
            
            if (isIntractable)
            {
                _currentBackground.raycastTarget = true;
                normalCG.alpha = 1f;
            }
            else
            {
                _currentBackground.raycastTarget = false;
                normalCG.alpha = _interactableAlpha;
            }

            UpdateBackgroundColor(isIntractable);
        }

        private void UpdateBackgroundColor(bool state)
        {
            _currentBackground.color = state ? _backgroundColorNormal : _backgroundColorDisabled;
        }

        private void UpdateFrame()
        {
            if (!_currentFrameImage)
            {
                return;
            }
            
            _currentFrameImage.gameObject.SetActive(_enableFrame);
            
            _currentFrameImage.sprite = _frameSprite;
            _currentFrameImage.pixelsPerUnitMultiplier = _framePixelsPerUnitMultiplier;
            _currentFrameImage.color = _frameColor;
        }
        
        private void ResetScale()
        {
            SetScaleBackground(1,0.4f);
        }

        #endregion
    }
}
