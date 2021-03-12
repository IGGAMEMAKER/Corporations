using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace SimpleUI
{
    [Serializable]
    public struct CountableAssetContainer
    {
        public List<CountableAsset> CountableAssets;
    }

    [Serializable]
    public struct CountableAsset
    {
        public string AssetPath;

        public long Usages;
        public long LastOpened;

        public CountableAsset(string assetPath)
        {
            AssetPath = assetPath;

            Usages = 0;
            LastOpened = 0;
        }
    }

    [Serializable]
    public struct SimpleUISceneType
    {
        public string ID;

        public string Url;
        public string Name;
        public string AssetPath;
        public bool Exists;

        public long Usages;
        public long LastOpened;

        public SimpleUISceneType(string url, string assetPath, string name = "")
        {
            Url = url;
            AssetPath = assetPath;
            Name = name.Length > 0 ? name : url;
            Exists = AssetDatabase.LoadAssetAtPath<UnityEngine.Object>(assetPath) != null;

            Usages = 0;
            LastOpened = 0;

            ID = "";

            SetGUID();
        }

        public void SetGUID()
        {
            if (ID != null && ID.Length != 0)
                return;

            // https://stackoverflow.com/questions/11313205/generate-a-unique-id
            var guid = string.Format("{1:N}", Url, Guid.NewGuid());
            //var guid = Guid.NewGuid().ToString();
            ID = guid;
        }
    }

    //[Serializable]
    public struct PrefabMatchInfoDetailed
    {
        public PrefabMatchInfo prefabMatchInfo;

        // for rename
        public GameObject Asset;
        public OpenUrl Component;

        public bool IsDirectMatch; // with no nested prefabs, can apply changes directly. (Both on root and it's childs)
        public bool IsNormalPartOfNestedPrefab; // absolutely normal prefab part with NO overrides. No actions required

        public bool IsOverridenAsComponentProperty;
        public bool IsOverridenAsAddedComponent;
    }


    public struct PrefabMatchInfo
    {
        public string PrefabAssetPath;
        public string URL;
        public string URL_ID;

        // debug
        public string ComponentName;
        public int ComponentID;
    }
}