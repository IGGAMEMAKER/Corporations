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
    // Save/Load info
    public partial class SimpleUI
    {
        static List<SimpleUISceneType> _prefabs;
        internal static Dictionary<string, List<UrlOpeningAttempt>> UrlOpeningAttempts;

        public static List<SimpleUISceneType> prefabs
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

        public static SimpleUISceneType GetPrefabByUrl(string url)
        {
            return prefabs.FirstOrDefault(p => p.Url.Equals(url));
        }



        // getting data
        static void LoadScripts()
        {
            allScripts = GetAllScripts();

        }
        static void LoadAssets()
        {
            allAssetsWithOpenUrl = WhatUsesComponent<OpenUrl>();
        }

        static void LoadReferences(string url)
        {
            referencesFromCode = WhichScriptReferencesConcreteUrl(url);
        }

        // File I/O
        internal static void SaveData()
        {
            var fileName = "SimpleUI/SimpleUI.txt";
            var fileName2 = "SimpleUI/SimpleUI-MissingUrls.txt";

            Newtonsoft.Json.JsonSerializer serializer = new Newtonsoft.Json.JsonSerializer();
            serializer.Converters.Add(new Newtonsoft.Json.Converters.JavaScriptDateTimeConverter());
            serializer.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
            serializer.TypeNameHandling = Newtonsoft.Json.TypeNameHandling.Auto;
            serializer.Formatting = Newtonsoft.Json.Formatting.Indented;

            var entityData = _prefabs;
            //var entityData = prefabs; // new Dictionary<int, IComponent[]>();

            using (StreamWriter sw = new StreamWriter(fileName))
            using (Newtonsoft.Json.JsonWriter writer = new Newtonsoft.Json.JsonTextWriter(sw))
            {
                if (entityData.Count > 0)
                {
                    serializer.Serialize(writer, entityData);
                }
            }

            var data = UrlOpeningAttempts;

            using (StreamWriter sw = new StreamWriter(fileName2))
            using (Newtonsoft.Json.JsonWriter writer = new Newtonsoft.Json.JsonTextWriter(sw))
            {
                if (data.Count() > 0)
                {
                    serializer.Serialize(writer, data);
                }
            }
        }

        static void LoadData()
        {
            //if (prefabs != null && prefabs.Count == 0)
            //    return;

            BoldPrint("Read SimpleUI.txt");

            var fileName = "SimpleUI/SimpleUI.txt";
            var missingUrls = "SimpleUI/SimpleUI-MissingUrls.txt";

            var settings = new Newtonsoft.Json.JsonSerializerSettings
            {
                TypeNameHandling = Newtonsoft.Json.TypeNameHandling.Auto,
                NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore,
            };

            var obj = Newtonsoft.Json.JsonConvert.DeserializeObject<List<SimpleUISceneType>>(File.ReadAllText(fileName),
                settings);
            var obj2 =
                Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, List<UrlOpeningAttempt>>>(
                    File.ReadAllText(missingUrls), settings);

            _prefabs = obj ?? new List<SimpleUISceneType>();
            UrlOpeningAttempts = obj2 ?? new Dictionary<string, List<UrlOpeningAttempt>>();
        }

        public static void UpdatePrefab(SimpleUISceneType prefab) => UpdatePrefab(prefab, ChosenIndex);

        public static void UpdatePrefab(SimpleUISceneType prefab, int index)
        {
            if (!hasChosenPrefab)
                return;

            prefabs[index] = prefab;
            SaveData();
        }

        public static void TryToIncreaseCurrentPrefabCounter()
        {
            if (hasChosenPrefab)
            {
                var pref = prefabs[ChosenIndex];

                pref.Usages++;
                pref.LastOpened = DateTime.Now.Ticks;

                UpdatePrefab(pref);
            }
        }
    }
}