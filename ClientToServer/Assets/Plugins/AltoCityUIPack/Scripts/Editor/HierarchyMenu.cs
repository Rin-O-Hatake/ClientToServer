using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace Plugins.AltoCityUIPack.Scripts.Editor
{
    public class HierarchyMenu : UnityEditor.Editor
    {
        [MenuItem("GameObject/AltoCityUIPack/Button/SmallCircle", false, 8)]
        static void CreateSmallCircleButton()
        {
            CreateUIComponent("Button/P_SmallCircleButton", "Button");
        }
        
        [MenuItem("GameObject/AltoCityUIPack/Button/Basic", false, 8)]
        static void CreateBasicButton()
        {
            CreateUIComponent("Button/P_Button", "Button");
        }
        
        [MenuItem("GameObject/AltoCityUIPack/InputField", false, 8)]
        static void CreateInputField()
        {
            CreateUIComponent("Input Field/P_InputView", "InputField");
        }
        
        [MenuItem("GameObject/AltoCityUIPack/ScrollBar/Vertical", false, 8)]
        static void CreateScrollBarVertical()
        {
            CreateUIComponent("Scrollbar/P_ScrollbarPanelVertical", "Scrollbar Vertical");
        }
        
        [MenuItem("GameObject/AltoCityUIPack/ScrollBar/Horizontal", false, 8)]
        static void CreateScrollBarHorizontal()
        {
            CreateUIComponent("Scrollbar/P_ScrollbarPanelHorizontal", "Scrollbar Horizontal");
        }
        
        [MenuItem("GameObject/AltoCityUIPack/Dropdown", false, 8)]
        static void CreateDropdown()
        {
            CreateUIComponent("Dropdown/Dropdown Alto", "Dropdown");
        }
        
        [MenuItem("GameObject/AltoCityUIPack/Switch", false, 8)]
        static void CreateSwitch()
        {
            CreateUIComponent("Switch/P_Switch", "Switch");
        }
        
        [MenuItem("GameObject/AltoCityUIPack/Slider/Default", false, 8)]
        static void CreateSlider()
        {
            CreateUIComponent("Slider/Slider", "Slider");
        }
        
        [MenuItem("GameObject/AltoCityUIPack/Slider/Radial", false, 8)]
        static void CreateSliderRadial()
        {
            CreateUIComponent("Slider/P_SliderRadial", "SliderRadial");
        }
        
        [MenuItem("GameObject/AltoCityUIPack/Notifications", false, 8)]
        static void CreateNotification()
        {
            CreateUIComponent("Notifications/P_Notification", "Notification");
        }

        static string objectPath;

        static void GetObjectPath()
        {
            objectPath = AssetDatabase.GetAssetPath(Resources.Load("Alto City UI Pack"));
            objectPath = objectPath.Replace("Resources/Alto City UI Pack.asset", "").Trim();
            objectPath += "Prefabs/";
        }

        static void MakeSceneDirty(GameObject source, string sourceName)
        {
            if (Application.isPlaying == false)
            {
                Undo.RegisterCreatedObjectUndo(source, sourceName);
                EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());
            }
        }

        static void ShowErrorDialog()
        {
            EditorUtility.DisplayDialog("Modern UI Pack", "Cannot create the object due to missing manager file. " +
                    "Make sure you have 'MUIP Manager' file in Modern UI Pack > Resources folder.", "Okay");
        }

        static void CreateUIComponent(string resourcePath, string name)
        {
            try
            {
                GetObjectPath();
                // UpdateCustomEditorPath();
                GameObject clone = Instantiate(AssetDatabase.LoadAssetAtPath(objectPath + resourcePath + ".prefab", typeof(GameObject)), Vector3.zero, Quaternion.identity) as GameObject;

                try
                {
                    if (Selection.activeGameObject == null)
                    {
                        var canvas = (Canvas)GameObject.FindObjectsOfType(typeof(Canvas))[0];
                        clone.transform.SetParent(canvas.transform, false);
                    }

                    else { clone.transform.SetParent(Selection.activeGameObject.transform, false); }

                    clone.name = name;
                    // LayoutRebuilder.ForceRebuildLayoutImmediate(clone.GetComponent<RectTransform>());
#if !UNITY_2021_3_OR_NEWER || UNITY_2022_1_OR_NEWER
                    MakeSceneDirty(clone, clone.name);
#endif
                }

                catch
                {
                    // CreateCanvas();
                    var canvas = (Canvas)FindObjectsOfType(typeof(Canvas))[0];
                    clone.transform.SetParent(canvas.transform, false);
                    clone.name = "Button";
#if !UNITY_2021_3_OR_NEWER || UNITY_2022_1_OR_NEWER
                    MakeSceneDirty(clone, clone.name);
#endif
                }

                Selection.activeObject = clone;
            }

            catch { ShowErrorDialog(); }
        }

        // [MenuItem("GameObject/Modern UI Pack/Canvas", false, 8)]
        // static void CreateCanvas()
        // {
        //     try
        //     {
        //         GetObjectPath();
        //         // UpdateCustomEditorPath();
        //         GameObject clone = Instantiate(AssetDatabase.LoadAssetAtPath(objectPath + "Canvas/Canvas" + ".prefab", typeof(GameObject)), Vector3.zero, Quaternion.identity) as GameObject;
        //         clone.name = clone.name.Replace("(Clone)", "").Trim();
        //         Selection.activeObject = clone;
        //         MakeSceneDirty(clone, clone.name);
        //     }
        //
        //     catch { ShowErrorDialog(); }
        // }

        // [MenuItem("Tools/Modern UI Pack/Show UI Manager %#M")]
        // static void ShowManager()
        // {
        //     Selection.activeObject = Resources.Load("MUIP Manager");
        //
        //     if (Selection.activeObject == null)
        //         Debug.Log("<b>[Modern UI Pack]</b>Can't find a file named 'MUIP Manager'. Make sure you have 'MUIP Manager' file in Resources folder.");
        // }
    }
}
