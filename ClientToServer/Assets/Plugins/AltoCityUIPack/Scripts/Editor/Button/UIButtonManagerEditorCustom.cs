#if UNITY_EDITOR
using Plugins.AltoCityUIPack.Scripts.Button;
using UnityEditor;
using UnityEngine;

namespace Plugins.AltoCityUIPack.Scripts.Editor.Button
{
    [CanEditMultipleObjects]
    [CustomEditor(typeof(UIButtonManagerCustom))]
    public class UIButtonManagerEditorCustom : UnityEditor.Editor
    {
        private GUISkin customSkin;
        private UIButtonManagerCustom buttonTarget;

        private void OnEnable()
        {
            buttonTarget = (UIButtonManagerCustom)target;
            
            customSkin = (GUISkin)Resources.Load("AltoCity-EditorDark");
        }

        public override void OnInspectorGUI()
        {
            EditorHandlerCustom.DrawComponentHeader(customSkin, "Button Top Header");

            GUIContent[] toolbarTabs = new GUIContent[3];
            toolbarTabs[0] = new GUIContent("Content");
            toolbarTabs[1] = new GUIContent("Resources");
            toolbarTabs[2] = new GUIContent("Settings");
            
            buttonTarget.latestTabIndex = EditorHandlerCustom.DrawTabs(buttonTarget.latestTabIndex, toolbarTabs, customSkin);

            if (GUILayout.Button(new GUIContent("Content", "Content"), customSkin.FindStyle("Tab Content")))
                buttonTarget.latestTabIndex = 0;
            if (GUILayout.Button(new GUIContent("Resources", "Resources"), customSkin.FindStyle("Tab Resources")))
                buttonTarget.latestTabIndex = 1;
            if (GUILayout.Button(new GUIContent("Settings", "Settings"), customSkin.FindStyle("Tab Settings"))) 
                buttonTarget.latestTabIndex = 2;
            
            GUILayout.EndHorizontal();
            
            var normalCG = serializedObject.FindProperty("normalCG");
            var currentText = serializedObject.FindProperty("_currentText");
            var currentTextAlignment = serializedObject.FindProperty("_currentTextAlignment");
            var Font = serializedObject.FindProperty("_Font");
            var currentTextColor = serializedObject.FindProperty("_currentTextColor");
            
            var currentImage = serializedObject.FindProperty("_currentImage");
            var imageColor = serializedObject.FindProperty("_imageColor");
            var horizontalLayoutImage = serializedObject.FindProperty("_horizontalLayoutImage");
            var infoLayout = serializedObject.FindProperty("_infoLayout");
            var paddingImage = serializedObject.FindProperty("_paddingImage");
            var paddingInfo = serializedObject.FindProperty("_paddingInfo");
            
            var currentBackground = serializedObject.FindProperty("_currentBackground");
            var backgroundColor = serializedObject.FindProperty("_backgroundColorNormal");
            var backgroundColorDisabled = serializedObject.FindProperty("_backgroundColorDisabled");
            var backgroundColorFocused = serializedObject.FindProperty("_backgroundColorFocused");
            var backgroundSprite = serializedObject.FindProperty("_backgroundSprite");
            var customBackground = serializedObject.FindProperty("_customBackground");
            var pixelsPerUnitMultiplier = serializedObject.FindProperty("_pixelsPerUnitMultiplier");
            var enableFocusedAnimation = serializedObject.FindProperty("_enableFocusedChangeColor");
            
            var enableFrame = serializedObject.FindProperty("_enableFrame");
            var currentFrameImage = serializedObject.FindProperty("_currentFrameImage");
            var frameSprite = serializedObject.FindProperty("_frameSprite");
            var frameColor = serializedObject.FindProperty("_frameColor");
            var framePixelsPerUnitMultiplier = serializedObject.FindProperty("_framePixelsPerUnitMultiplier");
            
            var ripple = serializedObject.FindProperty("ripple");

            var buttonIcon = serializedObject.FindProperty("buttonIcon");
            var buttonText = serializedObject.FindProperty("buttonText");
            var iconScale = serializedObject.FindProperty("_iconScale");
            var textSize = serializedObject.FindProperty("textSize");
            var hoverSound = serializedObject.FindProperty("hoverSound");
            var clickSound = serializedObject.FindProperty("clickSound");

            var autoFitContent = serializedObject.FindProperty("autoFitContent");
            var padding = serializedObject.FindProperty("padding");
            var spacing = serializedObject.FindProperty("spacing");
            var mainLayout = serializedObject.FindProperty("mainLayout");
            var mainFitter = serializedObject.FindProperty("mainFitter");
            var infoFitter = serializedObject.FindProperty("_infoFitter");

            var isInteractable = serializedObject.FindProperty("isIntractable");
            var interactableAlpha = serializedObject.FindProperty("_interactableAlpha");
            var enableIcon = serializedObject.FindProperty("enableIcon");
            var enableText = serializedObject.FindProperty("enableText");
            var enableButtonSounds = serializedObject.FindProperty("enableButtonSounds");
            var useHoverSound = serializedObject.FindProperty("useHoverSound");
            var useClickSound = serializedObject.FindProperty("useClickSound");
            var useRipple = serializedObject.FindProperty("useRipple");

            var centered = serializedObject.FindProperty("centered");
            var duration = serializedObject.FindProperty("duration");
            var maxSize = serializedObject.FindProperty("maxSize");
            var startColor = serializedObject.FindProperty("startColor");

            var onClick = serializedObject.FindProperty("onClick");
            var onHover = serializedObject.FindProperty("onHover");
            var onLeave = serializedObject.FindProperty("onLeave");
            var onPressButton = serializedObject.FindProperty("onPressButton");
            var _isOnPressButton = serializedObject.FindProperty("_isOnPressButton");
            var _stepNextCallBack = serializedObject.FindProperty("_stepNextCallBack");
            
            var _enableAnimation = serializedObject.FindProperty("_enableAnimation");
            var _enableAnimationOnPointerEnter = serializedObject.FindProperty("_enableAnimationOnPointerEnter");
            var _enableAnimationOnPointerClick = serializedObject.FindProperty("_enableAnimationOnPointerClick");
            var _durationAnimationOnPointerClick = serializedObject.FindProperty("_durationAnimationOnPointerClick");
            var _durationAnimationOnPointerEnter = serializedObject.FindProperty("_durationAnimationOnPointerEnter");
            var _sizeAnimationOnPointerClick = serializedObject.FindProperty("_sizeAnimationOnPointerClick");
            var _sizeAnimationOnPointerEnter = serializedObject.FindProperty("_sizeAnimationOnPointerEnter");
            
            buttonTarget.UpdateUI();
            switch (buttonTarget.latestTabIndex)
            {
                case 0:
                    EditorHandlerCustom.DrawHeader(customSkin, "Content Header", 6);

                    #region Icon And Text

                        GUILayout.BeginVertical(EditorStyles.helpBox);
                        GUILayout.Space(-3);

                        enableIcon.boolValue = EditorHandlerCustom.DrawTogglePlain(enableIcon.boolValue, customSkin, "Enable Icon");
                        GUILayout.Space(4);

                        if (enableIcon.boolValue)
                        {
                            EditorHandlerCustom.DrawPropertyCW(buttonIcon, customSkin, "Button Icon", 80);
                            EditorHandlerCustom.DrawPropertyCW(iconScale, customSkin, "Icon Scale", 80);
                            EditorHandlerCustom.DrawPropertyCW(imageColor, customSkin, "Color Icon", 80);
                            EditorHandlerCustom.DrawPropertyCW(paddingImage, customSkin, "Padding Icon", 80);
                        }

                        GUILayout.EndVertical();
                    
                        GUILayout.Space(6);

                        GUILayout.BeginVertical(EditorStyles.helpBox);
                        GUILayout.Space(-3);

                        enableText.boolValue = EditorHandlerCustom.DrawTogglePlain(enableText.boolValue, customSkin, "Enable Text");

                        GUILayout.Space(4);

                        if (enableText.boolValue)
                        {
                            EditorHandlerCustom.DrawPropertyCW(buttonText, customSkin, "Button Text", 80);
                            EditorHandlerCustom.DrawPropertyCW(textSize, customSkin, "Text Size", 80);
                            EditorHandlerCustom.DrawPropertyCW(Font, customSkin, "Font Text", 80);
                            EditorHandlerCustom.DrawPropertyCW(currentTextColor, customSkin, "Color Text", 80);
                            EditorHandlerCustom.DrawPropertyCW(currentTextAlignment, customSkin, "Alignment Text", 80);
                        }
                        GUILayout.EndVertical();
                    
                        GUILayout.Space(6);

                    #endregion

                    #region Backgrounds

                    GUILayout.BeginVertical(EditorStyles.helpBox);
                    
                    enableFocusedAnimation.boolValue = EditorHandlerCustom.DrawTogglePlain(enableFocusedAnimation.boolValue, customSkin, "Enable Focused Color");
                    
                    GUILayout.Space(5);
                    
                    EditorHandlerCustom.DrawPropertyCW(backgroundColor, customSkin, "Background Color", 180);
                    EditorHandlerCustom.DrawPropertyCW(backgroundColorDisabled, customSkin, "Background Color Disabled", 180);
                    
                    if (enableFocusedAnimation.boolValue)
                    {
                        EditorHandlerCustom.DrawPropertyCW(backgroundColorFocused, customSkin, "Background Color Focused", 180);   
                    }
                    
                    customBackground.boolValue = EditorHandlerCustom.DrawTogglePlain(customBackground.boolValue, customSkin, "Custom Background");
                    
                    GUILayout.Space(4);

                    if (customBackground.boolValue)
                    {
                        EditorHandlerCustom.DrawPropertyCW(backgroundSprite, customSkin, "Background Sprite", 140);
                        EditorHandlerCustom.DrawProperty(pixelsPerUnitMultiplier, customSkin, "Pixels Per U M"); 
                    }
                    
                    GUILayout.EndVertical();
                    
                    #endregion      
                    
                    GUILayout.Space(4);

                    #region Auto-Fit Content

                    GUILayout.BeginVertical(EditorStyles.helpBox);
                    GUILayout.Space(-3);

                    autoFitContent.boolValue = EditorHandlerCustom.DrawTogglePlain(autoFitContent.boolValue, customSkin, "Auto-Fit Content");

                    GUILayout.Space(4);
                    
                    
                    GUILayout.BeginHorizontal(EditorStyles.helpBox);
                    EditorGUI.indentLevel = 1;
                    EditorGUILayout.PropertyField(padding, new GUIContent(" Padding"), true);
                    EditorGUI.indentLevel = 0;
                    GUILayout.EndHorizontal();
                    
                    GUILayout.Space(4);
                    GUILayout.BeginHorizontal(EditorStyles.helpBox);
                    
                    EditorGUI.indentLevel = 1;
                    EditorGUILayout.PropertyField(paddingInfo, new GUIContent("Padding Info"), true);
                    EditorGUI.indentLevel = 0;
                    GUILayout.EndHorizontal();
                    
                    EditorHandlerCustom.DrawProperty(spacing, customSkin, "Spacing");
                    

                    GUILayout.EndVertical();
                    
                    #endregion
                    
                    #region Frame
                    
                    GUILayout.BeginVertical(EditorStyles.helpBox);
                    
                    enableFrame.boolValue = EditorHandlerCustom.DrawTogglePlain(enableFrame.boolValue, customSkin, "Enable Icon");
                    
                    GUILayout.Space(4);

                    if (enableFrame.boolValue)
                    {
                        EditorHandlerCustom.DrawPropertyCW(frameSprite, customSkin, "Frame Sprite", 140);
                        EditorHandlerCustom.DrawPropertyCW(frameColor, customSkin, "Frame Color", 140);
                        EditorHandlerCustom.DrawPropertyCW(framePixelsPerUnitMultiplier, customSkin, "Frame Pixels PUM", 140);
                    }
                    
                    GUILayout.Space(6);
                    
                    GUILayout.EndVertical();
                    
                    #endregion

                    EditorHandlerCustom.DrawHeader(customSkin, "Events Header", 10);
                    EditorGUILayout.PropertyField(onClick, new GUIContent("On Click"), true);
                    EditorGUILayout.PropertyField(onHover, new GUIContent("On Hover"), true);
                    EditorGUILayout.PropertyField(onLeave, new GUIContent("On Leave"), true);

                    _isOnPressButton.boolValue = EditorHandlerCustom.DrawTogglePlain(_isOnPressButton.boolValue, customSkin, "Enable Press Button");
                    
                    GUILayout.Space(4);
                    
                    if (_isOnPressButton.boolValue)
                    {
                        EditorGUILayout.PropertyField(onPressButton, new GUIContent("On Press"), true);
                        EditorHandlerCustom.DrawPropertyCW(_stepNextCallBack, customSkin, "Step Next CallBack", 130);
                    }

                    break;

                case 1:
                    EditorHandlerCustom.DrawHeader(customSkin, "Core Header", 6);
                     
                    EditorHandlerCustom.DrawProperty(normalCG, customSkin, "Normal Canvas Group");
                    
                    EditorHandlerCustom.DrawProperty(currentBackground, customSkin, "Background Image");
                    EditorHandlerCustom.DrawProperty(currentFrameImage, customSkin, "Frame Image");
                    EditorHandlerCustom.DrawProperty(currentText, customSkin, "Normal Text");
                    EditorHandlerCustom.DrawProperty(currentImage, customSkin, "Normal Icon");
                    EditorHandlerCustom.DrawProperty(horizontalLayoutImage, customSkin, "Image Fitter");
                    EditorHandlerCustom.DrawProperty(mainLayout, customSkin, "Main Layout");
                    EditorHandlerCustom.DrawProperty(mainFitter, customSkin, "Main Fitter");
                    EditorHandlerCustom.DrawProperty(infoFitter, customSkin, "Info Fitter");
                    EditorHandlerCustom.DrawProperty(infoLayout, customSkin, "Info Layout");


                    if (useRipple.boolValue) { EditorHandlerCustom.DrawProperty(ripple, customSkin, "Ripple"); }
                    break;

                case 2:
                    EditorHandlerCustom.DrawHeader(customSkin, "Options Header", 6);
                    isInteractable.boolValue = EditorHandlerCustom.DrawToggle(isInteractable.boolValue, customSkin, "Is Interactable");

                    if (!isInteractable.boolValue)
                    {
                        EditorHandlerCustom.DrawPropertyCW(interactableAlpha, customSkin, "Interactable Alpha", 180);
                    }
                    
                    GUI.enabled = true;
                    
                    GUILayout.BeginVertical(EditorStyles.helpBox);
                    GUILayout.Space(-3);
                
                    enableButtonSounds.boolValue = EditorHandlerCustom.DrawTogglePlain(enableButtonSounds.boolValue, customSkin, "Enable Button Sounds");

                    GUILayout.Space(4);

                    if (enableButtonSounds.boolValue)
                    {
                        if (useHoverSound.boolValue)
                        {
                            EditorHandlerCustom.DrawProperty(hoverSound, customSkin, "Hover Sound");
                        }

                        if (useClickSound.boolValue)
                        {
                            EditorHandlerCustom.DrawProperty(clickSound, customSkin, "Click Sound");
                        }

                        useHoverSound.boolValue = EditorHandlerCustom.DrawToggle(useHoverSound.boolValue, customSkin, "Enable Hover Sound");
                        useClickSound.boolValue = EditorHandlerCustom.DrawToggle(useClickSound.boolValue, customSkin, "Enable Click Sound");

                    }

                    GUILayout.EndVertical();

                    EditorHandlerCustom.DrawHeader(customSkin, "Customization Header", 10);
                    GUILayout.BeginVertical(EditorStyles.helpBox);
                    GUILayout.Space(-2);

                    useRipple.boolValue = EditorHandlerCustom.DrawTogglePlain(useRipple.boolValue, customSkin, "Use Ripple");

                    GUILayout.Space(4);

                    if (useRipple.boolValue)
                    {
                        // renderOnTop.boolValue = EditorHandlerCustom.DrawToggle(renderOnTop.boolValue, customSkin, "Render On Top");
                        centered.boolValue = EditorHandlerCustom.DrawToggle(centered.boolValue, customSkin, "Centered");
                        EditorHandlerCustom.DrawProperty(duration, customSkin, "Duration");
                        EditorHandlerCustom.DrawProperty(maxSize, customSkin, "Max Size");
                        EditorHandlerCustom.DrawProperty(startColor, customSkin, "Start Color");
                    }
                    
                    GUILayout.EndVertical();
                    EditorHandlerCustom.DrawHeader(customSkin, "Animation Header", 10);
                    
                    GUILayout.BeginVertical(EditorStyles.helpBox);
                    
                    _enableAnimation.boolValue = EditorHandlerCustom.DrawTogglePlain(_enableAnimation.boolValue, customSkin, "Use Animation Button");

                    if (_enableAnimation.boolValue)
                    {
                        GUILayout.Space(10);
                        
                        GUILayout.BeginVertical(EditorStyles.helpBox);
                        _enableAnimationOnPointerClick.boolValue = EditorHandlerCustom.DrawTogglePlain(_enableAnimationOnPointerClick.boolValue, customSkin, "Use Animation Pointer Click");
                        
                        GUILayout.Space(5);
                        
                        if (_enableAnimationOnPointerClick.boolValue)
                        {
                            EditorHandlerCustom.DrawProperty(_durationAnimationOnPointerClick, customSkin, "Duration");
                            EditorHandlerCustom.DrawProperty(_sizeAnimationOnPointerClick, customSkin, "Size");
                        }

                        GUILayout.EndVertical();
                        
                        GUILayout.BeginVertical(EditorStyles.helpBox);
                        
                        _enableAnimationOnPointerEnter.boolValue = EditorHandlerCustom.DrawTogglePlain(_enableAnimationOnPointerEnter.boolValue, customSkin, "Use Animation Pointer Enter");
                        
                        GUILayout.Space(5);
                        
                        if (_enableAnimationOnPointerEnter.boolValue)
                        {
                            EditorHandlerCustom.DrawProperty(_durationAnimationOnPointerEnter, customSkin, "Duration");
                            EditorHandlerCustom.DrawProperty(_sizeAnimationOnPointerEnter, customSkin, "Size");
                        }
                        
                        GUILayout.EndVertical();
                    }
                    
                    GUILayout.Space(10);
                    GUILayout.EndVertical();
                    break;
            }

            if (Application.isPlaying == false) { this.Repaint(); }
            serializedObject.ApplyModifiedProperties();
        }
    }
}
#endif
