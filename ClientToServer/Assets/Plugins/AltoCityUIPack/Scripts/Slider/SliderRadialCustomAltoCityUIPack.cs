using System;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Plugins.AltoCityUIPack.Scripts.Slider
{
    public class SliderRadialCustomAltoCityUIPack : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler
    {
        #region Field

        public TextMeshProUGUI valueText;
        [SerializeField] private bool showValue = true;
        [SerializeField, Range(1, 150)] private float _sizeText;
        [SerializeField] private Color _textColor;
        
        [SerializeField] private int _roundValue;
        [SerializeField, Range(0, 50)] private float _pixelsUnitBackgroundValue;

        [SerializeField] private Image _handleImage;
        [SerializeField] private Sprite _customHandle;
        [SerializeField] private Color _handleColor;
        
        public Image _fillImage;
        [SerializeField] private Color _fillColor;
        
        [SerializeField] private Image _backgroundSliderImage;
        [SerializeField] private Color _backgroundSliderColor;
        
        // Content
        public float currentValue = 50.0f;
        
        public Transform indicatorPivot;

        // Settings
        [Range(0, 8)] public int decimals;
        public bool isPercent;
        public SliderStartPoint startPoint = SliderStartPoint.Left;

        // Events
        public SliderEvent onValueChanged = new();
        public Action<float> OnValueFinallyChanged;
        
        //Sound
        [SerializeField] protected bool _useClickSound = true;
        [SerializeField] protected AudioClip _clickSound;
        [SerializeField] protected bool _useFocusedSound = true;
        [SerializeField] protected AudioClip _focusedSound;
        
        [SerializeField] private RectTransform hitRectTransform;
        [SerializeField] private bool isPointerDown;
        [SerializeField] private float currentAngle;
        [SerializeField] private float currentAngleOnPointerDown;
        [SerializeField] private float valueDisplayPrecision;
        
        [SerializeField] private float minValue = 0;
        [SerializeField] private float maxValue = 100;
        [SerializeField] private bool useRoundValue;

        #region Properties

        public float MinValue => minValue;
        public float MaxValue => maxValue;
        public int RoundValue => _roundValue;

        #endregion
        
        #endregion

        #region SliderPropereties

        public float SliderAngle
        {
            get { return currentAngle; }
            set { currentAngle = Mathf.Clamp(value, 0.0f, 360.0f); }
        }
        
        // Slider value with applied display precision, i.e. the number of decimals to show.
        public float SliderValue
        {
            get { return (long)(SliderValueRaw * valueDisplayPrecision) / valueDisplayPrecision; }
            set { SliderValueRaw = value; }
        }
        
        // Raw slider value, i.e. without any display precision applied to its value.
        public float SliderValueRaw
        {
            get { return SliderAngle / 360.0f * maxValue; }
            set { SliderAngle = value * 360.0f / maxValue; }
        }

        #endregion
        
        private void Start()
        {
            valueDisplayPrecision = Mathf.Pow(10, decimals);
            
            SliderValue = currentValue;
            onValueChanged.Invoke(SliderValueRaw);
            UpdateSliderAngle();
        }
        
        public void UpdateUI()
        {
            UpdateFill();
            UpdateHandle();
            UpdateBackgroundSlider();
            UpdateText();
        }

        #region Use

        public float GetSliderValue()
        {
            return useRoundValue ? (float)Math.Round(SliderValue, _roundValue) : SliderValue;
        }

        public void SetValueSlider(float value)
        {
            Start();
            SliderValue = value;
            UpdateSliderAngle();
        }

        #endregion

        #region Interface

        public void OnPointerDown(PointerEventData eventData)
        {
            if (_useClickSound)
            {
                _clickSound.PlayUISound();
            }
            
            hitRectTransform = eventData.pointerCurrentRaycast.gameObject.GetComponent<RectTransform>();
            isPointerDown = true;
            currentAngleOnPointerDown = SliderAngle;
            HandleSliderMouseInput(eventData, true);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (!_useFocusedSound)
            {
                return;
            }
            
            _focusedSound.PlayUISound();
        }
        
        public void OnPointerUp(PointerEventData eventData)
        {
            hitRectTransform = null;
            isPointerDown = false;
            OnValueFinallyChanged?.Invoke(SliderValueRaw);
        }
        
        public void OnDrag(PointerEventData eventData)
        {
            if (currentValue >= minValue)
            {
                HandleSliderMouseInput(eventData, false);
            }
            else if (currentValue <= minValue)
            {
                SliderValueRaw = minValue;
            }
        }

        #endregion

        private bool HasValueChanged()
        {
            return SliderAngle != currentAngleOnPointerDown;
        }
        
        private void HandleSliderMouseInput(PointerEventData eventData, bool allowValueWrap)
        {
            if (!isPointerDown)
                return;

            Vector2 localPos;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(hitRectTransform, eventData.position, eventData.pressEventCamera, out localPos);
            float newAngle = Mathf.Atan2(-localPos.y, localPos.x) * Mathf.Rad2Deg + 180f;

            if (!allowValueWrap)
            {
                currentAngle = SliderAngle;
                bool needsClamping = Mathf.Abs(newAngle - currentAngle) >= 180;

                if (needsClamping)
                    newAngle = currentAngle < newAngle ? 0.0f : 360.0f;
            }

            SliderAngle = newAngle;
            UpdateSliderAngle();

            if (HasValueChanged())
            {
                onValueChanged.Invoke(SliderValueRaw);   
            }
        }

        #region Updates

        private void UpdateSliderAngle()
        {
            if (!indicatorPivot || !_fillImage || !valueText)
            {
                return;
            }
            
            if (SliderValueRaw >= minValue)
            {
                float normalizedAngle = SliderAngle / 360.0f;
                indicatorPivot.transform.localEulerAngles = new Vector3(180.0f, 0.0f, SliderAngle);
                _fillImage.fillAmount = normalizedAngle;
                
                string value = useRoundValue ? Math.Round(SliderValue, _roundValue).ToString() : SliderValue.ToString();
                valueText.text =  value + (isPercent ? "%" : "");
                currentValue = SliderValue;
            }
        }

        private void UpdateText()
        {
            if (!valueText)
            {
                return;
            }

            valueText.fontSize = _sizeText;
            valueText.color = _textColor;
            valueText.gameObject.SetActive(showValue);   
        }
        
        private void UpdateFill()
        {
            if (!_fillImage)
            {
                return;
            }
            
            _fillImage.color =_fillColor;
        }

        private void UpdateHandle()
        {
            if (!_handleImage)
            {
                return;
            }

            _handleImage.sprite = _customHandle;
            _handleImage.color = _handleColor;
        }
        
        private void UpdateBackgroundSlider()
        {
            if (!_backgroundSliderImage)
            {
                return;
            }
            
            _backgroundSliderImage.color = _backgroundSliderColor;
        } 

        #endregion

    }
}
