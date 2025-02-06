#if UNITY_EDITOR
using Plugins.AltoCityUIPack.Scripts.Gradient;
using UnityEditor;
using UnityEngine;

namespace Plugins.AltoCityUIPack.Scripts.Editor.Gradient
{
    [CustomEditor(typeof(GradientCustomAltoCityUIPack))]
    public class GradientCustomAltoCityUIPackEditor : UnityEditor.Editor
    {
        private GUISkin customSkin;
        private int currentTab;

        private void OnEnable()
        {
            customSkin = (GUISkin)Resources.Load("AltoCity-EditorDark");
        }
        
        public override void OnInspectorGUI()
        {
            EditorHandlerCustom.DrawComponentHeader(customSkin, "GradientHeader");

            GUIContent[] toolbarTabs = new GUIContent[1];
            toolbarTabs[0] = new GUIContent("Settings");

            currentTab = EditorHandlerCustom.DrawTabs(currentTab, toolbarTabs, customSkin);

            if (GUILayout.Button(new GUIContent("Settings", "Settings"), customSkin.FindStyle("Tab Settings")))
                currentTab = 0;

            GUILayout.EndHorizontal();

            var effectGradient = serializedObject.FindProperty("_effectGradient");
            var gradientType = serializedObject.FindProperty("_gradientType");
            var offset = serializedObject.FindProperty("_offset");
            var zoom = serializedObject.FindProperty("_zoom");
            var modifyVertices = serializedObject.FindProperty("_modifyVertices");

            switch (currentTab)
            {
                case 0:
                    EditorHandlerCustom.DrawHeader(customSkin, "Options Header", 6);
                    EditorHandlerCustom.DrawPropertyCW(effectGradient, customSkin, "Gradient", 100);
                    EditorHandlerCustom.DrawPropertyCW(gradientType, customSkin, "Type", 100);
                    EditorHandlerCustom.DrawPropertyCW(offset, customSkin, "Offset", 100);
                    EditorHandlerCustom.DrawPropertyCW(zoom, customSkin, "Zoom", 100);
                    modifyVertices.boolValue = EditorHandlerCustom.DrawToggle(modifyVertices.boolValue, customSkin, "Complex Gradient");
                    break;
            }

            if (!Application.isPlaying)
            {
                this.Repaint();
            }
            serializedObject.ApplyModifiedProperties();
        }
    }
}
#endif