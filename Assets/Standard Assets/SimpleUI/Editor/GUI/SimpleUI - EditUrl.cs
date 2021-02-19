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
    public partial class SimpleUI
    {
        static string searchUrl = "";
        static Vector2 searchScrollPosition = Vector2.zero;

        bool renameUrlRecursively = true;
        string newEditingUrl = "";

        //public string newUrl => SimpleUI.newUrl;
        //public string newName => SimpleUI.newName;
        //public string newPath => SimpleUI.newPath;

        void SetNewUrl(string url)
        {
            newUrl = url;
        }

        void SetNewPath(string path)
        {
            newPath = path;
        }

        void SetNewName(string name)
        {
            newName = name;
        }

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

        void RenderLinkToEditing()
        {
            var index = ChosenIndex;
            var prefab = prefabs[index];

            Label(prefab.Url);

            Space();
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

            return text.Replace(from + "\"", to + "\"");//.Replace(from + "/", to);
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

        bool RenameUrlInScript(UsageInfo match, string from, string to) => RenameUrlInScript(match.ScriptName, from, to);
        bool RenameUrlInScript(string ScriptName, string from, string to)
        {
            var script = AssetDatabase.LoadAssetAtPath<MonoScript>(ScriptName);

            var txt = script.text;
            var replacedText = ReplaceUrlInCode(txt, from, to);

            bool textChanged = !txt.Equals(replacedText);

            if (!textChanged)
                return textChanged;

            StreamWriter writer = new StreamWriter(ScriptName, false);
            writer.Write(replacedText);
            writer.Close();

            return textChanged;
        }

        void AttachAnchorsToUrl()
        {
            foreach (var script in allScripts)
            {
                var path = script.Key;

                var candidates = prefabs
                    .Where(p => !p.Url.Equals("/"))
                    .OrderByDescending(u => u.Url.Count(c => c.Equals('/')));

                var renameCandidates = candidates.Select(p => p.Url).ToList();
                var futureCandidates = candidates.Select(url => $"simplelink:{url.Url}").ToList();
                //var futureCandidates = candidates.Select(url => $"simplelink:{url.ID}").ToList();

                bool success = RenameUrlsInScript(path, renameCandidates, futureCandidates);

                if (success)
                {
                    BoldPrint("Renamed Urls in " + path);
                }
            }
        }

        bool RenameUrlsInScript(string ScriptName, List<string> froms, List<string> tos)
        {
            var script = AssetDatabase.LoadAssetAtPath<MonoScript>(ScriptName);

            var txt = script.text;
            var replacedText = txt;

            for (var i = 0; i < froms.Count; i++)
            {
                replacedText = ReplaceUrlInCode(replacedText, froms[i], tos[i]);
            }

            var simplelink = "simplelink:";
            var duplicateLink = $"{simplelink}{simplelink}";

            while (replacedText.Contains(duplicateLink))
            {
                replacedText = replacedText.Replace(duplicateLink, simplelink);
            }

            bool textChanged = !txt.Equals(replacedText);

            if (!textChanged)
                return textChanged;

            StreamWriter writer = new StreamWriter(ScriptName, false);
            writer.Write(replacedText);
            writer.Close();

            return textChanged;
        }

        void RenameAssetsOld()
        {
            /*
foreach (var match in matches)
{
    if (match.IsNormalPartOfNestedPrefab)
        continue;

    //var asset = AssetDatabase.LoadAssetAtPath<GameObject>(match.PrefabAssetPath);
    //var asset = AssetDatabase.OpenAsset(AssetDatabase.LoadMainAssetAtPath(match.PrefabAssetPath));

    var asset = match.Asset;
    var component = match.Component;


    var newFormattedUrl = GetUrlFormattedToOpenUrl(component, from, to);

    Print($"Renaming {component.Url} => {newFormattedUrl} on component {match.ComponentName} in {match.PrefabAssetPath}");


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
*/
        }

        bool RenameUrl(string route, string from, string to, string finalURL, List<string> changedScripts)
        {
            //var matches = WhatUsesComponent(route, GetAllAssetsWithOpenUrl());
            var codeRefs = WhichScriptReferencesConcreteUrl(route);

            var assets = allAssetsWithOpenUrl; // GetAllAssetsWithOpenUrl();

            try
            {
                Print($"Replacing {from} to {to} in {route}");

                AssetDatabase.StartAssetEditing();

                Print("Rename in assets " + assets.Count);
                for (var i = 0; i < assets.Count; i++)
                {
                    var match = assets[i];
                    //Print("Checking match " + match.URL);

                    if (match.URL.Equals(route.TrimStart('/')))
                    {
                        BoldPrint("Renaming Asset MATCH!! " + route);

                        match.URL = finalURL.TrimStart('/');
                        assets[i] = match;
                    }
                }

                Print("Rename in code");
                foreach (var match in codeRefs)
                {
                    RenameUrlInScript(match, from, to);

                    changedScripts.Add(match.ScriptName);
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

                BoldPrint($"Trying to update prefab data: {route} => {finalURL}");
                var index = prefabs.FindIndex(p => p.Url.Equals(route));

                var prefab = GetPrefabByUrl(route);
                prefab.Url = finalURL;

                UpdatePrefab(prefab, index);

                SavePrefabMatches(assets);
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

        //void UpdatePrefab(SimpleUISceneType pref)
        //{
        //    SimpleUI.UpdatePrefab(pref);
        //}

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
                var subroutes = GetSubUrls(prefab.Url, true);

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

            var matches = WhatUsesComponent(newUrl, GetAllAssetsWithOpenUrl());
            var referencesFromCode = WhichScriptReferencesConcreteUrl(prefab.Url);

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
                    var prevUrl = newUrl;

                    var changedScripts = new List<string>();

                    // start from grandchilds first
                    foreach (var url in RenamingUrls.OrderByDescending(u => u.Count(c => c.Equals('/'))))
                    {
                        Print("Rename URL " + url);

                        var finalURL = url.Replace(newUrl, newEditingUrl);
                        RenameUrl(url, newUrl, newEditingUrl, finalURL, changedScripts);

                        Print("----------------");
                    }

                    OpenPrefabByUrl(newEditingUrl);
                    if (changedScripts.Any())
                    {
                        OpenAsset(changedScripts.First());
                    }

                    //newUrl = newEditingUrl;
                    //newUrl = prevUrl.Replace(newUrl, newEditingUrl);
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