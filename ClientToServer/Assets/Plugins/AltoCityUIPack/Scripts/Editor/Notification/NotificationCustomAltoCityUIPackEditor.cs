using Plugins.AltoCityUIPack.Scripts.Notification;
using UnityEditor;
using UnityEngine;

namespace Plugins.AltoCityUIPack.Scripts.Editor.Notification
{
    [CustomEditor(typeof(NotificationCustomAltoCityUIPack))]
    public class NotificationCustomAltoCityUIPackEditor : UnityEditor.Editor
    {
        private GUISkin customSkin;
        private NotificationCustomAltoCityUIPack notificationTarget;
        private int currentTab;
        
        private void OnEnable()
        {
            notificationTarget = (NotificationCustomAltoCityUIPack)target;
            
            customSkin = (GUISkin)Resources.Load("AltoCity-EditorDark");
        }
        
        public override void OnInspectorGUI()
        {
            EditorHandlerCustom.DrawComponentHeader(customSkin, "Notification Top Header");

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
            
            var notificationItems = serializedObject.FindProperty("_notificationItems");
            var currentNotificationType = serializedObject.FindProperty("_currentNotificationType");
            var rootRectTransform = serializedObject.FindProperty("_rootRectTransform");
            var currentCanvasGroup = serializedObject.FindProperty("_currentCanvasGroup");
            
            var iconImage = serializedObject.FindProperty("_iconImage");
            
            var title = serializedObject.FindProperty("_title");
            var titleText = serializedObject.FindProperty("_titleText");
            var fontTitle = serializedObject.FindProperty("_fontTitle");
           
            var description = serializedObject.FindProperty("_description");
            var descriptionText = serializedObject.FindProperty("_descriptionText");
            var fontDescription = serializedObject.FindProperty("_fontDescription");
           
            var backgroundImage = serializedObject.FindProperty("_backgroundImage");
            var backgroundSprite = serializedObject.FindProperty("_backgroundSprite");
            var pixelsPerUnitMultiplierBackground = serializedObject.FindProperty("_pixelsPerUnitMultiplierBackground");

            var backgroundTitleImage = serializedObject.FindProperty("_backgroundTitleImage");
            var backgroundTitleSprite = serializedObject.FindProperty("_backgroundTitleSprite");
            var pixelsPerUnitMultiplierBackgroundTitle = serializedObject.FindProperty("_pixelsPerUnitMultiplierBackgroundTitle");

            var enableTimer = serializedObject.FindProperty("_enableTimer");
            var timer = serializedObject.FindProperty("_timer");
            var startBehaviour = serializedObject.FindProperty("_startBehaviour");
            var closeBehaviour = serializedObject.FindProperty("_closeBehaviour");
            var isCloseBehaviourClick = serializedObject.FindProperty("_isCloseBehaviourClick");
            
            var enableButtonSounds = serializedObject.FindProperty("_enableButtonSounds");
            var useFocusedSound = serializedObject.FindProperty("_useFocusedSound");
            var useClickSound = serializedObject.FindProperty("_useClickSound");
            var focusedSound = serializedObject.FindProperty("_focusedSound");
            var clickSound = serializedObject.FindProperty("_clickSound");
            
            var enableFocusedAnimation = serializedObject.FindProperty("_enableFocusedAnimation");
            var sizeFocused = serializedObject.FindProperty("_sizeFocused");
            var durationAnimationOnPointerEnter = serializedObject.FindProperty("_durationAnimationOnPointerEnter");
            var enableChangeFocusedColor = serializedObject.FindProperty("_enableChangeFocusedColor");
            
            var animationOpenNotification = serializedObject.FindProperty("_animationOpenNotification");
            var startOpenPositionAnimation = serializedObject.FindProperty("_startOpenPositionAnimation");
            var durationAnimationOpen = serializedObject.FindProperty("_durationAnimationOpen");
            
            var onOpen = serializedObject.FindProperty("_onOpen");
            var onClose = serializedObject.FindProperty("_onClose");
            var onClick = serializedObject.FindProperty("_onClick");

            notificationTarget.UpdateUI();
            switch (currentTab)
            {
                case 0:
                    EditorHandlerCustom.DrawHeader(customSkin, "Content Header", 6);
                    
                    GUILayout.Space(8);

                    GUILayout.Space(8);
                    
                    GUILayout.BeginVertical(EditorStyles.helpBox);

                    EditorHandlerCustom.DrawPropertyCW(title, customSkin, "title", 190);
                    EditorHandlerCustom.DrawPropertyCW(fontTitle, customSkin, "Font Title", 190);

                    GUILayout.EndVertical();
                    
                    GUILayout.Space(8);
                    
                    GUILayout.BeginVertical(EditorStyles.helpBox);

                    EditorHandlerCustom.DrawPropertyCW(description, customSkin, "Description", 190);
                    EditorHandlerCustom.DrawPropertyCW(fontDescription, customSkin, "Font Description", 190);

                    GUILayout.EndVertical();
                    
                    GUILayout.Space(8);
                    
                    GUILayout.BeginVertical(EditorStyles.helpBox);

                    EditorHandlerCustom.DrawPropertyCW(backgroundSprite, customSkin, "Background Sprite", 190);
                    EditorHandlerCustom.DrawPropertyCW(pixelsPerUnitMultiplierBackground, customSkin, "Pixels PerUnit Background", 190);

                    GUILayout.EndVertical();
                    
                    GUILayout.Space(8);
                    
                    GUILayout.BeginVertical(EditorStyles.helpBox);

                    EditorHandlerCustom.DrawPropertyCW(backgroundTitleSprite, customSkin, "Background Title Sprite", 190);
                    EditorHandlerCustom.DrawPropertyCW(pixelsPerUnitMultiplierBackgroundTitle, customSkin, "Pixels PerUnit Background Title", 190);

                    GUILayout.EndVertical();
                    
                    GUILayout.Space(8);
                    
                    GUILayout.BeginVertical(EditorStyles.helpBox);
                    
                    enableTimer.boolValue = EditorHandlerCustom.DrawTogglePlain(enableTimer.boolValue, customSkin, "Enable Timer");
                    
                    GUILayout.Space(5);

                    if (enableTimer.boolValue)
                    {
                        EditorHandlerCustom.DrawPropertyCW(timer, customSkin, "Timer", 190);    
                    }
                    
                    EditorHandlerCustom.DrawPropertyCW(startBehaviour, customSkin, "Start Behaviour", 190);
                    EditorHandlerCustom.DrawPropertyCW(closeBehaviour, customSkin, "Close Behaviour", 190);

                    isCloseBehaviourClick.boolValue = EditorHandlerCustom.DrawTogglePlain(isCloseBehaviourClick.boolValue, customSkin, "Is Close Behaviour Click");
                    
                    GUILayout.Space(5);

                    GUILayout.EndVertical();

                    GUILayout.Space(8);

                    GUILayout.BeginVertical();
                    
                    EditorHandlerCustom.DrawPropertyCW(currentNotificationType, customSkin, "Current Notification Type", 190);
                    
                    GUILayout.Space(8);

                    EditorGUI.indentLevel = 1;
                    EditorGUILayout.PropertyField(notificationItems, new GUIContent("Notification Items"), true);
                    EditorGUI.indentLevel = 0;
                    
                    GUILayout.EndVertical();
                    
                    
                    GUILayout.Space(8);

                    EditorHandlerCustom.DrawHeader(customSkin, "Events Header", 10);
                    
                    EditorGUILayout.PropertyField(onClick, new GUIContent("On Click"), true);
                    EditorGUILayout.PropertyField(onOpen, new GUIContent("On Open"), true);
                    EditorGUILayout.PropertyField(onClose, new GUIContent("On Close"), true);
                    break;

                case 1:
                    EditorHandlerCustom.DrawHeader(customSkin, "Core Header", 6);
                     
                    EditorHandlerCustom.DrawPropertyCW(iconImage, customSkin, "Icon Image", 150);
                    EditorHandlerCustom.DrawPropertyCW(titleText, customSkin, "Title Text", 150);
                    EditorHandlerCustom.DrawPropertyCW(descriptionText, customSkin, "Description Text", 150);
                    EditorHandlerCustom.DrawPropertyCW(backgroundImage, customSkin, "Background Image", 150);
                    EditorHandlerCustom.DrawPropertyCW(backgroundTitleImage, customSkin, "Background Title Image", 150);
                    EditorHandlerCustom.DrawPropertyCW(rootRectTransform, customSkin, "Root Rect Transform", 150);
                    EditorHandlerCustom.DrawPropertyCW(currentCanvasGroup, customSkin, "Current Canvas Group", 150);

                    break;

                case 2:
                    EditorHandlerCustom.DrawHeader(customSkin, "Options Header", 6);

                    GUILayout.BeginVertical(EditorStyles.helpBox);
                    
                    enableButtonSounds.boolValue = EditorHandlerCustom.DrawTogglePlain(enableButtonSounds.boolValue, customSkin, "Enable Button Sounds");

                    GUILayout.Space(4);
                    
                    if (enableButtonSounds.boolValue)
                    {
                        useFocusedSound.boolValue = EditorHandlerCustom.DrawTogglePlain(useFocusedSound.boolValue, customSkin, "Use Focused Sound");

                        GUILayout.Space(4);
                        
                        if (useFocusedSound.boolValue)
                        {
                            EditorHandlerCustom.DrawPropertyCW(focusedSound, customSkin, "Focused Sound", 210);
                        }
                        
                        useClickSound.boolValue = EditorHandlerCustom.DrawTogglePlain(useClickSound.boolValue, customSkin, "Use Click Sound");
                        
                        GUILayout.Space(4);
                        
                        if (useClickSound.boolValue)
                        {
                            EditorHandlerCustom.DrawPropertyCW(clickSound, customSkin, "Click Sound", 210);
                        }
                    }
                    
                    GUILayout.Space(4);

                    GUILayout.EndVertical();
                    
                    GUILayout.Space(8);
                    
                    EditorHandlerCustom.DrawHeader(customSkin, "Animation Header", 10);
                    
                    enableFocusedAnimation.boolValue = EditorHandlerCustom.DrawTogglePlain(enableFocusedAnimation.boolValue, customSkin, "Enable Focused Animation");

                    GUILayout.Space(4);
                    
                    if (enableFocusedAnimation.boolValue)
                    {
                        EditorHandlerCustom.DrawPropertyCW(sizeFocused, customSkin, "Size Focused", 210);
                        EditorHandlerCustom.DrawPropertyCW(durationAnimationOnPointerEnter, customSkin, "Duration Animation OnPointer Enter", 210);
                    }

                    EditorHandlerCustom.DrawPropertyCW(animationOpenNotification, customSkin, "Animation Open Notification", 210);
                    EditorHandlerCustom.DrawPropertyCW(startOpenPositionAnimation, customSkin, "Start Open Position Animation", 210);
                    EditorHandlerCustom.DrawPropertyCW(durationAnimationOpen, customSkin, "Duration Animation Open", 210);

                    break;
            }

            if (!Application.isPlaying) { this.Repaint(); }
            serializedObject.ApplyModifiedProperties();
        }
    }
}
