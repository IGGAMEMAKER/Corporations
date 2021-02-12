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
    // Scripts, attached to prefab
    public partial class SimpleUI
    {
        void RenderScriptsAttachedToThisPrefab(SimpleUISceneType p)
        {
            var GO = Selection.activeObject as GameObject;
            var scripts = new Dictionary<string, int>();

            RenderScriptsAttachedToThisGameObject(GO.transform, ref scripts);

            Debug.Log("Scripts, attached to PREFAB");

            foreach (var s in scripts)
                Debug.Log(s.Key);
        }

        void RenderScriptsAttachedToThisGameObject(Transform GO, ref Dictionary<string, int> scripts)
        {
            foreach (Transform go in GO.transform)
            {
                foreach (Component c in go.GetComponents<Component>())
                {
                    string name = ObjectNames.GetInspectorTitle(c);
                    if (name.EndsWith("(Script)"))
                    {
                        string formated = name.Replace("(Script)", String.Empty).Replace(" ", String.Empty) + ".cs";
                        scripts[formated] = 1;
                    }
                }

                RenderScriptsAttachedToThisGameObject(go, ref scripts);
            }
        }
    }


    // what uses component OpenUrl
    public partial class SimpleUI
    {
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

        public static bool HasOverridenProperties(MonoBehaviour component, string[] properties)
        {
            var result = PrefabUtility.HasPrefabInstanceAnyOverrides(component.gameObject, false);


            var overrides = PrefabUtility.GetObjectOverrides(component.gameObject);

            // var wat = overrides.First().coupledOverride.GetAssetObject();
            // Debug.Log("first override " + wat);

            var nearestPrefab = PrefabUtility.GetCorrespondingObjectFromSource(component);

            // Debug.Log($"Check overrides for component {component.gameObject.name}");

            foreach (var modification in PrefabUtility.GetPropertyModifications(component))
            {
                if (modification.propertyPath.Contains("m_"))
                    continue;

                if (properties.Contains(modification.propertyPath))
                    return true;
                // Debug.Log($"Property: {modification.propertyPath}");
                // Debug.Log($"Value: {modification.value}");
                // Debug.Log(modification.target);
            }

            //Debug.Log("Corresponding object for " + component.gameObject.name + " is " + nearestPrefab.name);

            //var str = result ? "HAS" : "Has NO";

            //// PrintArbitraryInfo(null, $"{component.gameObject.name} {str} overrides"); // ({root.gameObject.name})

            //return result;

            return false;
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

        // path - RootAssetPath
        public static PrefabMatchInfo GetPrefabMatchInfo2(MonoBehaviour component, GameObject asset, string path, string[] properties)
        {
            var matchingComponent = new PrefabMatchInfo { PrefabAssetPath = path, ComponentName = component.gameObject.name };
            matchingComponent.URL = (component as OpenUrl).Url;
            matchingComponent.Asset = asset;
            matchingComponent.Component = component as OpenUrl;


            bool isAttachedToRootPrefab = HasNoPrefabsBetweenObjects(component, asset);
            bool isAttachedToNestedPrefab = !isAttachedToRootPrefab;

            if (isAttachedToRootPrefab)
            {
                // directly appears in prefab
                // so you can upgrade it and safely save ur prefab

                matchingComponent.IsDirectMatch = true;
                Print2($"Directly occurs as {matchingComponent.ComponentName} in {matchingComponent.PrefabAssetPath} {matchingComponent.URL}");
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
                    Print2(
                        $"{matchingComponent.ComponentName} Is <b>ATTACHED</b> to some nested prefab\n\nouter={outer.name} {ParseAddedComponents(outer)}, {component.GetInstanceID()}");
                }

                if (isPartOfNestedPrefab)
                {
                    // so you might need to check Overriden Properties

                    if (IsRootOverridenProperties2(component, asset, properties))
                    {
                        // update property and just save root prefab

                        matchingComponent.IsOverridenAsComponentProperty = true;
                        Print2($"Root <b>OVERRIDES VALUES</b> on {matchingComponent.ComponentName}");
                    }
                    else
                    {
                        // you will upgrade value in it's own prefab
                        // no actions are necessary for root prefab

                        matchingComponent.IsNormalPartOfNestedPrefab = true;
                        Print2($"{matchingComponent.ComponentName} is <b>part of a nested prefab</b>");
                    }
                }
            }

            return matchingComponent;
        }

        public static void PrintMatchInfo(IEnumerable<PrefabMatchInfo> matches)
        {
            foreach (var matchingComponent in matches)
            {
                if (matchingComponent.IsDirectMatch)
                {
                    // directly appears in prefab
                    // so you can upgrade it and safely save ur prefab

                    Print($"Directly occurs as {matchingComponent.ComponentName} in {matchingComponent.PrefabAssetPath}");
                }
                else
                {
                    // appears somewhere in nested prefabs

                    if (matchingComponent.IsOverridenAsAddedComponent)
                    {
                        // is added by root component

                        Print($"{matchingComponent.ComponentName} Is <b>ATTACHED BY ROOT</b> to some nested prefab\n");
                    }
                    else
                    {
                        // is part of nested prefab
                        if (matchingComponent.IsOverridenAsComponentProperty)
                        {
                            Print($"Root <b>OVERRIDES VALUES</b> on {matchingComponent.ComponentName}");
                        }

                        if (matchingComponent.IsNormalPartOfNestedPrefab)
                        {
                            Print($"{matchingComponent.ComponentName} is <b>part of a nested prefab</b>");
                        }
                    }
                }
            }
        }



        public static List<PrefabMatchInfo> WhatUsesComponent(string url, List<PrefabMatchInfo> matchInfos)
        {
            return matchInfos.Where(m => m.URL.Equals(url.TrimStart('/'))).ToList();
        }
        //public static List<PrefabMatchInfo> WhatUsesComponent(string url)
        //{
        //    var matches = WhatUsesComponent<OpenUrl>();

        //    return WhatUsesComponent(url, matches);
        //        //.Where(a => a.URL.Equals(url.TrimStart('/')))
        //        //.ToList();
        //}

        /// <summary>
        /// Only works if T is OpenUrl
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static List<PrefabMatchInfo> WhatUsesComponent<T>()
        {
            var typeToSearch = typeof(T);

            Debug.Log("Finding all Prefabs and scenes that have the component" + typeToSearch + "…");

            var excludeFolders = new[] { "Assets/Standard Assets" };
            var guids = AssetDatabase.FindAssets("t:scene t:prefab", new[] { "Assets" });

            var paths = guids.Select(AssetDatabase.GUIDToAssetPath).ToList();
            paths.RemoveAll(guid => excludeFolders.Any(guid.Contains));

            List<PrefabMatchInfo> matchingComponents = new List<PrefabMatchInfo>();

            var properties = new[] { "Url" };

            // save state
            List<Scene> scenes = new List<Scene>();

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
            else
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

        public struct UsageInfo
        {
            public string ScriptName;
        }

        /// <summary>
        /// returns array of matching indicies
        /// </summary>
        /// <param name="text"></param>
        /// <param name="url"></param>
        /// <param name="directMatch"></param>
        /// <returns></returns>
        //public static int[] GetUrlMatchesInText(string text, string url, bool directMatch)
        //{

        //}
        public static bool IsTextContainsUrl(string text, string url, bool directMatch)
        {
            //bool directMatch = true;
            var searchString = '"' + url;

            if (directMatch)
                searchString += '"';

            return text.Contains(searchString);
        }

        public static Dictionary<string, MonoScript> GetAllScripts()
        {
            var dict = new Dictionary<string, MonoScript>();

            foreach (var path in GetAllScriptPaths())
            {
                var asset = AssetDatabase.LoadAssetAtPath<MonoScript>(path);

                dict[path] = asset;
            }

            return dict;
        }

        public static List<string> GetAllScriptPaths()
        {
            var directory = "Assets/";

            var excludeFolders = new[] { "Assets/Standard Assets/Frost UI", "Assets/Standard Assets/SimpleUI", "Assets/Standard Assets/Libraries", "Assets/Systems", "Assets/Core" };
            var guids = AssetDatabase.FindAssets("t:Script", new[] { "Assets" });

            //Debug.Log($"Found {guids.Length} scripts");
            var paths = guids.Select(AssetDatabase.GUIDToAssetPath).ToList();

            paths.RemoveAll(guid => excludeFolders.Any(guid.Contains));

            return paths;
        }

        public List<UsageInfo> WhichScriptReferencesConcreteUrl(string url)
        {
            // Debug.Log("Finding all scrips, that call " + url);

            var list = new List<UsageInfo>();

            //foreach (var path in GetAllScriptPaths())
            foreach (var script in allScripts)
            {
                //var asset = AssetDatabase.LoadAssetAtPath<MonoScript>(path);
                var asset = script.Value;
                var path = script.Key;


                var txt = asset != null ? ("\n" + asset.text) : "";

                //if (asset == null)
                //{
                //    Debug.LogError("Cannot load prefab at path: " + path);

                //    continue;
                //}

                if (IsTextContainsUrl(txt, url, true))
                {
                    // Debug.Log($"Found url {url} in text " + path);

                    list.Add(new UsageInfo { ScriptName = path });
                }
            }

            return list;
        }

        // not used
        public static bool IsRootOverridenProperties(MonoBehaviour component, GameObject root, string[] properties)
        {
            var fastFilter = new Func<PropertyModification, bool>(p => properties.Contains(p.propertyPath));
            var print = new Func<IEnumerable<PropertyModification>, string>(p => string.Join(", ", p.Select(pp => pp.propertyPath).ToList()));

            var outermost = PrefabUtility.GetOutermostPrefabInstanceRoot(component);
            var outermostPropertyChanges = PrefabUtility.GetPropertyModifications(outermost).Where(fastFilter);

            var outermostPath = AssetDatabase.GetAssetPath(outermost);
            var outermostAsset = AssetDatabase.LoadMainAssetAtPath(outermostPath);

            // var objectOverrides = PrefabUtility.GetObjectOverrides(component.gameObject).Where(change => change.instanceObject.GetType() == typeof(OpenUrl));
            // var propertyChanges = PrefabUtility.GetPropertyModifications(component.gameObject).Where(p => !p.propertyPath.Contains("m_") && properties.Contains(p.propertyPath));
            // PrefabUtility.HasPrefabInstanceAnyOverrides()
            var propertyChanges = PrefabUtility.GetPropertyModifications(component.gameObject).Where(fastFilter);

            var text = $"Outermost changes {outermost.name} hasAnyChanges={PrefabUtility.HasPrefabInstanceAnyOverrides(outermost, false)}, {outermostPath}\n";
            text += print(outermostPropertyChanges);
            text += "\nComponent changes\n";
            text += print(propertyChanges);

            Debug.Log(text);

            return propertyChanges.Any();
        }

        // not used
        public static PrefabMatchInfo GetPrefabMatchInfo(MonoBehaviour component, GameObject root, string path, string[] matchingProperties)
        {
            var matchInfo = new PrefabMatchInfo { PrefabAssetPath = path, ComponentName = component.gameObject.name };
            return matchInfo;
            string text;


            var objectOverrides = PrefabUtility.GetObjectOverrides(component.gameObject)
                .Where(change => change.instanceObject.GetType() == typeof(OpenUrl));
            var propertyChanges = PrefabUtility.GetPropertyModifications(component.gameObject)
                .Where(p => !p.propertyPath.Contains("m_"));

            var parent = PrefabUtility.GetCorrespondingObjectFromSource(component.gameObject);
            var nearest = PrefabUtility.GetNearestPrefabInstanceRoot(component);
            var outermost = PrefabUtility.GetOutermostPrefabInstanceRoot(component);

            var selfAddedComponents = ParseAddedComponents(component.gameObject);
            var parentAddedComponents = ParseAddedComponents(parent);
            var nearestAddedComponents = ParseAddedComponents(nearest);
            var outermostAddedComponents = ParseAddedComponents(outermost);

            var urlChanges = propertyChanges
                    // .Where(p => p.target.GetInstanceID() == c.GetInstanceID())
                    .Where(p => matchingProperties.Contains(p.propertyPath))
                // .Where(p => p.target.GetType() == typeof(OpenUrl))
                ;

            if (urlChanges.Any())
                text = $"<b>HAS</b> {urlChanges.Count()} URL overrides of {component.gameObject.name} in {root.name}";
            else
                text =
                    $"<b>NO</b> url overrides of {component.gameObject.name} in {root.name}, while propertyChanges={propertyChanges.Count()}";

            var concatObjectOverrides = string.Join(", \n",
                objectOverrides.Select(change =>
                    (change.instanceObject.name + " (" + change.instanceObject.name + ")")));

            text += "\n\n" + $"({objectOverrides.Count()}) Object Overrides on: {component.gameObject.name}" + "\n\n" +
                    concatObjectOverrides;

            text += $"\n\nAdded Components self={component.gameObject}\n({selfAddedComponents})";
            text += $"\n\nAdded Components parent={parent}\n({parentAddedComponents})";
            text += $"\n\nAdded Components nearest={nearest}\n({nearestAddedComponents})";
            text += $"\n\nAdded Components outermost={outermost}\n({outermostAddedComponents})";

            Debug.Log(text);

            // PrintBlah(null, $"<b>NO</b> url overrides of {component.gameObject.name} in {root.name}. propertyChanges={urlChanges.Count()} hasOverrides=<b>{hasOverrides}</b>");


            // var c = component.gameObject;
            // var routeToRoot = new List<GameObject>();
            //
            // routeToRoot.Add(c);
            //
            // int counter = 0;
            // while (c.transform.parent != null && counter < 10)
            // {
            //     c = c.transform.parent.gameObject; 
            //     
            //     routeToRoot.Add(c);
            //
            //     counter++;
            // }
            //
            // if (counter == 10)
            // {
            //     PrintBlah(null, "<B>OVERFLOW</B>");
            // }
            // else
            // {
            //     routeToRoot.Reverse();
            //     foreach (var o in routeToRoot)
            //     {
            //         bool isRoot = root.GetInstanceID() == o.GetInstanceID();
            //         bool isPrefabSelf = PrefabUtility.IsAnyPrefabInstanceRoot(o);
            //         bool isPrefabVariantSelf = PrefabUtility.IsPartOfVariantPrefab(o);
            //
            //         var propertyChanges = PrefabUtility.GetPropertyModifications(o).ToList()
            //             .Where(p => !p.propertyPath.Contains("m_"))
            //             .Where(p => properties.Contains(p.propertyPath))
            //             .Where(p => p.target.GetType() == typeof(OpenUrl));
            //
            //         bool hasOverrides = false;
            //         
            //         PrintBlah(null, $"{o.name} - {o.GetInstanceID()}. isRoot={isRoot}, isPrefab={isPrefabSelf}, hasOverrides={hasOverrides}");
            //     }
            // }

            return matchInfo;
        }
    }
}