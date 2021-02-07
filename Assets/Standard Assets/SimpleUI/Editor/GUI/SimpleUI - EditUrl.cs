using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using static SimpleUI.SimpleUI;

namespace SimpleUI
{
    // editing route mode
    public partial class SimpleUIEditor
    {
        static string searchUrl = "";
        static Vector2 searchScrollPosition = Vector2.zero;

        public bool renameUrlRecursively = true;
        public string newEditingUrl = "";

        public string newUrl => SimpleUI.instance.newUrl;
        public string newName => SimpleUI.instance.newName;
        public string newPath => SimpleUI.instance.newPath;

        void RenderChosenPrefab()
        {
            if (!isConcreteUrlChosen)
            {
                // pick concrete URL
                RenderUrlsWhichAreAttachedToSamePrefab();
            }
            else
            {
                if (isUrlEditingMode)
                {
                    if (isUrlRemovingMode)
                    {
                        RenderUrlRemovingMode();
                    }
                    else
                    {
                        RenderEditingPrefabMode();
                    }
                }
                else
                {
                    RenderLinkToEditing();
                }
            }
        }

        void RenderUrlsWhichAreAttachedToSamePrefab()
        {
            var chosenPrefab = prefabs[ChosenIndex];
            var samePrefabUrls = prefabs.Where(p => p.AssetPath.Equals(chosenPrefab.AssetPath));

            Label("Prefab " + chosenPrefab.Name + " is attached to these urls");
            Label("Choose proper one!");

            Space();
            RenderPrefabs(samePrefabUrls);
        }

        void SetNewUrl(string url)
        {
            instance.newUrl = url;
        }

        void SetNewPath(string path)
        {
            instance.newPath = path;
        }

        void SetNewName(string name)
        {
            instance.newName = name;
        }

        void RenderLinkToEditing()
        {
            var index = ChosenIndex;
            var prefab = prefabs[index];

            Label(prefab.Url);

            if (Button("Edit prefab"))
            {
                isUrlEditingMode = true;
                isUrlRemovingMode = false;

                SetNewUrl(prefab.Url);
                newEditingUrl = prefab.Url;

                SetNewPath(prefab.AssetPath);
                SetNewName(prefab.Name);
            }

            Space();
            RenderPrefabs();
        }

        #region Rename Url utils
        string WrapStringWithTwoSlashes(string str)
        {
            str = WrapStringWithLeftSlash(str);
            str = WrapStringWithRightSlash(str);

            return str;
        }

        string WrapStringWithLeftSlash(string str)
        {
            if (!str.StartsWith("/"))
                str = "/" + str;

            return str;
        }

        string WrapStringWithRightSlash(string str)
        {
            if (!str.EndsWith("/"))
                str = str + "/";

            return str;
        }

        string TrimSlashes(string str)
        {
            // no trimming in / route
            if (str.Equals("/"))
                return str;

            return str.TrimStart('/').TrimEnd('/');
        }


        string ReplaceUrlInCode(string text, string from, string to)
        {
            var txt = text;

            // a/b/c
            // ==
            // a/b/c/
            // ==
            // /a/b/c
            // ==
            // /a/b/c/

            //txt = txt.Replace(WrapStringWithTwoSlashes(from), WrapStringWithTwoSlashes(to)); // two slashes
            //txt = txt.Replace(WrapStringWithLeftSlash(from), WrapStringWithLeftSlash(to)); // left slashes
            //txt = txt.Replace(WrapStringWithRightSlash(from), WrapStringWithRightSlash(to)); // right slashes
            //txt = txt.Replace(TrimSlashes(from), TrimSlashes(to)); // no slashes

            return text.Replace(from, to);
        }

        string GetUrlFormattedToOpenUrl(OpenUrl component, string from, string to)
        {
            bool addedSlash = false;
            var formattedUrl = component.Url;

            if (!formattedUrl.StartsWith("/"))
            {
                addedSlash = true;
                formattedUrl = "/" + formattedUrl;
            }

            var newUrl2 = formattedUrl.Replace(from, to);

            if (addedSlash)
                newUrl2 = newUrl2.TrimStart('/');

            return newUrl2;
        }

        void RenameUrlInPrefab(OpenUrl component, string newFormattedUrl, PrefabMatchInfo match)
        {
            // https://forum.unity.com/threads/how-do-i-edit-prefabs-from-scripts.685711/#post-4591885
            using (var editingScope = new PrefabUtility.EditPrefabContentsScope(match.PrefabAssetPath))
            {
                var prefabRoot = editingScope.prefabContentsRoot;

                var prefabbedComponent = prefabRoot.GetComponentsInChildren<OpenUrl>(true)[match.ComponentID];
                prefabbedComponent.Url = newFormattedUrl;

                // https://forum.unity.com/threads/remove-all-missing-components-in-prefabs.897761/
                GameObjectUtility.RemoveMonoBehavioursWithMissingScript(prefabRoot);
            }
        }

        void RenameUrlInScene(OpenUrl component, string newFormattedUrl, GameObject asset)
        {
            // https://forum.unity.com/threads/scripted-scene-changes-not-being-saved.526453/

            component.Url = newFormattedUrl;

            EditorUtility.SetDirty(component);
            EditorSceneManager.SaveScene(asset.scene);
        }

        void RenameUrlInScript(UsageInfo match, string from, string to)
        {
            var script = AssetDatabase.LoadAssetAtPath<MonoScript>(match.ScriptName);

            var replacedText = ReplaceUrlInCode(script.text, from, to);

            StreamWriter writer = new StreamWriter(match.ScriptName, false);
            writer.Write(replacedText);
            writer.Close();
        }

        bool RenameUrl(string route, string from, string to, string finalURL)
        {
            var matches = WhatUsesComponent(route, allAssetsWithOpenUrl);
            var codeRefs = SimpleUI.instance.WhichScriptReferencesConcreteUrl(route);

            try
            {
                AssetDatabase.StartAssetEditing();

                Print($"Replacing {from} to {to} in {route}");

                Print("Rename in assets");
                foreach (var match in matches)
                {
                    if (match.IsNormalPartOfNestedPrefab)
                        continue;

                    //var asset = AssetDatabase.LoadAssetAtPath<GameObject>(match.PrefabAssetPath);
                    //var asset = AssetDatabase.OpenAsset(AssetDatabase.LoadMainAssetAtPath(match.PrefabAssetPath));

                    var asset = match.Asset;
                    var component = match.Component;


                    var newFormattedUrl = GetUrlFormattedToOpenUrl(component, from, to);

                    Debug.Log($"Renaming {component.Url} => {newFormattedUrl} on component {match.ComponentName} in {match.PrefabAssetPath}");


                    // saving changes
                    if (SimpleUI.isSceneAsset(match.PrefabAssetPath))
                    {
                        RenameUrlInScene(component, newFormattedUrl, asset);
                    }

                    if (SimpleUI.isPrefabAsset(match.PrefabAssetPath))
                    {
                        RenameUrlInPrefab(component, newFormattedUrl, match);
                    }
                }

                Print("Rename in code");
                foreach (var match in codeRefs)
                {
                    RenameUrlInScript(match, from, to);
                }
            }
            catch (Exception ex)
            {
                Debug.LogError("Error occured during renaming " + route);

                return false;
            }
            finally
            {
                //AssetDatabase.SaveAssets();
                AssetDatabase.StopAssetEditing();

                var prefab = SimpleUI.instance.GetPrefabByUrl(route);
                prefab.Url = finalURL;

                UpdatePrefab(prefab);
            }

            return true;
        }
        #endregion

        void RenderStatButtons(SimpleUISceneType pref)
        {
            Space();
            Space();
            if (pref.Usages > 0 && GUILayout.Button("Reset Counter"))
            {
                pref.Usages = 0;

                UpdatePrefab(pref);
            }

            var maxUsages = prefabs.Max(p => p.Usages);
            if (pref.Usages < maxUsages && GUILayout.Button("Prioritize"))
            {
                pref.Usages = maxUsages + 1;

                UpdatePrefab(pref);
            }
        }

        void UpdatePrefab(SimpleUISceneType pref)
        {
            SimpleUI.instance.UpdatePrefab(pref);
        }

        void RenderRenameUrlButton(SimpleUISceneType prefab)
        {
            Space();

            renameUrlRecursively = EditorGUILayout.ToggleLeft("Rename subroutes too", renameUrlRecursively);

            Space();
            if (renameUrlRecursively)
                EditorGUILayout.HelpBox("Renaming this url will lead to renaming these urls too...", MessageType.Warning);
            else
                EditorGUILayout.HelpBox("Will only rename THIS url", MessageType.Warning);

            List<string> RenamingUrls = new List<string>();
            List<string> RenamingCodeUrls = new List<string>();

            RenamingUrls.Add(prefab.Url);

            if (renameUrlRecursively)
            {
                Space();
                var subroutes = SimpleUI.instance.GetSubUrls(prefab.Url, true);

                foreach (var route in subroutes)
                {
                    RenamingUrls.Add(route.Url);
                }

                // render
                foreach (var route in RenamingUrls)
                {
                    BoldLabel(route);
                }
            }

            var phrase = renameUrlRecursively ? "Rename url & subUrls" : "Rename THIS url";

            var matches = WhatUsesComponent(newUrl, allAssetsWithOpenUrl);
            var referencesFromCode = SimpleUI.instance.WhichScriptReferencesConcreteUrl(prefab.Url);

            // references from prefabs & scenes
            var names = matches.Select(m => $"<b>{SimpleUI.GetPrettyAssetType(m.PrefabAssetPath)}</b> " + SimpleUI.GetTrimmedPath(m.PrefabAssetPath)).ToList();
            var routes = matches.Select(m => m.PrefabAssetPath).ToList();

            // references from code
            foreach (var occurence in referencesFromCode)
            {
                names.Add($"<b>Code</b> {SimpleUI.GetTrimmedPath(occurence.ScriptName)}");
                routes.Add(occurence.ScriptName);
            }

            Space();
            if (Button(phrase))
            {
                if (EditorUtility.DisplayDialog("Do you want to rename url " + prefab.Url, "This action will rename url and subUrls in X prefabs, Y scenes and Z script files.\n\nPRESS CANCEL IF YOU HAVE UNSAVED PREFAB OR SCENE OR CODE CHANGES", "Rename", "Cancel"))
                {
                    Print("Rename starts now!");

                    // start from grandchilds first
                    foreach (var url in RenamingUrls.OrderByDescending(u => u.Count(c => c.Equals('/'))))
                    {
                        Print("Rename URL " + url);
                        var finalURL = url.Replace(newUrl, newEditingUrl);
                        RenameUrl(url, newUrl, newEditingUrl, finalURL);

                        Print("----------------");
                    }
                }
            }

            //EditorUtility.DisplayProgressBar("Renaming url", "Info", UnityEngine.Random.Range(0, 1f));
        }

        void RenderEditingPrefabMode()
        {
            var index = ChosenIndex;
            var prefab = prefabs[index];

            Label(prefab.Url);

            if (Button("Go back"))
            {
                isUrlEditingMode = false;
            }

            var prevUrl = newUrl;
            var prevName = newName;
            var prevPath = newPath;

            Space();



            Label("Edit url");
            newEditingUrl = EditorGUILayout.TextField("Url", newEditingUrl);

            if (newEditingUrl.Length > 0)
            {
                SetNewName(EditorGUILayout.TextField("Name", newName));

                if (newName.Length > 0)
                {
                    SetNewPath(EditorGUILayout.TextField("Asset Path", newPath));
                }
            }



            // if data changed, save it
            if (!prevPath.Equals(newPath) || !prevName.Equals(newName))
            {
                //prefab.Url = newEditingUrl;
                prefab.Name = newName;
                prefab.AssetPath = newPath;

                UpdatePrefab(prefab);
            }

            // if Url changed, rename everything
            if (!newUrl.Equals(newEditingUrl))
            {
                RenderRenameUrlButton(prefab);
            }

            Space();
            RenderRootPrefab();
            RenderSubroutes();

            // TODO url or path?
            // opened another url
            if (!newPath.Equals(prevPath))
                return;

            RenderStatButtons(prefab);

            Space(450);
            if (GUILayout.Button("Remove URL"))
            {
                isUrlRemovingMode = true;
                //prefabs.RemoveAt(index);
                //SaveData();
            }
        }
    }
}