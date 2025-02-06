using Cysharp.Threading.Tasks;
using Plugins.AltoCityUIPack.Scripts.InputField;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

namespace Core.ServerToClient
{
    public class LoggerClientServer : MonoBehaviour
    {
        #region Fields

        [Title("Logger Client Server Properties")]
        [SerializeField] private InputFieldsCustom _nameInput;
        [SerializeField] private InputFieldsCustom _ageInput;
        [SerializeField] private TMP_Text _errorOrSuccessText;
        
        [Title("Colors Text")]
        [SerializeField] private Color _errorColor;
        [SerializeField] private Color _successColor;
        
        private string _namePerson;
        private int _agePerson;

        #region Error data

        private const string IncorrectAge = "Enter the correct age";
        private const string NotEnteredData = "Enter the data";

        #endregion
        
        #endregion

        #region MonoBehaviour

        private void Start()
        {
            _nameInput.OnEndEdit.AddListener(SetNamePerson);
            _ageInput.OnEndEdit.AddListener(SetAgePerson);
        }

        private void OnDestroy()
        {
            _nameInput.OnEndEdit.RemoveListener(SetNamePerson);
            _ageInput.OnEndEdit.RemoveListener(SetAgePerson);
        }

        #endregion
        
        #region Buttons

        public void ButtonSetCharacterToServer()
        {
            if (!CheckError())
            {
                return;
            }
            
            SetCharacter().Forget();
        }

        #endregion

        #region Client to Server

        private async UniTaskVoid SetCharacter()
        {
            APIResponseDataT<DataPerson> response = await ClientManager.SetPerson_Server(_namePerson, _agePerson);
            
            DataPerson dataPerson = response.Data;

            ConclusionSuccess(dataPerson);
        }

        #endregion

        #region Data Person UI

        private void SetNamePerson(string newName)
            => _namePerson = newName;

        private void SetAgePerson(string newAge)
        {
            if (string.IsNullOrEmpty(newAge))
            {
                return;
            }
            
            _agePerson = int.Parse(newAge);   
        }

        private bool CheckError()
        {
            if (string.IsNullOrEmpty(_nameInput.InputFieldText) || string.IsNullOrEmpty(_ageInput.InputFieldText))
            {
                SetColorErrorText();
                _errorOrSuccessText.text = NotEnteredData;
                return false;
            }
            
            if (_agePerson < 0)
            {
                SetColorErrorText();
                _errorOrSuccessText.text = IncorrectAge;
                return false;
            }
            
            _errorOrSuccessText.text = string.Empty;
            return true;
        }

        private void SetColorErrorText()
        {
            _errorOrSuccessText.color = _errorColor;
        }

        private void ConclusionSuccess(DataPerson dataPerson)
        {
            string successText = $"Status: {dataPerson.Status} Message: {dataPerson.Message}";
            string dataPersonText = $"Name: {dataPerson.UserData.UserName} Age: {dataPerson.UserData.Age}";
            
            _errorOrSuccessText.color = _successColor;
            _errorOrSuccessText.text = dataPersonText;
            Debug.Log($"{successText} {dataPersonText}");
        }

        #endregion
    }
}
