

////[InitializeOnLoad]
////public static class DisplayCompleteUrl
//[CustomEditor(typeof(DisplayCompleteUrl))]
//public class DisplayCompleteUrl2: Editor
//{
//    //static EditorWindow Window;
//    //static SimpleUI SimpleUI;

//    //static void SetWindow()
//    //{
//    //    Window = EditorWindow.GetWindow(typeof(SimpleUI));

//    //    SimpleUI = Window as SimpleUI;

//    //    Debug.Log("Drawing " + SimpleUI.newUrl);
//    //}

//    //private void OnEnable()
//    //{
//    //    PrefabStage.prefabStageOpened += DummyMethod;
//    //}

//    //static DisplayCompleteUrl()
//    //{
//    //    PrefabStage.prefabStageOpened += DummyMethod;
//    //}

//    //public DisplayCompleteUrl()
//    //{
//    //    PrefabStage.prefabStageOpened += DummyMethod;
//    //}

//    //static void DummyMethod(PrefabStage stage)
//    //{
//    //    SetWindow();
//    //}

//    private void OnSceneGUI()
//    {
//        GUIStyle style = GUI.skin.FindStyle("Button");
//        style.richText = true;

//        EditorGUILayout.LabelField("DISPLAY COMPLETE URL");

//        if (GUILayout.Button("aa", style))
//        {
//            Debug.Log("OnSceneGUI Button");
//        }
//    }

//    //[ExecuteInEditMode]
//    //private void OnGUI()
//    //{
//    //    Debug.Log("OnGUI");

//    //    EditorGUILayout.LabelField("Left", EditorStyles.boldLabel);
//    //}

//    //private void OnDrawGizmos()
//    //{
//    //    Debug.Log("OnDrawGizmos");

//    //    EditorGUILayout.LabelField("Left", EditorStyles.boldLabel);

//    //}
//}



//public static class FixUnpackEnvironmentUIParent
//{
//    static FixUnpackEnvironmentUIParent()
//    {
//        PrefabStage.prefabStageOpened += DummyMethod;
//    }

//    static void DummyMethod(PrefabStage stage) { }
//}












//using System.Collections;
//using System.Collections.Generic;
//using UnityEditor;
//using UnityEngine;

//#if UNITY_EDITOR
//using UnityEditor.Experimental.SceneManagement;

////[InitializeOnLoad]
////public static class DisplayCompleteUrl
//[CustomEditor(typeof(DisplayCompleteUrl))]
//public class DisplayCompleteUrl2: Editor
//{
//    //static EditorWindow Window;
//    //static SimpleUI SimpleUI;

//    //static void SetWindow()
//    //{
//    //    Window = EditorWindow.GetWindow(typeof(SimpleUI));

//    //    SimpleUI = Window as SimpleUI;

//    //    Debug.Log("Drawing " + SimpleUI.newUrl);
//    //}

//    //private void OnEnable()
//    //{
//    //    PrefabStage.prefabStageOpened += DummyMethod;
//    //}

//    //static DisplayCompleteUrl()
//    //{
//    //    PrefabStage.prefabStageOpened += DummyMethod;
//    //}

//    //public DisplayCompleteUrl()
//    //{
//    //    PrefabStage.prefabStageOpened += DummyMethod;
//    //}

//    //static void DummyMethod(PrefabStage stage)
//    //{
//    //    SetWindow();
//    //}

//    private void OnSceneGUI()
//    {
//        GUIStyle style = GUI.skin.FindStyle("Button");
//        style.richText = true;

//        EditorGUILayout.LabelField("DISPLAY COMPLETE URL");

//        if (GUILayout.Button("aa", style))
//        {
//            Debug.Log("OnSceneGUI Button");
//        }
//    }

//    //[ExecuteInEditMode]
//    //private void OnGUI()
//    //{
//    //    Debug.Log("OnGUI");

//    //    EditorGUILayout.LabelField("Left", EditorStyles.boldLabel);
//    //}

//    //private void OnDrawGizmos()
//    //{
//    //    Debug.Log("OnDrawGizmos");

//    //    EditorGUILayout.LabelField("Left", EditorStyles.boldLabel);

//    //}
//}



////public static class FixUnpackEnvironmentUIParent
////{
////    static FixUnpackEnvironmentUIParent()
////    {
////        PrefabStage.prefabStageOpened += DummyMethod;
////    }

////    static void DummyMethod(PrefabStage stage) { }
////}
//#endif