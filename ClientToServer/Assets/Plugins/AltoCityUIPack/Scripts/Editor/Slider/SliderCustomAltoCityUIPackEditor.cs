using Plugins.AltoCityUIPack.Scripts.Slider;
using UnityEditor;
using UnityEngine;

namespace Plugins.AltoCityUIPack.Scripts.Editor.Slider
{
    [CustomEditor(typeof(SliderCustomAltoCityUIPack))]
    public class SliderCustomAltoCityUIPackEditor : UnityEditor.Editor
    {
        private GUISkin customSkin;
        private SliderCustomAltoCityUIPack _sliderTarget;
        private int currentTab;

        private void OnEnable()
        {
            _sliderTarget = (SliderCustomAltoCityUIPack)target;
            
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

            var sliderEvent = serializedObject.FindProperty("_sliderEvent");
            var sliderObject = serializedObject.FindProperty("mainSlider");
            
            var inputFieldCustom = serializedObject.FindProperty("_inputFieldCustom");
            var roundValue = serializedObject.FindProperty("_roundValue");

            var backgroundValueTextColor = serializedObject.FindProperty("_backgroundValueTextColor");
            var backgroundValueTextImage = serializedObject.FindProperty("_backgroundValueTextImage");
            var backgroundValueTextSprite = serializedObject.FindProperty("_backgroundValueTextSprite");
            var pixelsUnitBackgroundValue = serializedObject.FindProperty("_pixelsUnitBackgroundValue");
            
            var handleImage = serializedObject.FindProperty("_handleImage");
            var handleColor = serializedObject.FindProperty("_handleColor");
            
            var fillImage = serializedObject.FindProperty("_fillImage");
            var fillColor = serializedObject.FindProperty("_fillColor");
            
            var backgroundSliderImage = serializedObject.FindProperty("_backgroundSliderImage");
            var backgroundSliderColor = serializedObject.FindProperty("_backgroundSliderColor");
            
            var usePercent = serializedObject.FindProperty("usePercent");
            var useRoundValue = serializedObject.FindProperty("useRoundValue");
            var showValue = serializedObject.FindProperty("showValue");
            var useHoverSound = serializedObject.FindProperty("useHoverSound");
            var useClickSound = serializedObject.FindProperty("useClickSound");
            var hoverSound = serializedObject.FindProperty("hoverSound");
            var clickSound = serializedObject.FindProperty("clickSound");

            _sliderTarget.UpdateUI();
            switch (currentTab)
            {
                case 0:
                    EditorHandlerCustom.DrawHeader(customSkin, "Content Header", 6);

                    if (_sliderTarget.mainSlider != null)
                    {
                        GUILayout.BeginHorizontal(EditorStyles.helpBox);

                        EditorGUILayout.LabelField(new GUIContent("Current Value"), customSkin.FindStyle("Text"), GUILayout.Width(120));
                        _sliderTarget.mainSlider.value = EditorGUILayout.Slider(_sliderTarget.mainSlider.value, _sliderTarget.mainSlider.minValue, _sliderTarget.mainSlider.maxValue);

                        GUILayout.EndHorizontal();
                        GUILayout.BeginHorizontal(EditorStyles.helpBox);

                        EditorGUILayout.LabelField(new GUIContent("Min Value"), customSkin.FindStyle("Text"), GUILayout.Width(120));

                        if (_sliderTarget.mainSlider.wholeNumbers == false)
                            _sliderTarget.mainSlider.minValue = EditorGUILayout.FloatField(_sliderTarget.mainSlider.minValue);
                        else
                            _sliderTarget.mainSlider.minValue = EditorGUILayout.IntField((int)_sliderTarget.mainSlider.minValue);

                        GUILayout.EndHorizontal();
                        GUILayout.BeginHorizontal(EditorStyles.helpBox);

                        EditorGUILayout.LabelField(new GUIContent("Max Value"), customSkin.FindStyle("Text"), GUILayout.Width(120));

                        if (!_sliderTarget.mainSlider.wholeNumbers)
                        {
                            _sliderTarget.mainSlider.maxValue = EditorGUILayout.FloatField(_sliderTarget.mainSlider.maxValue);   
                        }
                        else
                        {
                            _sliderTarget.mainSlider.maxValue = EditorGUILayout.IntField((int)_sliderTarget.mainSlider.maxValue);   
                        }

                        GUILayout.EndHorizontal();
                        
                        GUILayout.Space(5);
                        
                        GUILayout.BeginVertical(EditorStyles.helpBox);
                        
                        EditorHandlerCustom.DrawPropertyCW(backgroundValueTextColor, customSkin, "Background Value Text Color", 180);
                        EditorHandlerCustom.DrawPropertyCW(backgroundValueTextSprite, customSkin, "Background Value Text Sprite", 180);
                        EditorHandlerCustom.DrawPropertyCW(pixelsUnitBackgroundValue, customSkin, "Pixels Unit Background Value", 180);

                        GUILayout.EndHorizontal();
                        
                        GUILayout.Space(5);
                        GUILayout.BeginVertical(EditorStyles.helpBox);
                        
                        EditorHandlerCustom.DrawPropertyCW(fillColor, customSkin, "Fill Color", 180);
                        
                        GUILayout.Space(5);
                        
                        EditorHandlerCustom.DrawPropertyCW(handleColor, customSkin, "Handle Color", 180);
                        
                        GUILayout.Space(5);
                        
                        EditorHandlerCustom.DrawPropertyCW(backgroundSliderColor, customSkin, "Background Slider Color", 180);
                            
                        GUILayout.EndHorizontal();
                        
                        GUILayout.Space(5);
                        
                        GUILayout.BeginHorizontal(EditorStyles.helpBox);

                        _sliderTarget.mainSlider.wholeNumbers = GUILayout.Toggle(_sliderTarget.mainSlider.wholeNumbers, new GUIContent("Use Whole Numbers"), customSkin.FindStyle("Toggle"));
                        _sliderTarget.mainSlider.wholeNumbers = GUILayout.Toggle(_sliderTarget.mainSlider.wholeNumbers, new GUIContent(""), customSkin.FindStyle("Toggle Helper"));

                        GUILayout.EndHorizontal();
                        
                        GUILayout.BeginVertical(EditorStyles.helpBox);
                        
                        useRoundValue.boolValue = EditorHandlerCustom.DrawTogglePlain(useRoundValue.boolValue, customSkin, "Use Round Value");

                        GUILayout.Space(5);
                        
                        if (useRoundValue.boolValue)
                        {
                            EditorHandlerCustom.DrawPropertyCW(roundValue, customSkin, "Round Value", 130);
                        }
                        
                        usePercent.boolValue = EditorHandlerCustom.DrawTogglePlain(usePercent.boolValue, customSkin, "Use Percent");
                        
                        GUILayout.Space(5);
                        
                        GUILayout.EndHorizontal();
                    }

                    else
                        EditorGUILayout.HelpBox("'Main Slider' is not assigned. Go to Resources tab and assign the correct variable.", MessageType.Error);

                    EditorHandlerCustom.DrawHeader(customSkin, "Events Header", 10);
                    EditorGUILayout.PropertyField(sliderEvent, new GUIContent("On Value Changed"), true);
                    break;

                case 1:
                    EditorHandlerCustom.DrawHeader(customSkin, "Core Header", 6);
                    
                    EditorHandlerCustom.DrawPropertyCW(sliderObject, customSkin, "Slider Source", 200);
                    EditorHandlerCustom.DrawPropertyCW(backgroundValueTextImage, customSkin, "Background Value Text Image", 200);
                    EditorHandlerCustom.DrawPropertyCW(handleImage, customSkin, "Handle Image", 200);
                    EditorHandlerCustom.DrawPropertyCW(fillImage, customSkin, "Fill Image", 200);
                    EditorHandlerCustom.DrawPropertyCW(backgroundSliderImage, customSkin, "Background Slider Image", 200);
                    
                    if (showValue.boolValue)
                    {
                        EditorHandlerCustom.DrawPropertyCW(inputFieldCustom, customSkin, "Value Text", 200);
                    }
                    GUILayout.Space(4);
                    break;

                case 2:
                    EditorHandlerCustom.DrawHeader(customSkin, "Options Header", 6);
                    useHoverSound.boolValue = EditorHandlerCustom.DrawToggle(useHoverSound.boolValue, customSkin, "Enable Hover Sound");
                    useClickSound.boolValue = EditorHandlerCustom.DrawToggle(useClickSound.boolValue, customSkin, "Enable Click Sound");
                    if (useHoverSound.boolValue)
                    {
                        EditorHandlerCustom.DrawProperty(hoverSound, customSkin, "Hover Sound");
                    }

                    if (useClickSound.boolValue)
                    {
                        EditorHandlerCustom.DrawProperty(clickSound, customSkin, "Click Sound");
                    }

                    EditorHandlerCustom.DrawHeader(customSkin, "UIM Header", 10);

                    break;
            }

            if (Application.isPlaying == false) { this.Repaint(); }
            serializedObject.ApplyModifiedProperties();
        }
    }
}
