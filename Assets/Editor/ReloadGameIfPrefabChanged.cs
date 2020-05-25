#if UNITY_EDITOR
using Assets.Core;
using System.Collections;
using UnityEditor;
using UnityEditor.Experimental.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

[InitializeOnLoad]
class CustomPrefabEnvironment
{
    static CustomPrefabEnvironment()
    {
        PrefabStage.prefabSaved += PrefabStage_prefabSaved;
    }

    static void PrefabStage_prefabSaved(GameObject obj)
    {
        Debug.Log("Prefab edited: " + obj.name);

        if (Application.isPlaying)
        {
            SceneManager.UnloadScene(1);
            //State.LoadGameScene();
            SceneManager.LoadScene(1, LoadSceneMode.Additive);

            ScheduleUtils.PauseGame(Contexts.sharedInstance.game);

            //ScreenUtils.UpdateScreen(Contexts.sharedInstance.game);
            //StartCorutine
        }
    }

    IEnumerable RefreshScreen()
    {
        yield return new WaitForSeconds(0.5f);

    }

    static void OnPrefabStageOpened(PrefabStage prefabStage)
    {
        Debug.Log("OnPrefabStageOpened " + prefabStage.prefabAssetPath);

        //// Get info from the PrefabStage
        //var root = prefabStage.prefabContentsRoot;
        //var scene = prefabStage.scene;
        //var renderer = root.GetComponent<Renderer>();

        //// If no renderer skip our custom environment
        //if (renderer == null)
        //    return;

        //// Create environment plane
        //var plane = GameObject.CreatePrimitive(PrimitiveType.Plane);
        //SceneManager.MoveGameObjectToScene(plane, scene);

        //// Adjust environment plane to the prefab root's lower bounds
        //Bounds bounds = renderer.bounds;
        //plane.transform.position = new Vector3(bounds.center.x, bounds.min.y, bounds.center.z);
    }
}
#endif