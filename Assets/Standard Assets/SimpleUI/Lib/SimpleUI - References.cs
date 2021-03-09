using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEditor;
using UnityEditor.Experimental.SceneManagement;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

namespace SimpleUI
{
    // what uses component OpenUrl
    public partial class SimpleUI
    {
        // path - RootAssetPath
        public static PrefabMatchInfo GetPrefabMatchInfo2(MonoBehaviour component, GameObject asset, string path, string[] properties)
        {
            var matchingComponent = new PrefabMatchInfo { PrefabAssetPath = path, ComponentName = component.gameObject.name };
            matchingComponent.URL = (component as OpenUrl).Url;
            matchingComponent.URL_ID = (component as OpenUrl).Url_ID;

            return matchingComponent;
        }

        public static PrefabMatchInfoDetailed GetFullPrefabMatchInfo2(MonoBehaviour component, GameObject asset, string path, string[] properties)
        {
            var matchingComponent = new PrefabMatchInfoDetailed
            {
                prefabMatchInfo = GetPrefabMatchInfo2(component, asset, path, properties),
            };

            matchingComponent.Asset = asset;
            matchingComponent.Component = component as OpenUrl;


            bool isAttachedToRootPrefab = HasNoPrefabsBetweenObjects(component, asset);
            bool isAttachedToNestedPrefab = !isAttachedToRootPrefab;

            if (isAttachedToRootPrefab)
            {
                // directly appears in prefab
                // so you can upgrade it and safely save ur prefab

                matchingComponent.IsDirectMatch = true;
                Print2($"Directly occurs as {matchingComponent.prefabMatchInfo.ComponentName} in {matchingComponent.prefabMatchInfo.PrefabAssetPath} {matchingComponent.prefabMatchInfo.URL}");
            }

            if (isAttachedToNestedPrefab)
            {
                bool isAddedByRoot = IsAddedAsOverridenComponent(component);
                bool isPartOfNestedPrefab = !isAddedByRoot;

                if (isAddedByRoot)
                {
                    // added, but not saved in that prefab
                    // so prefab will not see this component in itself

                    // you need to update URL of this component here, but don't accidentally apply changes to prefab, which this component sits on
                    // you can safely save changes in root prefab as well
                    var outer = PrefabUtility.GetOutermostPrefabInstanceRoot(component);

                    matchingComponent.IsOverridenAsAddedComponent = true;
                    Print2($"{matchingComponent.prefabMatchInfo.ComponentName} Is <b>ATTACHED</b> to some nested prefab\n\nouter={outer.name} {ParseAddedComponents(outer)}, {component.GetInstanceID()}");
                }

                if (isPartOfNestedPrefab)
                {
                    // so you might need to check Overriden Properties

                    if (IsRootOverridenProperties2(component, asset, properties))
                    {
                        // update property and just save root prefab

                        matchingComponent.IsOverridenAsComponentProperty = true;
                        Print2($"Root <b>OVERRIDES VALUES</b> on {matchingComponent.prefabMatchInfo.ComponentName}");
                    }
                    else
                    {
                        // you will upgrade value in it's own prefab
                        // no actions are necessary for root prefab

                        matchingComponent.IsNormalPartOfNestedPrefab = true;
                        Print2($"{matchingComponent.prefabMatchInfo.ComponentName} is <b>part of a nested prefab</b>");
                    }
                }
            }

            return matchingComponent;
        }

        public static List<PrefabMatchInfo> WhatUsesComponent() => WhatUsesComponent<OpenUrl>();
        public static List<PrefabMatchInfo> WhatUsesComponent(string url, List<PrefabMatchInfo> matchInfos) => matchInfos.Where(m => m.URL.Equals(url.TrimStart('/'))).ToList();
        public static List<PrefabMatchInfo> WhatUsesComponent<T>()
        {
            var typeToSearch = typeof(T);

            Debug.Log("Finding all Prefabs and scenes that have the component" + typeToSearch + "…");

            var excludeFolders = new[] { "Assets/Standard Assets" };
            var guids = AssetDatabase.FindAssets("t:scene t:prefab", new[] { "Assets" });

            var paths = guids.Select(AssetDatabase.GUIDToAssetPath).ToList();
            paths.RemoveAll(guid => excludeFolders.Any(guid.Contains));

            var matchingComponents = new List<PrefabMatchInfo>();

            // save state
            var scenes = new List<Scene>();

            foreach (var path in paths)
            {
                GetMatchingComponentsFromAsset(matchingComponents, path);
            }

            // restore state if scenes were opened
            // restore scene
            // restore prefab if was open

            return matchingComponents;
        }

        static void GetMatchingComponentsFromAsset(List<PrefabMatchInfo> matchingComponents, string path)
        {
            var properties = new[] { "Url" };
            List<Scene> openedScenes = new List<Scene>();

            if (isPrefabAsset(path))
            {
                GetMatchingComponentsFromPrefab<OpenUrl>(matchingComponents, path, properties);
            }

            if (isSceneAsset(path))
            {
                GetMatchingComponentsFromScene<OpenUrl>(matchingComponents, path, properties, openedScenes);
            }
        }


        static void GetMatchingComponentsFromScene<T>(List<PrefabMatchInfo> matchingComponents, string path, string[] properties, List<Scene> openedScenes)
        {
            // https://stackoverflow.com/questions/54452347/can-i-programatically-load-scenes-in-the-unity-editor

            var asset1 = AssetDatabase.LoadAssetAtPath<SceneAsset>(path);

            if (asset1 == null)
            {
                Debug.LogError("Cannot load SCENE at path: " + path);
                return;
            }

            //var scene = EditorSceneManager.GetSceneByPath(path);

            bool isOpenedAlready = openedScenes.Any(s => s.path.Equals(path));
            var scene = isOpenedAlready ? openedScenes.First(s => s.path.Equals(path)) : EditorSceneManager.OpenScene(path, OpenSceneMode.Additive);

            //if (!scene.isLoaded)
            //{
            //    Debug.LogError("Scene not loaded: " + path);

            //    return;
            //}

            //if (!scene.IsValid())
            //{
            //    Debug.LogError(asset1);
            //    Debug.LogError(asset1.name);
            //    Debug.LogError("Scene " + path + " is invalid");

            //    return;
            //}

            foreach (var asset in scene.GetRootGameObjects())
            {
                List<T> components = asset.GetComponentsInChildren<T>(true).ToList();

                if (components.Any())
                {
                    Print2("<b>----------------------------------------</b>");
                    Print2("SCENE: Found component(s) OpenUrl" + $" ({components.Count}) in file <b>" + path + "</b>");
                }

                foreach (var component1 in components)
                {
                    var component = component1 as MonoBehaviour;

                    var matchingComponent = GetPrefabMatchInfo2(component, asset, path, properties);

                    matchingComponents.Add(matchingComponent);
                }
            }

            if (!isOpenedAlready)
                EditorSceneManager.CloseScene(scene, true);
        }

        static void GetMatchingComponentsFromPrefab<T>(List<PrefabMatchInfo> matchingComponents, string path, string[] properties)
        {
            var asset = AssetDatabase.LoadAssetAtPath<GameObject>(path);

            if (asset == null)
            {
                Debug.LogError("Cannot load prefab at path: " + path);
                return;
            }

            List<T> components = asset.GetComponentsInChildren<T>(true).ToList();
            var self = asset.GetComponent<T>();

            if (self != null)
            {
                components.Add(self);
                Debug.Log("Add SELF COMPONENT");
            }

            if (components.Any())
            {
                Print2("<b>----------------------------------------</b>");
                Print2("PREFAB: Found component(s) OpenUrl" + $" ({components.Count}) in file <b>" + path + "</b>");
            }

            int componentId = 0;
            foreach (var component1 in components)
            {
                var component = component1 as MonoBehaviour;

                var matchingComponent = GetPrefabMatchInfo2(component, asset, path, properties);
                matchingComponent.ComponentID = componentId;

                matchingComponents.Add(matchingComponent);
                componentId++;
            }
        }



        // ----------------------------------
        public static bool HasNoPrefabsBetweenObjects(MonoBehaviour component, GameObject root)
        {
            // is directly attached to root prefab object with no in between prefabs

            // root GO
            // -> NonPrefab1 GO
            // -> NonPrefab2 GO
            // -> -> NonPrefab3 GO with our component
            // returns true

            // -> NonPrefab1 GO
            // -> Prefab2
            // -> -> NonPrefab3 Go with our component
            // returns false

            // PrefabUtility.IsAnyPrefabInstanceRoot(component.gameObject);
            // PrefabUtility.IsOutermostPrefabInstanceRoot(component.gameObject);
            // PrefabUtility.IsPartOfAnyPrefab(component.gameObject);
            // PrefabUtility.IsPartOfPrefabAsset(component.gameObject);
            // PrefabUtility.IsPartOfPrefabInstance(component.gameObject);
            // PrefabUtility.IsPartOfRegularPrefab(component.gameObject);
            // PrefabUtility.IsPartOfNonAssetPrefabInstance(component.gameObject);

            //var nearestRoot = PrefabUtility.GetNearestPrefabInstanceRoot(component);
            //var outerRoot = PrefabUtility.GetOutermostPrefabInstanceRoot(component);

            //var rootId = root.GetInstanceID();

            //var nearestId = nearestRoot.GetInstanceID();
            //var outerId = outerRoot.GetInstanceID();

            //var result = nearestId == outerId;

            //// Debug.Log($"isDirectly attached to root prefab? <b>{result}</b> c={component.gameObject.name}, root={root.gameObject.name}\n" 
            ////           + $"\n<b>nearest prefab ({nearestId}): </b>" + nearestRoot.name 
            ////           + $"\n<b>outer prefab ({outerId}): </b>" + outerRoot.name 
            ////           + $"\n\nroot instance ID={rootId}");

            //return result;

            var rootOf = PrefabUtility.GetPrefabAssetPathOfNearestInstanceRoot(root);
            var pathOfComponent = PrefabUtility.GetPrefabAssetPathOfNearestInstanceRoot(component);

            return rootOf.Equals(pathOfComponent);
        }

        public static bool IsAddedAsOverridenComponent(MonoBehaviour component)
        {
            var outermost = PrefabUtility.GetOutermostPrefabInstanceRoot(component);

            //// return !PrefabUtility.GetAddedComponents(outermost).Any(ac => ac.GetType() == component.GetType() && component.GetInstanceID() == ac.instanceComponent.GetInstanceID());

            //return PrefabUtility.IsAddedComponentOverride(component);

            return PrefabUtility.GetAddedComponents(outermost).Any(ac => component.GetInstanceID() == ac.instanceComponent.GetInstanceID());
        }


        public static bool IsRootOverridenProperties2(MonoBehaviour component, GameObject root, string[] properties)
        {
            var outermost = PrefabUtility.GetOutermostPrefabInstanceRoot(component);

            // var propertyOverrides = PrefabUtility.GetPropertyModifications(outermost)
            var objectOverrides = PrefabUtility.GetObjectOverrides(outermost)
                // .Where(modification => modification.instanceObject.GetType() == typeof(OpenUrl))
                .Where(modification => modification.instanceObject.GetInstanceID() == component.GetInstanceID());
            // .Where(modification => modification.target.GetType() == typeof(OpenUrl));
            // modification.target.GetInstanceID() == component.GetInstanceID() &&
            // properties.Contains(modification.propertyPath)
            // );

            var propsFormatted = string.Join("\n", objectOverrides.Select(modification => modification.instanceObject.GetType()));
            // Print("IsRoot overriding properties: " + propsFormatted);

            return objectOverrides.Any();
        }

        static string ParseAddedComponents(GameObject parent)
        {
            var addedComponents = PrefabUtility.GetAddedComponents(parent);

            var formattedAddedComponents = addedComponents.Where(FilterNecessaryComponents)
                .Select(ac => ac.instanceComponent.GetType() + " " + ac.instanceComponent.GetInstanceID() + " " + ac.instanceComponent.gameObject.GetInstanceID());

            return string.Join(", ", formattedAddedComponents);
        }

        private static bool FilterNecessaryComponents(AddedComponent arg)
        {
            return arg.instanceComponent.GetType() == typeof(OpenUrl);
        }
    }
}