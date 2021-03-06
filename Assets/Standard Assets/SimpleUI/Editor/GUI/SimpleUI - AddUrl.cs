﻿using System;
using UnityEngine;
using UnityEditor;

namespace SimpleUI
{
    // adding new routes
    // dragging prefabs
    public partial class SimpleUI
    {
        static GameObject PossiblePrefab;
        static string possiblePrefabName = "";

        static string draggedUrl = "";
        static string draggedName = "";
        static string draggedPath = "";

        private void RenderMakingAPrefabFromGameObject()
        {
            const string defaultPrefabName = "Bad prefab name: You cannot name new prefab GameObject, cause it's easy to confuse name";

            Space();
            possiblePrefabName = EditorGUILayout.TextField("Name", possiblePrefabName);

            var path = $"Assets/Prefabs/{possiblePrefabName}.prefab";

            bool hasSameNamePrefabAlready = AssetDatabase.LoadAssetAtPath<GameObject>(path) != null;
            bool isEmpty = possiblePrefabName.Length == 0;
            bool isDefaultName = possiblePrefabName.ToLower().Equals("gameobject");

            bool isNameOK = !isEmpty && !isDefaultName && !hasSameNamePrefabAlready;

            if (!isNameOK)
            {
                if (isDefaultName)
                    EditorGUILayout.HelpBox(defaultPrefabName, MessageType.Error);
                //EditorGUILayout.LabelField(defaultPrefabName);

                if (hasSameNamePrefabAlready)
                    EditorGUILayout.HelpBox($"Bad prefab name: prefab ({path}) already exists)", MessageType.Error);
                // EditorGUILayout.LabelField($"!!!Bad prefab name: empty: {isEmpty}, name=gameobject: {isDefaultName}, prefab already exists: {hasSameNamePrefabAlready}");
            }

            if (isNameOK && Button("CREATE prefab!"))
            {
                PrefabUtility.SaveAsPrefabAssetAndConnect(PossiblePrefab, path, InteractionMode.UserAction, out var success);

                Debug.Log("Prefab saving " + success);

                if (success)
                {
                    isDraggedGameObjectMode = false;
                    HandleDraggedPrefab(PossiblePrefab);
                }
            }

            Space();
            if (Button("Go Back"))
            {
                isDraggedGameObjectMode = false;
                isDraggedPrefabMode = false;
            }
        }

        void RenderAddingNewRouteFromDraggedPrefab()
        {
            Space();
            GUILayout.Label("Add DRAGGED prefab", EditorStyles.boldLabel);

            draggedName = EditorGUILayout.TextField("Name", draggedName);
            draggedUrl = EditorGUILayout.TextField("Url", draggedUrl);

            var dataCorrect = draggedUrl.Length > 0 && draggedName.Length > 0;

            if (dataCorrect && GUILayout.Button("Add DRAGGED prefab!"))
            {
                Space();

                draggedUrl = SimpleUI.GetValidatedUrl(draggedUrl);

                AddAsset(draggedUrl, draggedPath, draggedName);

                isDraggedPrefabMode = false;

                SaveData();

                Debug.Log("Added DRAGGED prefab");

                DestroyImmediate(PossiblePrefab);

                PossiblePrefab = null;

                Debug.Log("Removed object too");
            }
        }


        void SetAddingRouteMode()
        {
            var path = SimpleUI.GetOpenedAssetPath();

            // pick values from asset path
            var name = SimpleUI.GetPrettyNameFromAssetPath(path);
            SetNewName(name);

            var url = newUrl;
            if (!newUrl.EndsWith("/"))
                url += "/";

            url += newName;
            SetNewUrl(url);

            isUrlAddingMode = true;
        }

        void RenderAddingNewRoute()
        {
            if (!isUrlAddingMode)
            {
                SetAddingRouteMode();
            }

            Space();

            var assetType = isPrefabMode ? "prefab" : "SCENE";

            if (!isPrefabMode)
                return;

            var assetPath = SimpleUI.GetOpenedAssetPath();
            GUILayout.Label($"Add current asset ({assetType})", EditorStyles.boldLabel);

            SetNewUrl(EditorGUILayout.TextField("Url", newUrl));

            var url = newUrl;

            if (url == null)
                return;

            bool urlOK = url.StartsWith("/");
            bool newNameOK = newName.Length > 0;

            if (urlOK)
            {
                SetNewName(EditorGUILayout.TextField("Name", newName));
            }
            else
            {
                EditorGUILayout.HelpBox("Url needs to start with /", MessageType.Error);
            }

            if (urlOK && newNameOK)
            {
                Space();
                if (GUILayout.Button("Add asset!")) //  <" + newName + ">
                {
                    Debug.Log("Added asset");

                    AddAsset(url, assetPath, newName);

                    SaveData();
                }
            }
        }

        void AddAsset(string url, string assetPath, string name)
        {
            prefabs.Add(new SimpleUISceneType(url, assetPath, name) { LastOpened = DateTime.Now.Ticks });
        }

        void HandleDraggedPrefab(GameObject go)
        {
            isDraggedPrefabMode = true;
            PossiblePrefab = go;

            var parent = PrefabUtility.GetCorrespondingObjectFromSource(go);
            string prefabPath = AssetDatabase.GetAssetPath(parent);

            Debug.Log("route = " + prefabPath);

            // try to attach this prefab
            // to current prefab

            draggedName = SimpleUI.GetPrettyNameFromAssetPath(prefabPath);
            draggedPath = prefabPath;
            draggedUrl = newUrl.TrimEnd('/') + "/" + draggedName.TrimStart('/');
        }

        void HandleDraggedScene(string path)
        {
            draggedName = SimpleUI.GetPrettyNameFromAssetPath(path);
            draggedPath = path;
            draggedUrl = newUrl.TrimEnd('/') + "/" + draggedName.TrimStart('/');
        }

        void HandleDraggedGameObject(GameObject go)
        {
            isDraggedGameObjectMode = true;

            possiblePrefabName = go.name;

            draggedName = go.name;
            draggedPath = "";
            draggedUrl = newUrl.TrimEnd('/') + "/" + draggedName.TrimStart('/'); // draggedUrl = newUrl + "/" + draggedName;

            PossiblePrefab = go;
        }


        void HandleDragAndDrop()
        {
            if (Event.current.type == EventType.DragUpdated)
            {
                DragAndDrop.visualMode = DragAndDropVisualMode.Copy;
                Event.current.Use();
            }
            else if (Event.current.type == EventType.DragPerform)
            {
                // To consume drag data.
                DragAndDrop.AcceptDrag();

                // GameObjects from hierarchy.
                if (DragAndDrop.paths.Length == 0 && DragAndDrop.objectReferences.Length > 0)
                {
                    foreach (var obj in DragAndDrop.objectReferences)
                    {
                        var go = obj as GameObject;
                        bool isPrefab = PrefabUtility.GetPrefabAssetType(obj) != PrefabAssetType.NotAPrefab;

                        if (isPrefab)
                        {
                            Debug.Log("prefab - " + obj);

                            HandleDraggedPrefab(go);
                        }
                        else
                        {
                            Debug.Log("GameObject - " + obj);

                            HandleDraggedGameObject(go);
                        }
                    }
                }
                // Object outside project. It mays from File Explorer (Finder in OSX).
                else if (DragAndDrop.paths.Length > 0 && DragAndDrop.objectReferences.Length == 0)
                {
                    Debug.Log("File");
                    foreach (string path in DragAndDrop.paths)
                    {
                        if (SimpleUI.isSceneAsset(path))
                        {
                            Debug.Log("- Dragging Scene! " + path);

                            HandleDraggedScene(path);
                        }
                        else
                        {
                            Debug.Log("- " + path);
                        }
                    }
                }
                // Unity Assets including folder.
                else if (DragAndDrop.paths.Length == DragAndDrop.objectReferences.Length)
                {
                    Debug.Log("UnityAsset");
                    for (int i = 0; i < DragAndDrop.objectReferences.Length; i++)
                    {
                        var obj = DragAndDrop.objectReferences[i];
                        string path = DragAndDrop.paths[i];
                        Debug.Log(obj.GetType().Name);

                        // Folder.
                        if (obj is DefaultAsset)
                        {
                            Debug.Log(path);
                        }
                        // C# or JavaScript.
                        else if (obj is MonoScript)
                        {
                            Debug.Log(path + "\n" + obj);
                        }
                        else if (obj is Texture2D)
                        {
                        }
                    }
                }
                // Log to make sure we cover all cases.
                else
                {
                    Debug.Log("Out of reach");
                    Debug.Log("Paths:");
                    foreach (string path in DragAndDrop.paths)
                    {
                        Debug.Log("- " + path);
                    }

                    Debug.Log("ObjectReferences:");
                    foreach (var obj in DragAndDrop.objectReferences)
                    {
                        Debug.Log("- " + obj);
                    }
                }
            }
        }
    }
}