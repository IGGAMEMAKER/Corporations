using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEditor;
using UnityEditor.Experimental.SceneManagement;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;
using System.Linq;

namespace SimpleUI
{
    // Read
    // Как-работает-editorwindow-ongui-в-unity-3d
    // https://ru.stackoverflow.com/questions/515395/%D0%9A%D0%B0%D0%BA-%D1%80%D0%B0%D0%B1%D0%BE%D1%82%D0%B0%D0%B5%D1%82-editorwindow-ongui-%D0%B2-unity-3d

    // https://answers.unity.com/questions/37180/how-to-highlight-or-select-an-asset-in-project-win.html
    // https://gist.github.com/rutcreate/0af3c34abd497a2bceed506f953308d7
    // https://stackoverflow.com/questions/36850296/get-a-prefabs-file-location-in-unity
    // https://forum.unity.com/threads/dropdown-in-inspector.468739/
    // https://forum.unity.com/threads/editorguilayout-scrollview-not-working.143502/

    // optional
    // https://answers.unity.com/questions/201848/how-to-create-a-drop-down-menu-in-editor.html
    // https://gist.github.com/bzgeb/3800350
    // GUID http://www.unity3d.ru/distribution/viewtopic.php?f=18&t=4640


    //string myString = "Hello World";
    //bool groupEnabled;
    //bool myBool = true;
    //float myFloat = 1.23f;
    // void RenderExample()
    // {
    // // myString = EditorGUILayout.TextField ("Text Field", myString);
    //         
    // // groupEnabled = EditorGUILayout.BeginToggleGroup ("Optional Settings", groupEnabled);
    // // myBool = EditorGUILayout.Toggle ("Toggle", myBool);
    // // myFloat = EditorGUILayout.Slider ("Slider", myFloat, -3, 3);
    // // EditorGUILayout.EndToggleGroup ();
    // }

    //[CreateAssetMenu(fileName = "SimpleUIDataContainer", menuName = "SimpleUI data container", order = 51)]
    public partial class SimpleUI : EditorWindow
    {
        bool isConcreteUrlChosen;

        public string newUrl = "";
        public string newName = "";
        public string newPath = "";


        internal static string GetOpenedAssetPath()
        {
            if (isPrefabMode)
            {
                return PrefabStageUtility.GetCurrentPrefabStage().assetPath;
            }

            return SceneManager.GetActiveScene().path;
        }

        static SimpleUI()
        {
            BoldPrint("Static SimpleUI");

            PrefabStage.prefabStageOpened += PrefabStage_prefabOpened;
            PrefabStage.prefabStageClosing += PrefabStage_prefabClosed;

            PrefabStage.prefabSaved += PrefabStage_prefabSaved;

            EditorSceneManager.activeSceneChangedInEditMode += EditorSceneManager_activeSceneChangedInEditMode;
            EditorSceneManager.sceneOpened += EditorSceneManager_sceneOpened;
            EditorSceneManager.sceneClosing += EditorSceneManager_sceneClosing;

            //EditorApplication.quitting += Application_quitting;
        }

        public static SimpleUI GetInstance()
        {
            BoldPrint("Get Instance");

            try
            {
                var w = GetWindow<SimpleUI>("Simple UI", false);
                w.autoRepaintOnSceneChange = true;
                w.ScanProject();

                return w;
            }
            catch (Exception ex)
            {
                Debug.LogError("ERROR IN GET INSTANCE");
                Debug.LogError(ex);
            }

            BoldPrint("return new SimpleUI instance");
            return new SimpleUI();
        }

        //private void OnEnable()
        //{
        //    if (isProjectScanned)
        //    {
        //        OnRecompile();
        //    }
        //    else
        //    {
        //        Print("Initialize");
        //    }
        //}


        //void OnRecompile()
        //{
        //    Print("OnRecompile");

        //    if (EditorWindow.HasOpenInstances<OpenUrlPickerWindow>())
        //        EditorWindow.GetWindow<OpenUrlPickerWindow>().Close();

        //    LoadScripts();
        //    LoadReferences(GetCurrentUrl());
        //}


        //private static void Application_quitting()
        //{
        //    BoldPrint("Quitting");

        //    var instance = GetInstance();

        //    instance.newName = "";
        //    instance.newPath = "";
        //    instance.newUrl = "";
        //}
    }
}