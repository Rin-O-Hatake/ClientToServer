using System;
using Plugins.AltoCityUIPack.Scripts.Slider;
using UnityEditor;
using UnityEngine;

namespace Plugins.AltoCityUIPack.Scripts.Editor.Slider
{
    [CustomEditor(typeof(SliderRadialCustomAltoCityUIPack))]
    public class SliderRadialCustomAltoCityUIPackEditor : SliderCustomAltoCityUIPackEditor
    {
        private SliderRadialCustomAltoCityUIPack _sliderTarget;
        private GUISkin customSkin;
        private int currentTab;
        
        private void OnEnable()
        {
            _sliderTarget = (SliderRadialCustomAltoCityUIPack)target;
            
            customSkin = (GUISkin)Resources.Load("AltoCity-EditorDark");
        }
        public override void OnInspectorGUI()
        {
            
            EditorHandlerCustom.DrawComponentHeader(customSkin, "Slider Top Header");

            GUIContent[] toolbarTabs = new GUIContent[3];
            toolbarTabs[0] = new GUIContent("Content");
            toolbarTabs[1] = new GUIContent("Resources");
            toolbarTabs[2] = new GUIContent("Settings");

            currentTab = EditorHandlerCustom.DrawTabs(currentTab, toolbarTabs, customSkin);

            if (GUILayout.Button(new GUIContent("Content", "Content"), customSkin.FindStyle("Tab Content")))
                currentTab = 0;
            if (GUILayout.Button(new GUIContent("Resources", "Resources"), customSkin.FindStyle("Tab Resources")))
                currentTab = 1;
            if (GUILayout.Button(new GUIContent("Settings", "Settings"), customSkin.FindStyle("Tab Settings")))
                currentTab = 2;

            GUILayout.EndHorizontal();
            
            var currentValue = serializedObject.FindProperty("currentValue");
            var sizeText = serializedObject.FindProperty("_sizeText");
            var textColor = serializedObject.FindProperty("_textColor");
            
            var minValue = serializedObject.FindProperty("minValue");
            var maxValue = serializedObject.FindProperty("maxValue");
            
            var indicatorPivot = serializedObject.FindProperty("indicatorPivot");
                
            var valueText = serializedObject.FindProperty("valueText");
            var roundValue = serializedObject.FindProperty("_roundValue");
            var pixelsUnitBackgroundValue = serializedObject.FindProperty("_pixelsUnitBackgroundValue");
            
            var handleImage = serializedObject.FindProperty("_handleImage");
            var handleColor = serializedObject.FindProperty("_handleColor");
            var customHandle = serializedObject.FindProperty("_customHandle");

            var fillImage = serializedObject.FindProperty("_fillImage");
            var fillColor = serializedObject.FindProperty("_fillColor");
            
            var backgroundSliderImage = serializedObject.FindProperty("_backgroundSliderImage");
            var backgroundSliderColor = serializedObject.FindProperty("_backgroundSliderColor");
            
            var usePercent = serializedObject.FindProperty("isPercent");
            var useRoundValue = serializedObject.FindProperty("useRoundValue");
            var showValue = serializedObject.FindProperty("showValue");
            var useClickSound = serializedObject.FindProperty("_useClickSound");
            var clickSound = serializedObject.FindProperty("_clickSound");
            var useFocusedSound = serializedObject.FindProperty("_useFocusedSound");
            var focusedSound = serializedObject.FindProperty("_focusedSound");
            
            
            var onValueChanged = serializedObject.FindProperty("onValueChanged");

            _sliderTarget.UpdateUI();
            switch (currentTab)
            {
                case 0:
                    EditorHandlerCustom.DrawHeader(customSkin, "Content Header", 6);
                    
                        GUILayout.BeginHorizontal(EditorStyles.helpBox);
                    
                        EditorGUILayout.LabelField(new GUIContent("Current Value"), customSkin.FindStyle("Text"), GUILayout.Width(120));
                        currentValue.floatValue = EditorGUILayout.Slider(currentValue.floatValue, _sliderTarget.MinValue, _sliderTarget.MaxValue);
                    
                        GUILayout.EndHorizontal();
                        GUILayout.BeginHorizontal(EditorStyles.helpBox);

                        EditorHandlerCustom.DrawPropertyCW(minValue, customSkin, "Min Value", 180);

                        GUILayout.EndHorizontal();
                        GUILayout.BeginHorizontal(EditorStyles.helpBox);

                        EditorHandlerCustom.DrawPropertyCW(maxValue, customSkin, "Max Value", 180);

                        GUILayout.EndHorizontal();
                        
                        GUILayout.Space(5);
                        
                        GUILayout.BeginVertical(EditorStyles.helpBox);
                    
                        EditorHandlerCustom.DrawPropertyCW(pixelsUnitBackgroundValue, customSkin, "Pixels Unit Background Value", 180);
                            
                        GUILayout.EndHorizontal();
                        
                        GUILayout.Space(5);
                        GUILayout.BeginVertical(EditorStyles.helpBox);
                        
                        EditorHandlerCustom.DrawPropertyCW(fillColor, customSkin, "Fill Color", 180);
                        
                        GUILayout.Space(5);
                        
                        EditorHandlerCustom.DrawPropertyCW(handleColor, customSkin, "Handle Color", 180);
                        EditorHandlerCustom.DrawPropertyCW(customHandle, customSkin, "Custom Handle Sprite", 180);

                    GUILayout.Space(5);
                        
                        EditorHandlerCustom.DrawPropertyCW(backgroundSliderColor, customSkin, "Background Slider Color", 180);
                            
                        GUILayout.EndHorizontal();
                        
                        GUILayout.Space(5);
                        
                        GUILayout.BeginVertical(EditorStyles.helpBox);
                        
                        useRoundValue.boolValue = EditorHandlerCustom.DrawTogglePlain(useRoundValue.boolValue, customSkin, "Use Round Value");

                        GUILayout.Space(5);
                        
                        if (useRoundValue.boolValue)
                        {
                            EditorHandlerCustom.DrawPropertyCW(roundValue, customSkin, "Round Value", 130);
                        }
                        
                        usePercent.boolValue = EditorHandlerCustom.DrawTogglePlain(usePercent.boolValue, customSkin, "Use Percent");
                        
                        GUILayout.Space(5);
                    
                        showValue.boolValue = EditorHandlerCustom.DrawTogglePlain(showValue.boolValue, customSkin, "Show value Text");
                    
                        GUILayout.Space(5);

                        if (showValue.boolValue)
                        {
                            EditorHandlerCustom.DrawPropertyCW(sizeText, customSkin, "Size Text", 180);
                            EditorHandlerCustom.DrawPropertyCW(textColor, customSkin, "Text Color", 180);
                        }
                        GUILayout.EndHorizontal();

                        EditorHandlerCustom.DrawHeader(customSkin, "Events Header", 10);
                    
                        EditorGUILayout.PropertyField(onValueChanged, new GUIContent("On Value Changed"), true);
                    break;

                case 1:
                    EditorHandlerCustom.DrawHeader(customSkin, "Core Header", 6);
                    EditorHandlerCustom.DrawPropertyCW(handleImage, customSkin, "Handle Image", 200);
                    EditorHandlerCustom.DrawPropertyCW(fillImage, customSkin, "Fill Image", 200);
                    EditorHandlerCustom.DrawPropertyCW(backgroundSliderImage, customSkin, "Background Slider Image", 200);
                    EditorHandlerCustom.DrawPropertyCW(indicatorPivot, customSkin, "Indicator Pivot", 200);
                    
                    if (showValue.boolValue)
                    {
                        EditorHandlerCustom.DrawPropertyCW(valueText, customSkin, "Label Text", 200);
                    }
                    GUILayout.Space(4);
                    break;

                case 2:
                    EditorHandlerCustom.DrawHeader(customSkin, "Options Header", 6);
                    useClickSound.boolValue = EditorHandlerCustom.DrawToggle(useClickSound.boolValue, customSkin, "Enable Click Sound");

                    if (useClickSound.boolValue)
                    {
                        EditorHandlerCustom.DrawProperty(clickSound, customSkin, "Click Sound");
                    }
                    
                    useFocusedSound.boolValue = EditorHandlerCustom.DrawToggle(useFocusedSound.boolValue, customSkin, "Enable Focused Sound");

                    if (useFocusedSound.boolValue)
                    {
                        EditorHandlerCustom.DrawProperty(focusedSound, customSkin, "Focused Sound");
                    }

                    EditorHandlerCustom.DrawHeader(customSkin, "UIM Header", 10);

                    break;
            }
            
            if (_sliderTarget._fillImage != null && _sliderTarget.indicatorPivot != null && _sliderTarget.valueText != null)
            {
                _sliderTarget.SliderValueRaw = currentValue.floatValue;
                float normalizedAngle = _sliderTarget.SliderAngle / 360.0f;
                _sliderTarget.indicatorPivot.transform.localEulerAngles = new Vector3(180.0f, 0.0f, _sliderTarget.SliderAngle);
                _sliderTarget._fillImage.fillAmount = normalizedAngle;
                _sliderTarget.valueText.text = Math.Round(currentValue.floatValue, _sliderTarget.RoundValue) + (_sliderTarget.isPercent ? "%" : "");
            }

            if (Application.isPlaying == false) { this.Repaint(); }
            serializedObject.ApplyModifiedProperties();
        }

    }
}
