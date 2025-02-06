using Plugins.AltoCityUIPack.Scripts.Dropdown;
using UnityEditor;
using UnityEngine;

namespace Plugins.AltoCityUIPack.Scripts.Editor.Dropdown
{
    [CustomEditor(typeof(DropdownUICustom))]
    public class DropdownUICustomEditor : UnityEditor.Editor
    {
        private GUISkin customSkin;
        private DropdownUICustom dropdownTarget;
        private int currentTab;

        private void OnEnable()
        {
            dropdownTarget = (DropdownUICustom)target;
            
            customSkin = (GUISkin)Resources.Load("AltoCity-EditorDark");
        }

        public override void OnInspectorGUI()
        {
            EditorHandlerCustom.DrawComponentHeader(customSkin, "Dropdown Top Header");

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
            
        
            var backgroundMainColor = serializedObject.FindProperty("_backgroundMainColor");
            var backgroundImage = serializedObject.FindProperty("_backgroundImage");
            var pixelsPerUnitMultiplier = serializedObject.FindProperty("_pixelsPerUnitMultiplier");
            var backgroundSprite = serializedObject.FindProperty("_backgroundSprite");
            var customBackground = serializedObject.FindProperty("_customBackground");
        
            var sizeIcon = serializedObject.FindProperty("_sizeIcon");
            var sizeText = serializedObject.FindProperty("_sizeText");
            var alignmentOptions = serializedObject.FindProperty("_alignmentOptions");
            var textColor = serializedObject.FindProperty("_textColor");
            var fontText = serializedObject.FindProperty("_fontText");
            
            var selectedItemIndex = serializedObject.FindProperty("selectedItemIndex");
            var items = serializedObject.FindProperty("items");
            var backgroundItemsImage = serializedObject.FindProperty("_backgroundItemsImage");
            var backgroundItemsSprite = serializedObject.FindProperty("_backgroundItemsSprite");
            var pixelsBackgroundItems = serializedObject.FindProperty("_pixelsBackgroundItems");
            var colorBackgroundItems = serializedObject.FindProperty("_colorBackgroundItems");
            var colorItemsSelected = serializedObject.FindProperty("_colorItemsSelected");
            var colorItemsDefault = serializedObject.FindProperty("_colorItemsDefault");
            var changeBackgroundItemsColor = serializedObject.FindProperty("_changeBackgroundItemsColor");
            var fontTextItems = serializedObject.FindProperty("_fontTextItems");
            var backgroundItemSprite = serializedObject.FindProperty("_backgroundItemSprite");
            var _pixelsBackgroundItem = serializedObject.FindProperty("_pixelsBackgroundItem");
            
            var textItemsSelectedColor = serializedObject.FindProperty("_textItemsSelectedColor");
            var textItemsDefaultColor = serializedObject.FindProperty("_textItemsDefaultColor");
            var changeTextItemsColor = serializedObject.FindProperty("_changeTextItemsColor");
        
            var selectedText = serializedObject.FindProperty("selectedText");
            var selectedImage = serializedObject.FindProperty("selectedImage");
            var itemParent = serializedObject.FindProperty("itemParent");
            var itemObject = serializedObject.FindProperty("itemObject");
            var listCG = serializedObject.FindProperty("listCG");

            var enableIcon = serializedObject.FindProperty("enableIcon");
            var enableDropdownSounds = serializedObject.FindProperty("enableDropdownSounds");
            var hoverSound = serializedObject.FindProperty("hoverSound");
            var clickSound = serializedObject.FindProperty("clickSound");
            var itemSpacing = serializedObject.FindProperty("itemSpacing");
            var itemPaddingLeft = serializedObject.FindProperty("itemPaddingLeft");
            var itemPaddingRight = serializedObject.FindProperty("itemPaddingRight");
            var itemPaddingTop = serializedObject.FindProperty("itemPaddingTop");
            var itemPaddingBottom = serializedObject.FindProperty("itemPaddingBottom");
            
            var eventTrigger = serializedObject.FindProperty("_eventTrigger");
            var disabledDropdowns = serializedObject.FindProperty("_disabledDropdowns");

            var durationOpenAnimateDropdown = serializedObject.FindProperty("_durationOpenAnimateDropdown");
            var durationCloseAnimateDropdown = serializedObject.FindProperty("_durationCloseAnimateDropdown");
            var _animationOpenDropdown = serializedObject.FindProperty("_animationOpenDropdown");
            var _animationCloseDropdown = serializedObject.FindProperty("_animationCloseDropdown");
            var _enableChangeImageOpenDropdown = serializedObject.FindProperty("_enableChangeImageOpenDropdown");
            
            var OnValueChanged = serializedObject.FindProperty("onValueChanged");

            GUILayout.EndHorizontal();

            dropdownTarget.UpdateUI();
            switch (currentTab)
            {
                case 0:
                    EditorHandlerCustom.DrawHeader(customSkin, "Content Header", 6);

                    if (dropdownTarget.Items is { Count: > 0 })
                    {
                        GUILayout.BeginVertical(EditorStyles.helpBox);
                        
                        GUILayout.BeginHorizontal();

                        GUI.enabled = false;
                        EditorGUILayout.LabelField(new GUIContent("Selected Item:"), customSkin.FindStyle("Text"),
                            GUILayout.Width(100));
                        GUI.enabled = true;

                        EditorGUILayout.LabelField(new GUIContent(dropdownTarget.Items[selectedItemIndex.intValue].itemName),
                            customSkin.FindStyle("Text")); 

                        GUILayout.EndHorizontal();
                        
                        GUILayout.Space(2);

                        selectedItemIndex.intValue =
                            EditorGUILayout.IntSlider(selectedItemIndex.intValue, 0, dropdownTarget.Items.Count - 1);

                        GUILayout.EndVertical();
                    }
                    
                    GUILayout.BeginVertical();
                    
                    EditorGUI.indentLevel = 1;
                    EditorGUILayout.PropertyField(items, new GUIContent("Dropdown Items"), true);
                    EditorGUI.indentLevel = 0;
                    
                    GUILayout.EndVertical();
                    
                    GUILayout.Space(10);
                    
                    GUILayout.BeginVertical();
                    
                    EditorHandlerCustom.DrawPropertyCW(sizeIcon, customSkin, "Size Icon", 130);
                    
                    EditorHandlerCustom.DrawPropertyCW(sizeText, customSkin, "Size Text", 130);
                    EditorHandlerCustom.DrawPropertyCW(alignmentOptions, customSkin, "Alignment", 130);
                    EditorHandlerCustom.DrawPropertyCW(textColor, customSkin, "Text Color", 130);
                    EditorHandlerCustom.DrawPropertyCW(fontText, customSkin, "Font Main Text", 130);
                    EditorHandlerCustom.DrawPropertyCW(fontTextItems, customSkin, "Font Items Text", 130);
                    
                    GUILayout.EndVertical();
                    
                    GUILayout.Space(10);
                    
                    GUILayout.BeginVertical();
                    
                    EditorHandlerCustom.DrawPropertyCW(backgroundMainColor, customSkin, "Background Color", 130);
                    
                    customBackground.boolValue = EditorHandlerCustom.DrawTogglePlain(customBackground.boolValue, customSkin, "Custom Background");
                    
                    GUILayout.Space(5);
                    if (customBackground.boolValue)
                    {
                        EditorHandlerCustom.DrawPropertyCW(backgroundSprite, customSkin, "Background Sprite", 130); 
                        EditorHandlerCustom.DrawPropertyCW(pixelsPerUnitMultiplier, customSkin, "Pixels Background", 130);
                    }
                    
                    GUILayout.EndVertical();
                    
                    GUILayout.Space(5);
                    
                    GUILayout.BeginVertical();
                    
                    EditorHandlerCustom.DrawPropertyCW(backgroundItemsSprite, customSkin, "Background Items Sprite", 200); 
                    EditorHandlerCustom.DrawPropertyCW(pixelsBackgroundItems, customSkin, "Pixels Background Items", 200);
                    EditorHandlerCustom.DrawPropertyCW(colorBackgroundItems, customSkin, "Color Background Items", 200);
                    
                    GUILayout.Space(10);
                    
                    GUILayout.BeginVertical();
                    
                    changeBackgroundItemsColor.boolValue = EditorHandlerCustom.DrawTogglePlain(changeBackgroundItemsColor.boolValue, customSkin, "Custom Background Items Color");
                    
                    GUILayout.Space(10);

                    if (changeBackgroundItemsColor.boolValue)
                    {
                        EditorHandlerCustom.DrawPropertyCW(colorItemsSelected, customSkin, "Color Items Selected", 200);
                        EditorHandlerCustom.DrawPropertyCW(colorItemsDefault, customSkin, "Color Items Default", 200);   
                    }
                    
                    GUILayout.Space(10);
                    
                    changeTextItemsColor.boolValue = EditorHandlerCustom.DrawTogglePlain(changeTextItemsColor.boolValue, customSkin, "Custom Text Items Color");
                    
                    GUILayout.Space(10);

                    if (changeTextItemsColor.boolValue)
                    {
                        EditorHandlerCustom.DrawPropertyCW(textItemsSelectedColor, customSkin, "Color Text Items Selected", 200);
                        EditorHandlerCustom.DrawPropertyCW(textItemsDefaultColor, customSkin, "Color Text Items Default", 200);   
                    }
                    
                    GUILayout.EndVertical();
                    
                    GUILayout.Space(10);
                    
                    EditorHandlerCustom.DrawPropertyCW(backgroundItemSprite, customSkin, "Background Item Sprite", 200);
                    EditorHandlerCustom.DrawPropertyCW(_pixelsBackgroundItem, customSkin, "Pixels Background Item", 200);
                    
                    GUILayout.EndVertical();
                    
                    EditorHandlerCustom.DrawHeader(customSkin, "Events Header", 10);
                    EditorGUILayout.PropertyField(OnValueChanged, new GUIContent("On Value Changed"), true);

                    break;
                
                case 1:
                    EditorHandlerCustom.DrawHeader(customSkin, "Core Header", 6);
                    EditorHandlerCustom.DrawProperty(selectedText, customSkin, "Selected Text");
                    EditorHandlerCustom.DrawProperty(selectedImage, customSkin, "Selected Image");
                    EditorHandlerCustom.DrawProperty(backgroundImage, customSkin, "Background Image");
                    EditorHandlerCustom.DrawProperty(itemObject, customSkin, "Item Prefab");
                    EditorHandlerCustom.DrawProperty(itemParent, customSkin, "Item Parent");
                    EditorHandlerCustom.DrawProperty(backgroundItemsImage, customSkin, "Background Items");
                    EditorHandlerCustom.DrawProperty(eventTrigger, customSkin, "eventTrigger");

                    EditorHandlerCustom.DrawProperty(listCG, customSkin, "List Canvas Group");
                    
                    break;
                case 2:
                    EditorHandlerCustom.DrawHeader(customSkin, "Customization Header", 6);
                    enableIcon.boolValue = EditorHandlerCustom.DrawToggle(enableIcon.boolValue, customSkin, "Enable Header Icon");
                    EditorHandlerCustom.DrawPropertyCW(itemSpacing, customSkin, "Item Spacing", 90);

                    GUILayout.BeginVertical(EditorStyles.helpBox);
                    GUILayout.BeginHorizontal();
                    EditorGUILayout.LabelField(new GUIContent("Item Padding"), customSkin.FindStyle("Text"), GUILayout.Width(90));
                    GUILayout.EndHorizontal();
                    EditorGUI.indentLevel = 1;

                    EditorGUILayout.PropertyField(itemPaddingTop, new GUIContent("Top"));
                    EditorGUILayout.PropertyField(itemPaddingBottom, new GUIContent("Bottom"));
                    EditorGUILayout.PropertyField(itemPaddingLeft, new GUIContent("Left"));
                    EditorGUILayout.PropertyField(itemPaddingRight, new GUIContent("Right"));
                    
                    EditorGUI.indentLevel = 0;
                    GUILayout.EndVertical();
                    
                    GUILayout.BeginVertical(EditorStyles.helpBox);
                    
                    EditorGUILayout.PropertyField(disabledDropdowns, new GUIContent("Disabled Dropdowns"));
                    
                    GUILayout.EndVertical();

                    enableDropdownSounds.boolValue = EditorHandlerCustom.DrawTogglePlain(enableDropdownSounds.boolValue, customSkin, "Enable Dropdown Sounds");
                   
                    GUILayout.Space(3);

                    if (enableDropdownSounds.boolValue)
                    {
                        GUILayout.BeginVertical(EditorStyles.helpBox);
                        GUILayout.Space(-3);
                        
                        GUILayout.Space(3);
                        
                        EditorHandlerCustom.DrawProperty(hoverSound, customSkin, "Hover Sound");

                        GUILayout.EndVertical();
                        GUILayout.BeginVertical(EditorStyles.helpBox);
                        GUILayout.Space(-3);
                        
                        GUILayout.Space(3);
                        
                        EditorHandlerCustom.DrawProperty(clickSound, customSkin, "Click Sound");

                        GUILayout.EndVertical();
                        
                    }
                    
                    EditorHandlerCustom.DrawHeader(customSkin, "Animation Header", 10);
                    
                    GUILayout.BeginVertical(EditorStyles.helpBox);
                    
                    EditorHandlerCustom.DrawPropertyCW(durationOpenAnimateDropdown, customSkin, "Duration Open Dropdown", 180);
                    EditorHandlerCustom.DrawPropertyCW(durationCloseAnimateDropdown, customSkin, "Duration Close Dropdown", 180);
                    
                    GUILayout.EndVertical();
                    
                    GUILayout.Space(5);
                    
                    GUILayout.BeginVertical(EditorStyles.helpBox);
                    
                    _enableChangeImageOpenDropdown.boolValue = EditorHandlerCustom.DrawTogglePlain(_enableChangeImageOpenDropdown.boolValue, customSkin, "Enable Change Image hen Open");

                    GUILayout.Space(5);

                    if (_enableChangeImageOpenDropdown.boolValue)
                    {
                        EditorHandlerCustom.DrawPropertyCW(_animationCloseDropdown, customSkin, "Animation Close Dropdown", 180);  
                        EditorHandlerCustom.DrawPropertyCW(_animationOpenDropdown, customSkin, "Animation Open Dropdown", 180); 
                    }
                    
                    GUILayout.EndVertical();
                    
                    break;
            }
            
            if (!Application.isPlaying) { this.Repaint(); }
            serializedObject.ApplyModifiedProperties();
        }
    }
}
