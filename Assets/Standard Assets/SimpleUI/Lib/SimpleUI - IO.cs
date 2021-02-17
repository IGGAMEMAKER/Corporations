using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEditor;
using UnityEditor.Experimental.SceneManagement;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;
using System.Diagnostics;

namespace SimpleUI
{
    // Save/Load info
    public partial class SimpleUI
    {
        public bool isProjectScanned => SessionState.GetBool("isProjectScanned", false);

        List<SimpleUISceneType> _prefabs;
        internal Dictionary<string, List<UrlOpeningAttempt>> UrlOpeningAttempts;

        public List<SimpleUISceneType> prefabs
        {
            get
            {
                if (_prefabs == null || _prefabs.Count == 0)
                {
                    LoadData();
                }

                return _prefabs;
            }
        }

        public SimpleUISceneType GetPrefabByUrl(string url)
        {
            return prefabs.FirstOrDefault(p => p.Url.Equals(url));
        }


        // getting data
        public void ScanProject(bool forceLoad = false)
        {
            if ((!isProjectScanned || forceLoad) && isInstance)
            {
                BoldPrint("Scanning project (Loading assets & scripts)");

                SessionState.SetBool("isProjectScanned", true);

                // load prefabs and missing urls
                LoadData();

                var start = DateTime.Now;
                LoadScripts();

                var assetsEnd = DateTime.Now;
                LoadAssets();

                BoldPrint($"Loaded assets & scripts in {Measure(start)} (assets: {Measure(assetsEnd)}, code: {Measure(start, assetsEnd)})");
            }
        }


        void LoadScripts()
        {
            if (allScripts == null)
            {
                BoldPrint("Scripts are null");
            }

            allScripts = GetAllScripts();
        }

        void LoadAssets()
        {
            allAssetsWithOpenUrl = WhatUsesComponent();
            assetCount = allAssetsWithOpenUrl.Count();
        }

        void LoadReferences(string url)
        {
            referencesFromCode = WhichScriptReferencesConcreteUrl(url);
        }

        // File I/O
        internal void SaveData()
        {
            var fileName = "SimpleUI/SimpleUI.txt";
            var fileName2 = "SimpleUI/SimpleUI-MissingUrls.txt";

            Newtonsoft.Json.JsonSerializer serializer = new Newtonsoft.Json.JsonSerializer();
            serializer.Converters.Add(new Newtonsoft.Json.Converters.JavaScriptDateTimeConverter());
            serializer.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
            serializer.TypeNameHandling = Newtonsoft.Json.TypeNameHandling.Auto;
            serializer.Formatting = Newtonsoft.Json.Formatting.Indented;

            using (StreamWriter sw = new StreamWriter(fileName))
            using (Newtonsoft.Json.JsonWriter writer = new Newtonsoft.Json.JsonTextWriter(sw))
            {
                var entityData = _prefabs;
                //var entityData = prefabs; // new Dictionary<int, IComponent[]>();

                if (entityData.Count > 0)
                {
                    serializer.Serialize(writer, entityData);
                }
            }


            using (StreamWriter sw = new StreamWriter(fileName2))
            using (Newtonsoft.Json.JsonWriter writer = new Newtonsoft.Json.JsonTextWriter(sw))
            {
                var data = UrlOpeningAttempts;

                if (data.Count() > 0)
                {
                    serializer.Serialize(writer, data);
                }
            }
        }

        void LoadData()
        {
            BoldPrint("Read SimpleUI.txt");

            _prefabs = GetPrefabsFromFile();
            UrlOpeningAttempts = GetFailedUrlOpenings();
        }

        public static Newtonsoft.Json.JsonSerializerSettings settings => new Newtonsoft.Json.JsonSerializerSettings
        {
            TypeNameHandling = Newtonsoft.Json.TypeNameHandling.Auto,
            NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore,
        };

        public static Dictionary<string, List<UrlOpeningAttempt>> GetFailedUrlOpenings()
        {
            var missingUrls = "SimpleUI/SimpleUI-MissingUrls.txt";

            var obj2 = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, List<UrlOpeningAttempt>>>(File.ReadAllText(missingUrls), settings);

            return obj2 ?? new Dictionary<string, List<UrlOpeningAttempt>>();
        }

        public static List<SimpleUISceneType> GetPrefabsFromFile()
        {
            var fileName = "SimpleUI/SimpleUI.txt";

            var obj = Newtonsoft.Json.JsonConvert.DeserializeObject<List<SimpleUISceneType>>(File.ReadAllText(fileName), settings);

            return obj ?? new List<SimpleUISceneType>();
        }

        public void UpdatePrefab(SimpleUISceneType prefab) => UpdatePrefab(prefab, ChosenIndex);
        public void UpdatePrefab(SimpleUISceneType prefab, int index)
        {
            if (!hasChosenPrefab)
                return;

            prefabs[index] = prefab;
            SaveData();
        }

        static string GetCallerName(int skipFrames)
        {
            skipFrames++;
            var frame = new StackFrame(skipFrames);

            if (frame.GetMethod().Name.Equals("get_SimpleUI"))
            {
                return GetCallerName(skipFrames + 1);
            }

            return $"{frame.GetMethod().Name}";
        }

        public static SimpleUI GetInstance()
        {
            //return instance;

            var time = DateTime.Now;
            var instances = GetAllInstances<SimpleUI>();

            //if (instances.Length == 0)
            //{
            //    throw new Exception("Create instance of SimpleUI ScriptableObject");
            //}
            //else
            {
                var callerName = GetCallerName(1);

                //Print("Loading Instance (" + instances.Count() + $") in {Measure(time)} method: " + callerName);


                return instances.First();
            }
        }

        public static T[] GetAllInstances<T>() where T : ScriptableObject
        {
            // https://answers.unity.com/questions/1425758/how-can-i-find-all-instances-of-a-scriptable-objec.html

            string[] guids = AssetDatabase.FindAssets("t:" + typeof(T).Name);  //FindAssets uses tags check documentation for more info
            T[] a = new T[guids.Length];

            for (int i = 0; i < guids.Length; i++)         //probably could get optimized 
            {
                string path = AssetDatabase.GUIDToAssetPath(guids[i]);
                a[i] = AssetDatabase.LoadAssetAtPath<T>(path);
            }

            return a;
        }
    }
}