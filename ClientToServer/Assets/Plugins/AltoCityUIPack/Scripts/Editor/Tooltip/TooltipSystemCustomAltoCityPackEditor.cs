#if UNITY_EDITOR
using Plugins.AltoCityUIPack.Scripts.Tooltip;
using UnityEditor;
using UnityEngine;

namespace Plugins.AltoCityUIPack.Scripts.Editor.Tooltip
{
    [CanEditMultipleObjects]
    [CustomEditor(typeof(TooltipSystemCustomAltoCityPack))]
    public class TooltipSystemCustomAltoCityPackEditor : UnityEditor.Editor
    {
        private GUISkin customSkin;
        private TooltipSystemCustomAltoCityPack tooltipTarget;
        
        private void OnEnable()
        {
            tooltipTarget = (TooltipSystemCustomAltoCityPack)target;
            
            customSkin = (GUISkin)Resources.Load("AltoCity-EditorDark");
        }
        
        public override void OnInspectorGUI()
        {
            EditorHandlerCustom.DrawComponentHeader(customSkin, "Tooltip Top Header");

            GUIContent[] toolbarTabs = new GUIContent[3];
            toolbarTabs[0] = new GUIContent("Content");
            toolbarTabs[1] = new GUIContent("Resources");
            toolbarTabs[2] = new GUIContent("Settings");
            
            tooltipTarget.LastTabIndex = EditorHandlerCustom.DrawTabs(tooltipTarget.LastTabIndex, toolbarTabs, customSkin);

            if (GUILayout.Button(new GUIContent("Content", "Content"), customSkin.FindStyle("Tab Content")))
                tooltipTarget.LastTabIndex = 0;
            if (GUILayout.Button(new GUIContent("Resources", "Resources"), customSkin.FindStyle("Tab Resources")))
                tooltipTarget.LastTabIndex = 1;
            if (GUILayout.Button(new GUIContent("Settings", "Settings"), customSkin.FindStyle("Tab Settings"))) 
                tooltipTarget.LastTabIndex = 2;
            
            GUILayout.EndHorizontal();
            
            var tooltipAltoCityUIPack = serializedObject.FindProperty("_tooltipAltoCityUIPack");
            
            var tooltipRectTransform = serializedObject.FindProperty("_tooltipRectTransform");

            var currentTypeTooltip = serializedObject.FindProperty("_currentTypeTooltip");
            var allTextAlignment = serializedObject.FindProperty("_allTextAlignment");
            var allFont = serializedObject.FindProperty("_allFont");
            var maxWidthToolTip = serializedObject.FindProperty("_maxWidthToolTip");


            tooltipTarget.UpdateUI();
            switch (tooltipTarget.LastTabIndex)
            {
                case 0:
                    EditorHandlerCustom.DrawHeader(customSkin, "Content Header", 6);
                    
                    GUILayout.BeginVertical(EditorStyles.helpBox);
                    
                    EditorHandlerCustom.DrawPropertyCW(currentTypeTooltip, customSkin, "Type Tooltip [Editor]", 180);
                    
                    GUILayout.Space(4);
                    
                    EditorHandlerCustom.DrawPropertyCW(allTextAlignment, customSkin, "All Text Alignment", 180);
                    EditorHandlerCustom.DrawPropertyCW(allFont, customSkin, "All Font", 180);
                    EditorHandlerCustom.DrawPropertyCW(maxWidthToolTip, customSkin, "Max Width ToolTip", 180);
                    
                    GUILayout.EndVertical();
                    break;
                case 1:
                    EditorHandlerCustom.DrawHeader(customSkin, "Core Header", 6);
                    
                    EditorHandlerCustom.DrawProperty(tooltipAltoCityUIPack, customSkin, "Tooltip");
                    EditorHandlerCustom.DrawProperty(tooltipRectTransform, customSkin, "Tooltip RectTransform");

                    break;

                case 2:
                    EditorHandlerCustom.DrawHeader(customSkin, "Options Header", 6);
                    break;
            }

            if (Application.isPlaying == false) { this.Repaint(); }
            serializedObject.ApplyModifiedProperties();
        }
    }
}
#endif
