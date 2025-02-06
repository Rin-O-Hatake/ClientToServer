using Plugins.AltoCityUIPack.Scripts;
using Plugins.AltoCityUIPack.Scripts.Scrollbar;
using UnityEditor;
using UnityEngine;

namespace Plugins.AltoCityUIPack.Scripting.Scrollbar
{
    [CanEditMultipleObjects]
    [CustomEditor(typeof(ScrollbarCustomUI))]
    public class ScrollbarEditorCustomUI : Editor
    {
        private GUISkin customSkin;
        private ScrollbarCustomUI inputScrollbarTarget;
        
        
        private void OnEnable()
        {
            inputScrollbarTarget = (ScrollbarCustomUI)target;
            
            customSkin = (GUISkin)Resources.Load("AltoCity-EditorDark");
        }
        
        public override void OnInspectorGUI()
        {
            EditorHandlerCustom.DrawComponentHeader(customSkin, "Scrollbar Header");

            GUIContent[] toolbarTabs = new GUIContent[2];
            toolbarTabs[0] = new GUIContent("Content");
            toolbarTabs[1] = new GUIContent("Resources");

            inputScrollbarTarget.latestTabIndex =
                EditorHandlerCustom.DrawTabs(inputScrollbarTarget.latestTabIndex, toolbarTabs, customSkin);

            if (GUILayout.Button(new GUIContent("Content", "Content"), customSkin.FindStyle("Tab Content")))
                inputScrollbarTarget.latestTabIndex = 0;
            if (GUILayout.Button(new GUIContent("Resources", "Resources"), customSkin.FindStyle("Tab Resources")))
                inputScrollbarTarget.latestTabIndex = 1;

            GUILayout.EndHorizontal();
            
            var scrollbarBackgroundColor = serializedObject.FindProperty("_scrollbarBackgroundColor");
            var _scrollbarHandleColor = serializedObject.FindProperty("_scrollbarHandleColor");
            var scrollbarBackgroundImage = serializedObject.FindProperty("_scrollbarBackgroundImage");
            var scrollbarHandleImage = serializedObject.FindProperty("_scrollbarHandleImage");
            var normalColorScrollbar = serializedObject.FindProperty("_normalColorScrollbar");
            
            var scrollRect = serializedObject.FindProperty("_scrollRect");
            var currentScrollbar = serializedObject.FindProperty("_currentScrollbar");
        
            var _movementType = serializedObject.FindProperty("_movementType");
            var _scrollSensity = serializedObject.FindProperty("_scrollSensitivity");
            
            inputScrollbarTarget.UpdateUI();
            switch (inputScrollbarTarget.latestTabIndex)
            {
                case 0:
                    EditorHandlerCustom.DrawHeader(customSkin, "Content Header", 6);

                    #region Icon And Text

                    GUILayout.BeginVertical(EditorStyles.helpBox);
                    GUILayout.Space(-3);
                    
                    EditorHandlerCustom.DrawPropertyCW(scrollbarBackgroundColor, customSkin, "Scrollbar Background Color", 160);
                    EditorHandlerCustom.DrawPropertyCW(_scrollbarHandleColor, customSkin, "Scrollbar Handle Color", 160);
                    EditorHandlerCustom.DrawPropertyCW(normalColorScrollbar, customSkin, "Normal Color Scrollbar", 160);
                    
                    EditorHandlerCustom.DrawPropertyCW(_scrollSensity, customSkin, "_scrollSensitivity", 160);
                    EditorHandlerCustom.DrawPropertyCW(_movementType, customSkin, "Movement Type", 160);

                    GUILayout.EndVertical();

                    #endregion
                break;

                case 1:
                    EditorHandlerCustom.DrawHeader(customSkin, "Core Header", 6);
                    
                    GUILayout.BeginVertical(EditorStyles.helpBox);
                    
                    EditorHandlerCustom.DrawProperty(scrollbarBackgroundImage, customSkin, "Scroll Background Image");
                    EditorHandlerCustom.DrawProperty(scrollbarHandleImage, customSkin, "Scroll Handle Image");
                    EditorHandlerCustom.DrawProperty(scrollRect, customSkin, "ScrollRect");
                    EditorHandlerCustom.DrawProperty(currentScrollbar, customSkin, "Scrollbar");

                    GUILayout.EndVertical();
                    break;
                
            }
            
            if (Application.isPlaying == false) { this.Repaint(); }
            serializedObject.ApplyModifiedProperties();
        }
    }
}
