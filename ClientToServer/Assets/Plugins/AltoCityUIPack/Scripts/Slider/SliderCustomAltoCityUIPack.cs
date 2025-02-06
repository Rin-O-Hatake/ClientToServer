using System;
using Plugins.AltoCityUIPack.Scripts.InputField;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Plugins.AltoCityUIPack.Scripts.Slider
{
    public sealed class SliderCustomAltoCityUIPack : MonoBehaviour
    {
        public UnityEngine.UI.Slider mainSlider;
        
        [SerializeField] private InputFieldsCustom _inputFieldCustom;
        [SerializeField] private int _roundValue;
        [SerializeField] private Color _backgroundValueTextColor;
        [SerializeField] private Image _backgroundValueTextImage;
        [SerializeField] private Sprite _backgroundValueTextSprite;
        [SerializeField, Range(0, 50)] private float _pixelsUnitBackgroundValue;

        [SerializeField] private Image _handleImage;
        [SerializeField] private Color _handleColor;
        
        [SerializeField] private Image _fillImage;
        [SerializeField] private Color _fillColor;
        
        [SerializeField] private Image _backgroundSliderImage;
        [SerializeField] private Color _backgroundSliderColor;
        
        [SerializeField] private bool useHoverSound = true;
        [SerializeField] private bool useClickSound = true;
        [SerializeField] private AudioClip hoverSound;
        [SerializeField] private AudioClip clickSound;

        [SerializeField] private bool usePercent;
        [SerializeField] private bool useRoundValue;
        [SerializeField] private float minValue;
        [SerializeField] private float maxValue;

        [System.Serializable]
        public class SliderEvent : UnityEvent<float> { }
        
        [SerializeField] private SliderEvent onValueChanged = new();
        
        [SerializeField] private SliderEvent _sliderEvent;

        #region Properties

        public SliderEvent SliderEvents => _sliderEvent;

        #endregion

        private void Awake()
        {
            if (useClickSound)
            {
                mainSlider.onValueChanged.AddListener(delegate
                {
                    clickSound.PlayUISound();
                });
            }

            mainSlider.onValueChanged.AddListener(delegate
            {
                _sliderEvent.Invoke(mainSlider.value);
            });

            UpdateUI();
        }
        
        #region Use

        public void SetValue(float value)
        {
            if (!mainSlider)
            {
                return;
            }
            
            mainSlider.value = value;
            UpdateUI();
        }

        public float GetValue()
        {
            return mainSlider.value;
        }

        public void SetText(string text)
        {
            if (!_inputFieldCustom)
            {
                return;
            }
            
            _inputFieldCustom.SetInputField(text);
        }

        #endregion

        public void UpdateUI()
        {
            UpdateValueText();
            UpdateBackgroundValueText();
            UpdateField();
            UpdateHandle();
            UpdateBackgroundSlider();
            UpdateTypeInputField();
        }

        #region Updates

        public void UpdateValueText()
        {
            if (!_inputFieldCustom || !mainSlider)
            {
                return;
            }
            
            string value = useRoundValue ? Math.Round(mainSlider.value, _roundValue).ToString() : mainSlider.value.ToString();
            _inputFieldCustom.SetPlaceholder(value + (usePercent ? "%" : string.Empty));
            _sliderEvent.Invoke(mainSlider.value);
        }

        private void UpdateBackgroundValueText()
        {
            if (!_backgroundValueTextImage)
            {
                return;
            }
            
            _backgroundValueTextImage.sprite = _backgroundValueTextSprite;
            _backgroundValueTextImage.color = _backgroundValueTextColor;
            _backgroundValueTextImage.pixelsPerUnitMultiplier = _pixelsUnitBackgroundValue;
        }

        private void UpdateField()
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

        private void UpdateTypeInputField()
        {
            if (!_inputFieldCustom || !mainSlider)
            {
                return;
            }
            
            _inputFieldCustom.SetTypeInputField(mainSlider.wholeNumbers
                ? TMP_InputField.ContentType.IntegerNumber
                : TMP_InputField.ContentType.DecimalNumber);
        }
        
        public void SetValue(string value)
        {
            if (!mainSlider || !_inputFieldCustom || string.IsNullOrEmpty(value))
            {
                return;
            }
            
            mainSlider.value = float.Parse(value);
            UpdateUI();
            _inputFieldCustom.SetInputField(string.Empty);
        }

        #endregion

        #region Trigger

        public void OnPointerEnter()
        {
            if (useHoverSound)
            {
                hoverSound.PlayUISound();
            }
        }

        #endregion
    }
}
