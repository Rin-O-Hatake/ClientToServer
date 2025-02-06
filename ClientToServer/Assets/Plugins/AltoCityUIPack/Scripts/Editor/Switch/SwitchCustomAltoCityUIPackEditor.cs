#if UNITY_EDITOR
using Plugins.AltoCityUIPack.Scripting;
using Plugins.AltoCityUIPack.Scripts.Switch;
using UnityEditor;
using UnityEngine;

namespace Plugins.AltoCityUIPack.Scripts.Editor.Switch
{
    [CustomEditor(typeof(SwitchCustomAltoCityUIPack))]
    public class SwitchCustomAltoCityUIPackEditor : UnityEditor.Editor
    {
        private GUISkin customSkin;
        private SwitchCustomAltoCityUIPack switchTarget;
        private int currentTab;

        private void OnEnable()
        {
            switchTarget = (SwitchCustomAltoCityUIPack)target;

            customSkin = (GUISkin)Resources.Load("AltoCity-EditorDark");
        }

        public override void OnInspectorGUI()
        {
            EditorHandlerCustom.DrawComponentHeader(customSkin, "Switch Top Header");

            GUIContent[] toolbarTabs = new GUIContent[2];
            toolbarTabs[0] = new GUIContent("Content");
            toolbarTabs[1] = new GUIContent("Settings");
    
            currentTab = EditorHandlerCustom.DrawTabs(currentTab, toolbarTabs, customSkin);

            if (GUILayout.Button(new GUIContent("Content", "Content"), customSkin.FindStyle("Tab Content")))
                currentTab = 0;
            if (GUILayout.Button(new GUIContent("Settings", "Settings"), customSkin.FindStyle("Tab Settings")))
                currentTab = 1;

            GUILayout.EndHorizontal();
            
            var toggle = serializedObject.FindProperty("_toggle");
            var toggleColor = serializedObject.FindProperty("_toggleColor");
            var toggleSprite = serializedObject.FindProperty("_toggleSprite");
            
            var toggleIsOn = serializedObject.FindProperty("_toggleIsOn");
            var toggleIsOff = serializedObject.FindProperty("_toggleIsOff");
            var _duration = serializedObject.FindProperty("_duration");
            var colorBackgroundIsOn = serializedObject.FindProperty("_colorBackgroundIsOn");
            var colorBackgroundIsOff = serializedObject.FindProperty("_colorBackgroundIsOff");
            
            var cgSwitch = serializedObject.FindProperty("_cgSwitch");
            var backgroundImage = serializedObject.FindProperty("_backgroundImage");
            var backgroundSprite = serializedObject.FindProperty("_backgroundSprite");
            var pixelsPerUnitImage = serializedObject.FindProperty("_pixelsPerUnitImage");
            
            var isInteractable = serializedObject.FindProperty("_isInteractable");
            
            var isOn = serializedObject.FindProperty("isOn");
            var onValueChanged = serializedObject.FindProperty("onValueChanged");
            var OnEvents = serializedObject.FindProperty("OnEvents");
            var OffEvents = serializedObject.FindProperty("OffEvents");
            var enableSwitchSounds = serializedObject.FindProperty("enableSwitchSounds");
            var useHoverSound = serializedObject.FindProperty("useHoverSound");
            var useClickSound = serializedObject.FindProperty("useClickSound");
            var hoverSound = serializedObject.FindProperty("hoverSound");
            var clickSound = serializedObject.FindProperty("clickSound");

            switchTarget.UpdateUI();
            switch (currentTab)
            {
                case 0:
                    EditorHandlerCustom.DrawHeader(customSkin, "Content Header", 6);
                    
                    GUILayout.BeginVertical(EditorStyles.helpBox);
                    
                    isInteractable.boolValue = EditorHandlerCustom.DrawTogglePlain(isInteractable.boolValue, customSkin, "IsInteractable");
                    GUILayout.Space(3);
                    isOn.boolValue = EditorHandlerCustom.DrawTogglePlain(isOn.boolValue, customSkin, "isOn");
                    
                    GUILayout.Space(5);
                    
                    EditorGUILayout.PropertyField(toggleColor, new GUIContent("Toggle Color"), true);
                    EditorGUILayout.PropertyField(toggleSprite, new GUIContent("Toggle Sprite"), true);
                    
                    GUILayout.Space(5);
                    
                    EditorHandlerCustom.DrawPropertyCW(backgroundSprite, customSkin, "Background Sprite", 170);
                    EditorHandlerCustom.DrawPropertyCW(pixelsPerUnitImage, customSkin, "Pixels Per Unit Background", 170);
                    
                    GUILayout.Space(5);
                    
                    GUILayout.EndVertical();
                    
                    EditorHandlerCustom.DrawHeader(customSkin, "Animation Header", 10);
                    
                    GUILayout.BeginVertical(EditorStyles.helpBox);
                    
                    EditorHandlerCustom.DrawPropertyCW(_duration, customSkin, "Duration Animation", 150);
                    EditorHandlerCustom.DrawPropertyCW(colorBackgroundIsOn, customSkin, "Color Background IsOn", 150);
                    EditorHandlerCustom.DrawPropertyCW(colorBackgroundIsOff, customSkin, "Color Background IsOff", 150);
                    
                    GUILayout.EndVertical();
                    
                    EditorHandlerCustom.DrawHeader(customSkin, "Events Header", 6);
                    GUILayout.BeginVertical(EditorStyles.helpBox);
                    
                    EditorGUILayout.PropertyField(onValueChanged, new GUIContent("On Value Changed"), true);
                    EditorGUILayout.PropertyField(OnEvents, new GUIContent("On Events"), true);
                    EditorGUILayout.PropertyField(OffEvents, new GUIContent("Off Events"), true);
                    
                    GUILayout.EndVertical();
                    break;

                case 1:
                    EditorHandlerCustom.DrawHeader(customSkin, "Options Header", 6);
                    
                    GUILayout.BeginVertical(EditorStyles.helpBox);
                    
                    EditorHandlerCustom.DrawPropertyCW(toggle, customSkin, "Toggle", 150);
                    EditorHandlerCustom.DrawPropertyCW(cgSwitch, customSkin, "cgSwitch", 150);
                    EditorHandlerCustom.DrawPropertyCW(backgroundImage, customSkin, "Background Image", 150);
                    EditorHandlerCustom.DrawPropertyCW(toggleIsOn, customSkin, "IsOnPositionTransform", 150);
                    EditorHandlerCustom.DrawPropertyCW(toggleIsOff, customSkin, "IsOffPositionTransform", 150);
                    
                    GUILayout.EndVertical();

                    GUILayout.Space(5);
                    
                    GUILayout.BeginVertical(EditorStyles.helpBox);
                    GUILayout.Space(-3);
                    enableSwitchSounds.boolValue = EditorHandlerCustom.DrawTogglePlain(enableSwitchSounds.boolValue, customSkin, "Enable Switch Sounds");
                    GUILayout.Space(3);

                    if (enableSwitchSounds.boolValue)
                    {
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
                        
                    }
                    
                    GUILayout.EndVertical();

                    break;
            }

            if (Application.isPlaying == false) { this.Repaint(); }
            serializedObject.ApplyModifiedProperties();
        }
    }
}

#endif
