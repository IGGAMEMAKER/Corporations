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
        private bool isProjectScanned = false;

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
        internal void ScanProject()
        {
            if (!isProjectScanned)
            {
                BoldPrint("Loading assets & scripts");

                var start = DateTime.Now;

                LoadAssets();

                var assetsEnd = DateTime.Now;

                LoadScripts();

                BoldPrint($"Loaded assets & scripts in {Measure(start)} (assets: {Measure(start, assetsEnd)}, code: {Measure(assetsEnd)})");

                isProjectScanned = true;
            }
        }

        void LoadScripts()
        {
            allScripts = GetAllScripts();

        }
        void LoadAssets()
        {
            allAssetsWithOpenUrl = WhatUsesComponent<OpenUrl>();
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

        public void UpdatePrefab(SimpleUISceneType prefab) => UpdatePrefab(prefab, ChosenIndex);

        public void UpdatePrefab(SimpleUISceneType prefab, int index)
        {
            if (!hasChosenPrefab)
                return;

            prefabs[index] = prefab;
            SaveData();
        }
    }
}