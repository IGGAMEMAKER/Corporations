using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class SimpleUIEventHandler : MonoBehaviour
{
    // string - url
    // GameObject - prefab
    public Dictionary<string, GameObject> Objects;

    public string CurrentUrl;
    static List<SimpleUISceneType> prefabs; // = new List<NewSceneTypeBlah>();


    public void OpenUrl(string url)
    {
        var p = GetPrefab(url);
        
        if (p != null)
            p.SetActive(true);
    }

    GameObject GetPrefab(string url)
    {
        if (!Objects.ContainsKey(url))
        {
            if (!prefabs.Any(p => p.Url.Equals(url)))
            {
                Debug.LogError("URL " + url + " not found");
                return null;
            }
            var pre = prefabs.First(p => p.Url.Equals(url));

            var obj = AssetDatabase.LoadAssetAtPath<GameObject>(pre.AssetPath);
            if (obj == null)
            {
                Debug.LogError("Prefab in route " + pre.AssetPath + " not found");
                return null;
            }
            
            // Objects[url] = Instantiate(AssetDatabase.GetMainAssetTypeAtPath(pre.AssetPath));
            Objects[url] = Instantiate(obj);
        }

        return Objects[url];
    }

    static void SaveData()
    {
        var fileName = "SimpleUI/SimpleUI.txt";

        Newtonsoft.Json.JsonSerializer serializer = new Newtonsoft.Json.JsonSerializer();
        serializer.Converters.Add(new Newtonsoft.Json.Converters.JavaScriptDateTimeConverter());
        serializer.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
        serializer.TypeNameHandling = Newtonsoft.Json.TypeNameHandling.Auto;
        serializer.Formatting = Newtonsoft.Json.Formatting.Indented;

        var entityData = prefabs; // new Dictionary<int, IComponent[]>();

        using (StreamWriter sw = new StreamWriter(fileName))
        using (Newtonsoft.Json.JsonWriter writer = new Newtonsoft.Json.JsonTextWriter(sw))
        {
            if (entityData.Count > 0)
            {
                Debug.Log("Serializing data " + entityData.Count);
                serializer.Serialize(writer, entityData);

                Debug.Log("Serialized " + entityData.Count);
            }
        }
    }

    static void LoadData()
    {
        var fileName = "SimpleUI/SimpleUI.txt";

        List<SimpleUISceneType> obj = Newtonsoft.Json.JsonConvert.DeserializeObject<List<SimpleUISceneType>>(
            File.ReadAllText(fileName), new Newtonsoft.Json.JsonSerializerSettings
            {
                TypeNameHandling = Newtonsoft.Json.TypeNameHandling.Auto,
                NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore,
            });

        prefabs = obj ?? new List<SimpleUISceneType>();
    }
}