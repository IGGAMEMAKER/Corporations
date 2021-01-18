using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor.Experimental.SceneManagement;

[InitializeOnLoad]
public static class DisplayCompleteUrl
{
    static EditorWindow Window;
    static SimpleUI SimpleUI;

    private static void SetWindow()
    {
        Window = EditorWindow.GetWindow(typeof(SimpleUI));

        SimpleUI = Window as SimpleUI;

        Debug.Log("Drawing " + SimpleUI.newUrl);
    }

    static DisplayCompleteUrl()
    {
        PrefabStage.prefabStageOpened += DummyMethod;
    }

    static void DummyMethod(PrefabStage stage) {
        SetWindow();
    }
}


 
//public static class FixUnpackEnvironmentUIParent
//{
//    static FixUnpackEnvironmentUIParent()
//    {
//        PrefabStage.prefabStageOpened += DummyMethod;
//    }

//    static void DummyMethod(PrefabStage stage) { }
//}
#endif