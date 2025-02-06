using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;

namespace Plugins.AltoCityUIPack.Scripts.InputField
{
    [ExecuteInEditMode]
    public class InputFieldsCustom : MonoBehaviour
    {
        #region Fields

        //PlaceHolder
        [SerializeField] private TMP_FontAsset _placeholderFont;
        [SerializeField] private TMP_Text _placeholder;
        [SerializeField] private string _placeholderText;
        [SerializeField] private TextAlignmentOptions _placeholderAlignment;
        [SerializeField, Range(0,100)] private float _placeholderSize;
        [SerializeField] private Color _colorPlaceholder;
        [SerializeField] private float _sizeScaleEnd;
        
        //Field
        [SerializeField] private Image _filledImage;
        [SerializeField] private Color _filledColor;
        [SerializeField] private bool _enableFilled; 

        //InputFiled
        [SerializeField] private TMP_InputField _inputField;
        [SerializeField] private Color _inputFieldColor;
        [SerializeField] private TMP_FontAsset _inputFieldFont;
        [SerializeField] private string _inputFieldText;
        [SerializeField] private TextAlignmentOptions _inputFieldAlignment;
        [SerializeField, Range(0,100)] private float _inputFieldSize;
        [SerializeField] private Color _inputFieldColorSelected;
        [SerializeField] private TMP_InputField.ContentType _contentTypeInputField = TMP_InputField.ContentType.Standard;
        
        //Interactable 
        [SerializeField] private bool _isInteractable = true;
        
        [SerializeField] private ColorBlock _colorBlockInputFiled;
        [SerializeField] private bool _enablePlaceholder;

        private bool _pauseTimeSubmit;
        
        [Header("Events")]
        public UnityEvent onSubmit;
        
        public int latestTabIndex;

        #region Properties

        public string InputFieldText => _inputField.text;
        
        public bool IsFocused => _inputField.isFocused;
        
        public TMP_InputField.ContentType ContentTypeInputField => _contentTypeInputField;

        public TMP_InputField.SelectionEvent OnSelect => _inputField.onSelect;
        public TMP_InputField.SubmitEvent OnEndEdit => _inputField.onEndEdit;
        public TMP_InputField.OnChangeEvent OnValueChanged => _inputField.onValueChanged;

        #endregion

        #endregion

        private void OnEnable()
        {
            _inputField.onSelect.AddListener(delegate { HidePlaceholder(); });
            _inputField.onEndEdit.AddListener(delegate { UpdateState(); });
            _inputField.onValueChanged.AddListener(delegate { UpdateState(); });
            _inputField.onSubmit.AddListener(delegate { Submit(); });
            UpdateState();
            UpdateUI();
        }

        private void OnDestroy()
        {
            _inputField.onSelect.RemoveListener(delegate { HidePlaceholder(); });
            _inputField.onEndEdit.RemoveListener(delegate { UpdateState(); });
            _inputField.onValueChanged.RemoveListener(delegate { UpdateState(); });
            _inputField.onSubmit.RemoveListener(delegate { Submit(); });
        }

        private async void Submit()
        {
            if (_pauseTimeSubmit)
            {
                return;
            }

            _pauseTimeSubmit = true;
            onSubmit?.Invoke();

            await Task.Delay(300);

            _pauseTimeSubmit = false;
        }

        #region Use

        public void SetInteractable(bool state)
        {
            _isInteractable = state;
            UpdateInteractable();
        }

        public void SetPlaceholder(string text)
        {
            _placeholderText = text;
            
            UpdatePlaceHolder();
	    	UpdateState();
        }

        public void SetInputField(string text)
        {
            _inputFieldText = text;
            
            UpdateInputField();
            UpdateState();
        }

        public void SelectInputField()
        {
            _inputField.Select();
        	UpdateState();
        }

        public void SetTypeInputField(TMP_InputField.ContentType contentType)
        {
            _contentTypeInputField = contentType;
			UpdateState();
        }

        #endregion

        #region Updates

        public void UpdateUI()
        {
            UpdateInputField();
            UpdateFilled();
            UpdatePlaceHolder();
            UpdateState();
            UpdateInteractable();
        }

        private void UpdateFilled()
        {
            if (!_filledImage)
            {
                return;
            }

            if (!_enableFilled)
            {
                _filledImage.gameObject.SetActive(false);
                return;
            }
            
            _filledImage.gameObject.SetActive(true);
            _filledImage.color = _filledColor;
        }

        private void UpdatePlaceHolder()
        {
            if (!_placeholder)
            {
                return;
            }
            
            if (!_enablePlaceholder)
            {
                EnablePlaceholder(false);
                return;
            }

            EnablePlaceholder(true);

            _placeholder.color = _colorPlaceholder;
            _placeholder.font = _placeholderFont;
            _placeholder.fontSize = _placeholderSize;
            _placeholder.text = _placeholderText;
            _placeholder.alignment = _placeholderAlignment;
        }

        private void UpdateInputField()
        {
            if (!_inputField)
            {
                return;
            }
            
            _inputField.textComponent.color = _inputFieldColor;
            _inputField.fontAsset = _inputFieldFont;
            _inputField.pointSize = _inputFieldSize;
            _inputField.text = _inputFieldText;
            _inputField.textComponent.alignment = _inputFieldAlignment;
            _inputField.selectionColor = _inputFieldColorSelected;
            _inputField.contentType = _contentTypeInputField;
            
            _inputField.colors = _colorBlockInputFiled;
        }

        private void UpdateInteractable()
        {
            if (!_inputField)
            {
                return;
            }

            _inputField.interactable = _isInteractable;
        }

        public void UpdateInputFieldText(string text)
        {
            _inputFieldText = text;
        }

        #endregion

        #region Animation

        private void UpdateState()
        {
            if (_inputField.text.Length == 0) 
            {
                if (_enablePlaceholder)
                {
                    ExpendPlaceholder();
                }
            }
            else
            {
                HidePlaceholder();
            }
        }

        private void HidePlaceholder()
        {
            EnablePlaceholder(false);
        }
        
        private void ExpendPlaceholder()
        {
            EnablePlaceholder(true);
        }

        #endregion
        
        private void EnablePlaceholder(bool state)
        {
            Color color = _placeholder.color;
            if (state)
            {
                color.a = 1;
                _placeholder.color = color;
                return;
            }

            color.a = 0;
            _placeholder.color = color;
        }
    }
}
