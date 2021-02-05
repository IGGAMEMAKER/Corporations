using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
//using Assets.Core;
using UnityEngine;
using UnityEditor;
using UnityEditor.Experimental.SceneManagement;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;


// Save/Load info
public partial class SimpleUI
{
    static List<SimpleUISceneType> _prefabs;
    static Dictionary<string, List<UrlOpeningAttempt>> UrlOpeningAttempts;

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


    #region UI shortcuts

    static void Print2(string text)
    {
        Print("PRT2: " + text);
    }

    static void Print(string text)
    {
        Debug.Log(text);
    }

    public static bool Button(string text)
    {
        GUIStyle style = GUI.skin.FindStyle("Button");
        style.richText = true;

        if (!text.Contains("\n"))
            text += "\n";

        return GUILayout.Button($"<b>{text}</b>", style);
    }

    public static void Label(string text)
    {
        Space();
        BoldLabel(text);
    }

    public static void BoldLabel(string text)
    {
        GUILayout.Label(text, EditorStyles.boldLabel);
    }

    public static void Space(int space = 15)
    {
        GUILayout.Space(space);
    }

    #endregion


    #region string utils

    public static IEnumerable<SimpleUISceneType> GetSubUrls(string url, bool recursive) =>
        prefabs.Where(p => isSubRouteOf(p.Url, url, recursive));


    /// <summary>
    /// if recursive == false
    /// function will return true ONLY for DIRECT subroutes
    /// 
    /// otherwise it will return true for sub/sub routes too
    /// </summary>
    /// <param name="subUrl"></param>
    /// <param name="root"></param>
    /// <param name="recursive"></param>
    /// <returns></returns>
    public static bool isSubRouteOf(string subUrl, string root, bool recursive)
    {
        // searching for /ProjectScreen descendants

        // filter /Abracadabra
        if (!subUrl.StartsWith(root))
            return false;

        // filter self
        if (subUrl.Equals(root))
            return false;

        var subStr = subUrl.Substring(root.Length);

        if (root.Equals("/"))
            subStr = subUrl;

        bool isUrlItself = subStr.IndexOf('/') == 0;

        // filter /ProjectScreenBLAH
        if (!isUrlItself)
            return false;

        // remaining urls are
        // /ProjectScreen/Blah
        // /ProjectScreen/Blah/Blah

        // subStrs
        // /Blah
        // /Blah/Blah

        bool isDirectSubroute = subStr.LastIndexOf('/') == 0;
        bool isSubSubRoute = subStr.LastIndexOf('/') > 0;

        if (!recursive)
            return isDirectSubroute;
        else
            return true;
    }

    static string GetPrettyNameFromAssetPath(string path)
    {
        var x = path.Split('/').Last();
        var ind = x.LastIndexOf(".prefab");

        if (isSceneAsset(x))
        {
            ind = x.LastIndexOf(".unity");
        }

        return x.Substring(0, ind);
    }

    public static string GetPrettyNameForExistingUrl(string url)
    {
        var prefab = GetPrefabByUrl(url);

        return prefab.Name;
    }

    public static string GetUpperUrl(string url)
    {
        var index = url.LastIndexOf("/");

        if (index <= 0)
            return "/";

        return url.Substring(0, index);
    }

    bool Contains(string text1, string searching)
    {
        return text1.ToLower().Contains(searching.ToLower());
    }

    string GetValidatedUrl(string url)
    {
        if (!url.StartsWith("/"))
            return url.Insert(0, "/");

        return url;
    }

    #endregion

    public static bool IsAssetPathExists(string path)
    {
        return prefabs.Any(p => p.AssetPath.Equals(path));
    }

    public static bool IsUrlExist(string url)
    {
        return prefabs.Any(p => p.Url.Equals(url));
    }

    static void SaveData()
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

        Print("<B>LOAD DATA</B>");

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

    static void UpdatePrefab(SimpleUISceneType prefab) => UpdatePrefab(prefab, ChosenIndex);

    public static void UpdatePrefab(SimpleUISceneType prefab, int index)
    {
        if (!hasChosenPrefab)
            return;

        prefabs[index] = prefab;
        SaveData();
    }

    // ----- utils -------------
    void RenderPrefabs(IEnumerable<SimpleUISceneType> list, string trimStart = "")
    {
        foreach (var p in list)
        {
            var c = GUI.color;

            // set color
            bool isChosen = hasChosenPrefab && prefabs[ChosenIndex].AssetPath.Equals(p.AssetPath);

            var color = isChosen ? Color.yellow : Color.white;

            //ColorUtility.TryParseHtmlString(isChosen ? "gold" : "white", out Color color);
            //var color = ColorUtility.TryParseHtmlString(isChosen ? "#FFAB04" Visuals.GetColorFromString(isChosen ? Colors.COLOR_YOU : Colors.COLOR_NEUTRAL);

            GUI.contentColor = color;
            GUI.color = color;
            GUI.backgroundColor = color;


            GUIStyle style = GUI.skin.FindStyle("Button");
            style.richText = true;

            string trimmedUrl = p.Url;

            if (trimStart.Length > 0)
            {
                var lastDashIndex = trimmedUrl.LastIndexOf('/');

                trimmedUrl = trimmedUrl.Substring(lastDashIndex);
                //trimmedUrl = trimmedUrl.Trim(trimStart.ToCharArray());
            }

            if (GUILayout.Button($"<b>{p.Name}</b>\n{trimmedUrl}", style))
            {
                OpenPrefab(p);
            }

            // restore colors
            GUI.contentColor = c;
            GUI.color = c;
            GUI.backgroundColor = c;
        }
    }

    static void TryToIncreaseCurrentPrefabCounter()
    {
        if (hasChosenPrefab)
        {
            var pref = prefabs[ChosenIndex];

            pref.Usages++;
            pref.LastOpened = DateTime.Now.Ticks;

            UpdatePrefab(pref);
        }
    }

    static SimpleUISceneType GetPrefabByUrl(string url)
    {
        return prefabs.FirstOrDefault(p => p.Url.Equals(url));
    }

    public static void OpenUrl(string url)
    {
        SimpleUIEventHandler eventHandler = FindObjectOfType<SimpleUIEventHandler>();

        if (eventHandler == null)
        {
            Debug.LogError("SimpleUIEventHandler NOT FOUND");
            
            return;
        }

        var queryIndex = url.IndexOf('?');
        var query = "";

        if (queryIndex >= 0)
        {
            query = url.Substring(queryIndex);
            url = url.Substring(0, queryIndex);
        }

        eventHandler.OpenUrl(url);
    }

    static void BoldPrint(string text)
    {
        Debug.Log($"<b>{text}</b>");
    }

}
