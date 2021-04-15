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

        //[SerializeField]
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

        internal void SaveData()
        {
            SavePrefabs(_prefabs);
            SaveUrlOpeningAttempts(UrlOpeningAttempts);
        }

        void LoadData()
        {
            BoldPrint("Read SimpleUI.txt");

            _prefabs = GetPrefabsFromFile();

            UnityEngine.Debug.Log(_prefabs.Count);
            UrlOpeningAttempts = GetUrlOpeningAttempts();
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

        // ------------------- Updates --------------------------
        public void UpdatePrefab(SimpleUISceneType prefab) => UpdatePrefab(prefab, ChosenIndex);
        public void UpdatePrefab(SimpleUISceneType prefab, int index)
        {
            if (!hasChosenPrefab)
                return;

            prefabs[index] = prefab;
            SavePrefabs(prefabs);
        }

        public static void StaticUpdatePrefab(SimpleUISceneType prefab, int index)
        {
            var prefs = GetPrefabsFromFile();

            prefs[index] = prefab;
            SavePrefabs(prefs);
        }

        // ---------------- Save/Load data
        static void SaveUrlOpeningAttempts(Dictionary<string, List<UrlOpeningAttempt>> data)
        {
            SaveToFile(PATH_MissingAssets, data);
        }

        static void SavePrefabs(List<SimpleUISceneType> data)
        {
            SaveToFile(PATH_SimpleUI, data);
        }

        static void SavePrefabMatches(List<PrefabMatchInfo> data)
        {
            SaveToFile(PATH_Matches, data);
        }

        public static Dictionary<string, List<UrlOpeningAttempt>> GetUrlOpeningAttempts()
        {
            return GetJSONDataFromFile<Dictionary<string, List<UrlOpeningAttempt>>>(PATH_MissingAssets);
        }

        public static List<SimpleUISceneType> GetPrefabsFromFile()
        {
            return GetJSONDataFromFile<List<SimpleUISceneType>>(PATH_SimpleUI);
        }

        public static List<PrefabMatchInfo> GetPrefabMatchesFromFile()
        {
            return GetJSONDataFromFile<List<PrefabMatchInfo>>(PATH_Matches);
        }
    }
}