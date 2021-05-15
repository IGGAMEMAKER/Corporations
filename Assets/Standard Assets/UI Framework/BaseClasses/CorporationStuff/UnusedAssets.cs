using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

// https://gist.github.com/Happsson/af02d7a644db6b44bc834c8699bf3495

public class UnusedAssets : MonoBehaviour
{
}

public class UnusedPrefabs : EditorWindow
{
    [MenuItem("Window/Clean up Prefabs")]
    public static void ShowWindow()
    {
        EditorWindow.GetWindow<UnusedPrefabs>("Clean up Prefabs");
    }
}

public class InactiveCodeDetector : EditorWindow
{
    private bool isDone = false;
    private List<string> paths = new List<string>();
    private List<string> prefabs = new List<string>();
    private List<Scene> allScenes = new List<Scene>();

    private Scene currentlyOpenScene;

    Vector2 scrollPosition;


    private List<string> allUsedComponents = new List<string>();

    private List<String> unusedAssets = new List<string>();

    [MenuItem("Window/Clean up scripts")]
    public static void ShowWindow()
    {
        EditorWindow.GetWindow<InactiveCodeDetector>("Clean up scripts");
    }

    private void OnGUI()
    {
        GUILayout.Label("Find possibly unused scripts.");
        if (GUILayout.Button("Search"))
        {
            Console.Clear();
            isDone = false;
            unusedAssets.Clear();
            CleanUp();
            currentlyOpenScene = EditorSceneManager.GetActiveScene();
            FindAll();
            CheckAllScenes();
            Compare();
            CloseScenes();
            // CleanUp();
            isDone = true;
        }

        if (isDone)
        {
            PromtUser();
        }

    }

    private void PromtUser()
    {
        Debug.ClearDeveloperConsole();
        string content = $"{unusedAssets.Count} / {allUsedComponents.Count} scripts are not found on any GameObject in this project.";
        string warning =
            "NOTE - This does NOT mean it is safe to delete. The script could still be accessed via code or on a prefab";
        string foundPrefabs = $"Found {prefabs.Count} prefabs";

        scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition);
        GUILayout.Label(content);
        GUILayout.Label(foundPrefabs);
        GUILayout.Label(warning, EditorStyles.boldLabel);
        foreach (var usedComponent in allUsedComponents)
        {
            Debug.Log("AllUsedComponent " + usedComponent);
        }
        foreach (string s in unusedAssets)
        {
            GUILayout.Label(s);
            if (GUILayout.Button("Show in finder/explorer"))
            {
                EditorUtility.RevealInFinder(s);
            }

            GUILayout.Space(10);
        }

        EditorGUILayout.EndScrollView();
    }

    private void CleanUp()
    {
        paths.Clear();
        allScenes.Clear();
        allUsedComponents.Clear();
        prefabs.Clear();

    }

    private void Compare()
    {
        foreach (string filename in paths)
        {
            string formatted = filename.Split('/').Last();
            if (formatted != "InactiveCodeDetector.cs")
            {
                string isUsed = (from string s in allUsedComponents where s.ToLower() == formatted.ToLower() select s)
                    .FirstOrDefault();

                if (isUsed == null)
                {
                    unusedAssets.Add(filename);
                    //Debug.Log("Script " + filename + " is not attached to any game object in this project");
                }
            }
        }
    }

    private void CheckAllScenes()
    {
        Scene[] scenes = new Scene[EditorSceneManager.sceneCount];
        for (int i = 0; i < scenes.Length; i++)
        {
            scenes[i] = EditorSceneManager.GetSceneAt(i);


            GameObject[] rootObjectsInScene = scenes[i].GetRootGameObjects();
            Debug.Log($"Root objects in scene {scenes[i].name}: " + string.Join("\n", rootObjectsInScene.Select(r => r.name)));
            foreach (GameObject g in rootObjectsInScene)
            {
                foreach (Component c in g.GetComponents<Component>())
                {
                    string name = ObjectNames.GetInspectorTitle(c);
                    if (name.EndsWith("(Script)"))
                    {
                        string formated = name.Replace("(Script)", String.Empty).Replace(" ", String.Empty) + ".cs";
                        allUsedComponents.Add(formated.ToLower());
                    }

                }
            }
        }
    }

    private void CloseScenes()
    {

        foreach (Scene s in allScenes)
        {
            if (s != currentlyOpenScene)
            {
                EditorSceneManager.CloseScene(s, true);
            }
        }

    }

    private void FindAll()
    {
        string p = Application.dataPath;
        ProcessDirectory(p);
    }

    private void ProcessDirectory(string path)
    {
        string[] fileEntries = Directory.GetFiles(path);
        foreach (string filename in fileEntries)
        {
            if (filename.EndsWith(".cs"))
            {
                paths.Add(filename);
            }

            if (filename.EndsWith(".unity"))
            {
                Scene scene = EditorSceneManager.OpenScene(filename, OpenSceneMode.Additive);
                allScenes.Add(scene);
            }

            if (filename.EndsWith(".prefab"))
            {
                prefabs.Add(filename);
            }
        }

        string[] subdirectories = Directory.GetDirectories(path);
        foreach (string sub in subdirectories)
        {
            ProcessDirectory(sub);
        }
    }
}
