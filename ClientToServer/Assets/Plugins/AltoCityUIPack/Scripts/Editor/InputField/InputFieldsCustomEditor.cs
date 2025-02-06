using Plugins.AltoCityUIPack.Scripting;
using Plugins.AltoCityUIPack.Scripts.InputField;
using UnityEditor;
using UnityEngine;

namespace Plugins.AltoCityUIPack.Scripts.Editor.InputField
{
    [CanEditMultipleObjects]
    [CustomEditor(typeof(InputFieldsCustom))]
    public class InputFieldsCustomEditor : UnityEditor.Editor
    {
        private GUISkin customSkin;
        private InputFieldsCustom inputFieldTarget;

        private void OnEnable()
        {
            inputFieldTarget = (InputFieldsCustom)target;
            
            customSkin = (GUISkin)Resources.Load("AltoCity-EditorDark");
        }

        public override void OnInspectorGUI()
        {
            EditorHandlerCustom.DrawComponentHeader(customSkin, "InputField Header");

            GUIContent[] toolbarTabs = new GUIContent[2];
            toolbarTabs[0] = new GUIContent("Content");
            toolbarTabs[1] = new GUIContent("Resources");

            inputFieldTarget.latestTabIndex =
                EditorHandlerCustom.DrawTabs(inputFieldTarget.latestTabIndex, toolbarTabs, customSkin);

            if (GUILayout.Button(new GUIContent("Content", "Content"), customSkin.FindStyle("Tab Content")))
                inputFieldTarget.latestTabIndex = 0;
            if (GUILayout.Button(new GUIContent("Resources", "Resources"), customSkin.FindStyle("Tab Resources")))
                inputFieldTarget.latestTabIndex = 1;

            GUILayout.EndHorizontal();
            
            var filledImage = serializedObject.FindProperty("_filledImage");
            var filledColor = serializedObject.FindProperty("_filledColor");
            var enableFilled = serializedObject.FindProperty("_enableFilled");

            var placeholder = serializedObject.FindProperty("_placeholder");
            var placeholderFont = serializedObject.FindProperty("_placeholderFont");
            var placeholderText = serializedObject.FindProperty("_placeholderText");
            var placeholderAlignment = serializedObject.FindProperty("_placeholderAlignment");
            var placeholderSize = serializedObject.FindProperty("_placeholderSize");
            var colorPlaceholder = serializedObject.FindProperty("_colorPlaceholder");
            var sizeScaleEnd = serializedObject.FindProperty("_sizeScaleEnd");
            
            var inputField = serializedObject.FindProperty("_inputField");
            var inputFieldColor = serializedObject.FindProperty("_inputFieldColor");
            var inputFieldFont = serializedObject.FindProperty("_inputFieldFont");
            var inputFieldText = serializedObject.FindProperty("_inputFieldText");
            var inputFieldAlignment = serializedObject.FindProperty("_inputFieldAlignment");
            var inputFieldSize = serializedObject.FindProperty("_inputFieldSize");
            var inputFieldColorSelected = serializedObject.FindProperty("_inputFieldColorSelected");
            var contentTypeInputField = serializedObject.FindProperty("_contentTypeInputField");
            
            var isInteractable = serializedObject.FindProperty("_isInteractable");

            var colorBlockInputFiled = serializedObject.FindProperty("_colorBlockInputFiled");
            
            var onSubmit = serializedObject.FindProperty("onSubmit");
            
            var enablePlaceholder = serializedObject.FindProperty("_enablePlaceholder");
            
            inputFieldTarget.UpdateUI();
            switch (inputFieldTarget.latestTabIndex)
            {
                case 0:
                    EditorHandlerCustom.DrawHeader(customSkin, "Content Header", 6);

                    GUILayout.BeginVertical(EditorStyles.helpBox);
                    
                    isInteractable.boolValue =
                        EditorHandlerCustom.DrawTogglePlain(isInteractable.boolValue, customSkin, "Enable Interactable");
                    
                    GUILayout.Space(5);
                    
                    GUILayout.EndVertical();
                    
                    #region Icon And Text

                    GUILayout.BeginVertical(EditorStyles.helpBox);
                    GUILayout.Space(-3);
                    
                    enableFilled.boolValue =
                        EditorHandlerCustom.DrawTogglePlain(enableFilled.boolValue, customSkin, "Enable Filled");
                    
                    GUILayout.Space(4);

                    if (enableFilled.boolValue)
                    {
                        EditorHandlerCustom.DrawPropertyCW(filledColor, customSkin, "Filled Color", 170);
                    }

                    GUILayout.EndVertical();
                    
                    GUILayout.BeginVertical(EditorStyles.helpBox);

                    enablePlaceholder.boolValue =
                        EditorHandlerCustom.DrawTogglePlain(enablePlaceholder.boolValue, customSkin, "Enable Placeholder");

                    GUILayout.Space(4);

                    if (enablePlaceholder.boolValue)
                    {
                        EditorHandlerCustom.DrawPropertyCW(placeholderFont, customSkin, "Placeholder Font", 170);
                        EditorHandlerCustom.DrawPropertyCW(placeholderAlignment, customSkin, "placeholder Alignment", 170);
                        EditorHandlerCustom.DrawPropertyCW(placeholderSize, customSkin, "Placeholder Size", 170);
                        EditorHandlerCustom.DrawPropertyCW(sizeScaleEnd, customSkin, "Size Scale End", 170);
                        
                        GUILayout.Space(4);
                        
                        EditorHandlerCustom.DrawPropertyCW(placeholderText, customSkin, "Placeholder Text", 170);
                        EditorHandlerCustom.DrawPropertyCW(colorPlaceholder, customSkin, "Placeholder Text Color", 170);
                    }

                    GUILayout.EndVertical();
                    
                    GUILayout.Space(20);
                    
                    GUILayout.BeginVertical(EditorStyles.helpBox);
                    
                    EditorHandlerCustom.DrawPropertyCW(inputFieldColor, customSkin, "InputField Color", 170);
                    EditorHandlerCustom.DrawPropertyCW(inputFieldFont, customSkin, "InputField Font", 170);
                    EditorHandlerCustom.DrawPropertyCW(inputFieldAlignment, customSkin, "InputField Alignment", 170);
                    EditorHandlerCustom.DrawPropertyCW(inputFieldSize, customSkin, "InputField Size", 170);
                    EditorHandlerCustom.DrawPropertyCW(inputFieldColorSelected, customSkin, "InputField Color Selected", 170);
                    EditorHandlerCustom.DrawPropertyCW(contentTypeInputField, customSkin, "Content Type InputField", 170);

                    GUILayout.Space(4);
                    
                    EditorHandlerCustom.DrawPropertyCW(inputFieldText, customSkin, "InputField Text", 170);
                    
                    GUILayout.EndVertical();
                    
                    GUILayout.Space(10);
                    
                    EditorHandlerCustom.DrawPropertyCW(colorBlockInputFiled, customSkin, "Colors InputFiled", 170);
                    
                    EditorHandlerCustom.DrawHeader(customSkin, "Events Header", 6);
                    
                    GUILayout.Space(20);
                    
                    GUILayout.BeginVertical(EditorStyles.helpBox);
                    
                    EditorGUILayout.PropertyField(onSubmit, new GUIContent("On Submit"), true);
                    
                    GUILayout.EndVertical();

                    #endregion
                break;

                case 1:
                    EditorHandlerCustom.DrawHeader(customSkin, "Core Header", 6);
                    
                    GUILayout.BeginVertical(EditorStyles.helpBox);

                    if (enableFilled.boolValue)
                    {
                        EditorHandlerCustom.DrawProperty(filledImage, customSkin, "Filled Image");
                    }

                    if (enablePlaceholder.boolValue)
                    {
                        EditorHandlerCustom.DrawProperty(placeholder, customSkin, "Placeholder");
                    }
                    
                    EditorHandlerCustom.DrawProperty(inputField, customSkin, "InputField");

                    GUILayout.EndVertical();
                    
                    break;
                
            }
            
            if (Application.isPlaying == false) { this.Repaint(); }
            serializedObject.ApplyModifiedProperties();
        }
    }
}
