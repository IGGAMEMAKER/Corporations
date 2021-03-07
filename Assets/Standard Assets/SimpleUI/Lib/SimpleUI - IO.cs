using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEditor;
using System.Diagnostics;

namespace SimpleUI
{
    // Save/Load info
    public partial class SimpleUI
    {
        // https://stackoverflow.com/questions/50800690/prevent-losing-data-for-editorwindow-in-dll-when-editor-recompiles

        public bool isProjectScanned => SessionState.GetBool("isProjectScanned", false);
        public void MarkProjectAsScanned() => SessionState.SetBool("isProjectScanned", true);

        const string PATH_CountableAssets = "SimpleUI/CountableAssets.txt";
        const string PATH_SimpleUI = "SimpleUI/SimpleUI.txt";
        const string PATH_MissingAssets = "SimpleUI/SimpleUI-MissingUrls.txt";
        const string PATH_Matches = "SimpleUI/SimpleUI-matches.txt";

        List<CountableAsset> _assets;
        public List<CountableAsset> countableAssets
        {
            get
            {
                if (_assets == null)
                    LoadCountableAssets();

                return _assets;
            }
        }

        List<SimpleUISceneType> _prefabs = new List<SimpleUISceneType>();
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

        [SerializeField]
        List<PrefabMatchInfo> allAssetsWithOpenUrl;
        public List<PrefabMatchInfo> GetAllAssetsWithOpenUrl()
        {
            if (allAssetsWithOpenUrl == null)
            {
                LoadAssets();
            }

            return allAssetsWithOpenUrl; // new List<PrefabMatchInfo>();// => SimpleUI.allAssetsWithOpenUrl;
        }

        public Dictionary<string, MonoScript> allScripts = new Dictionary<string, MonoScript>();

        // refs to concrete url
        public List<UsageInfo> referencesFromCode = new List<UsageInfo>();

        public SimpleUISceneType GetPrefabByGuid(string guid)
        {
            return prefabs.FirstOrDefault(p => p.ID.Equals(guid));
        }
        public SimpleUISceneType GetPrefabByUrl(string url)
        {
            return prefabs.FirstOrDefault(p => p.Url.Equals(url));
        }



        // getting data
        public void ScanProject(bool forceLoad = false)
        {
            if (!isProjectScanned || forceLoad)
            {
                BoldPrint("Scanning project (Loading assets & scripts)");

                MarkProjectAsScanned();

                LoadCountableAssets();
                // FullAssetScan();
            }
        }

        void FullAssetScan()
        {
            // load prefabs and missing urls
            LoadData();

            var start = DateTime.Now;
            LoadScripts();

            var assetsEnd = DateTime.Now;

            // scanning all assets
            var matches = WhatUsesComponent();

            // saving matches
            SavePrefabMatches(matches);

            // restoring from file
            LoadAssets();

            BoldPrint($"Loaded assets & scripts in {Measure(start)} (assets: {Measure(assetsEnd)}, code: {Measure(start, assetsEnd)})");
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
            allAssetsWithOpenUrl = GetPrefabMatchesFromFile();
        }

        void LoadReferences(string url)
        {
            referencesFromCode = WhichScriptReferencesConcreteUrl(url);
        }

        static void SaveUrlOpeningAttempts(Dictionary<string, List<UrlOpeningAttempt>> data)
        {
            var fileName2 = PATH_MissingAssets;

            Newtonsoft.Json.JsonSerializer serializer = new Newtonsoft.Json.JsonSerializer();
            serializer.Converters.Add(new Newtonsoft.Json.Converters.JavaScriptDateTimeConverter());
            serializer.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
            serializer.TypeNameHandling = Newtonsoft.Json.TypeNameHandling.Auto;
            serializer.Formatting = Newtonsoft.Json.Formatting.Indented;

            using (StreamWriter sw = new StreamWriter(fileName2))
            using (Newtonsoft.Json.JsonWriter writer = new Newtonsoft.Json.JsonTextWriter(sw))
            {
                if (data.Count() > 0)
                {
                    serializer.Serialize(writer, data);
                }
            }
        }

        static void SavePrefabs(List<SimpleUISceneType> entityData)
        {
            var fileName = PATH_SimpleUI;

            Newtonsoft.Json.JsonSerializer serializer = new Newtonsoft.Json.JsonSerializer();
            serializer.Converters.Add(new Newtonsoft.Json.Converters.JavaScriptDateTimeConverter());
            serializer.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
            serializer.TypeNameHandling = Newtonsoft.Json.TypeNameHandling.Auto;
            serializer.Formatting = Newtonsoft.Json.Formatting.Indented;

            using (StreamWriter sw = new StreamWriter(fileName))
            using (Newtonsoft.Json.JsonWriter writer = new Newtonsoft.Json.JsonTextWriter(sw))
            {
                if (entityData.Count > 0)
                {
                    serializer.Serialize(writer, entityData);
                }
            }
        }

        static void SaveCountableAssets(List<CountableAsset> entityData)
        {
            var fileName = PATH_CountableAssets;

            Newtonsoft.Json.JsonSerializer serializer = new Newtonsoft.Json.JsonSerializer();
            serializer.Converters.Add(new Newtonsoft.Json.Converters.JavaScriptDateTimeConverter());
            serializer.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
            serializer.TypeNameHandling = Newtonsoft.Json.TypeNameHandling.Auto;
            serializer.Formatting = Newtonsoft.Json.Formatting.Indented;

            using (StreamWriter sw = new StreamWriter(fileName))
            using (Newtonsoft.Json.JsonWriter writer = new Newtonsoft.Json.JsonTextWriter(sw))
            {
                if (entityData.Count > 0)
                {
                    serializer.Serialize(writer, entityData);
                }
            }
        }

        static void SavePrefabMatches(List<PrefabMatchInfo> data)
        {
            var fileName = PATH_Matches;

            Newtonsoft.Json.JsonSerializer serializer = new Newtonsoft.Json.JsonSerializer();
            serializer.Converters.Add(new Newtonsoft.Json.Converters.JavaScriptDateTimeConverter());
            serializer.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
            serializer.TypeNameHandling = Newtonsoft.Json.TypeNameHandling.Auto;
            serializer.Formatting = Newtonsoft.Json.Formatting.Indented;

            using (StreamWriter sw = new StreamWriter(fileName))
            using (Newtonsoft.Json.JsonWriter writer = new Newtonsoft.Json.JsonTextWriter(sw))
            {
                if (data.Count > 0)
                {
                    serializer.Serialize(writer, data);
                }
            }
        }

        // File I/O
        internal void SaveData()
        {
            SavePrefabs(_prefabs);
            SaveUrlOpeningAttempts(UrlOpeningAttempts);
        }

        void LoadData()
        {
            BoldPrint("Read SimpleUI.txt");

            _prefabs = GetPrefabsFromFile();
            UrlOpeningAttempts = GetUrlOpeningAttempts();
        }

        void LoadCountableAssets()
        {
            _assets = GetCountableAssetsFromFile();
        }

        public static Newtonsoft.Json.JsonSerializerSettings settings => new Newtonsoft.Json.JsonSerializerSettings
        {
            TypeNameHandling = Newtonsoft.Json.TypeNameHandling.Auto,
            NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore,
        };

        public static Dictionary<string, List<UrlOpeningAttempt>> GetUrlOpeningAttempts()
        {
            var missingUrls = PATH_MissingAssets;

            var obj2 = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, List<UrlOpeningAttempt>>>(File.ReadAllText(missingUrls), settings);

            return obj2 ?? new Dictionary<string, List<UrlOpeningAttempt>>();
        }

        public static List<SimpleUISceneType> GetPrefabsFromFile()
        {
            var fileName = PATH_SimpleUI;

            var obj = Newtonsoft.Json.JsonConvert.DeserializeObject<List<SimpleUISceneType>>(File.ReadAllText(fileName), settings);

            return obj ?? new List<SimpleUISceneType>();
        }

        public static List<CountableAsset> GetCountableAssetsFromFile()
        {
            var fileName = PATH_CountableAssets;
            var fullDirectory = fileName; // Directory.GetCurrentDirectory() + fileName;

            if (!File.Exists(fullDirectory))
            {
                var fs = File.Create(fullDirectory);
                fs.Close();
            }

            var obj = Newtonsoft.Json.JsonConvert.DeserializeObject<List<CountableAsset>>(File.ReadAllText(fileName), settings);

            return obj ?? new List<CountableAsset>();
        }

        public static List<PrefabMatchInfo> GetPrefabMatchesFromFile()
        {
            var fileName = PATH_Matches;

            var obj = Newtonsoft.Json.JsonConvert.DeserializeObject<List<PrefabMatchInfo>>(File.ReadAllText(fileName), settings);

            return obj ?? new List<PrefabMatchInfo>();
        }

        public void UpdatePrefab(SimpleUISceneType prefab) => UpdatePrefab(prefab, ChosenIndex);
        public void UpdatePrefab(SimpleUISceneType prefab, int index)
        {
            if (!hasChosenPrefab)
                return;

            prefabs[index] = prefab;
            SavePrefabs(prefabs);
        }

        public void UpdateCountableAsset(CountableAsset asset, int index)
        {
            countableAssets[index] = asset;
            SaveCountableAssets(countableAssets);
        }

        public static void StaticUpdatePrefab(SimpleUISceneType prefab, int index)
        {
            var prefs = GetPrefabsFromFile();

            prefs[index] = prefab;
            SavePrefabs(prefs);
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
            var w = GetWindow<SimpleUI>("Simple UI", false);

            w.ScanProject();

            return w;

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