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
        // Save to file
        static void SaveToFile<T>(string fileName, T data)
        {
            var jsonString = JsonUtility.ToJson(data);
            File.WriteAllText(fileName, jsonString);

            //Newtonsoft.Json.JsonSerializer serializer = new Newtonsoft.Json.JsonSerializer();
            //serializer.Converters.Add(new Newtonsoft.Json.Converters.JavaScriptDateTimeConverter());
            //serializer.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
            //serializer.TypeNameHandling = Newtonsoft.Json.TypeNameHandling.Auto;
            //serializer.Formatting = Newtonsoft.Json.Formatting.Indented;

            //using (StreamWriter sw = new StreamWriter(fileName))
            //using (Newtonsoft.Json.JsonWriter writer = new Newtonsoft.Json.JsonTextWriter(sw))
            //{
            //    if (data.Count() > 0)
            //    {
            //        serializer.Serialize(writer, data);
            //    }
            //}
        }

        public static Newtonsoft.Json.JsonSerializerSettings settings => new Newtonsoft.Json.JsonSerializerSettings
        {
            TypeNameHandling = Newtonsoft.Json.TypeNameHandling.Auto,
            NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore,
        };

        public static T GetJSONDataFromFile<T>(string fileName)
        {
            Print("Get JSON data " + fileName);

            try
            {

                var obj = JsonUtility.FromJson<T>(File.ReadAllText(fileName));
                //var obj = Newtonsoft.Json.JsonConvert.DeserializeObject<T>(File.ReadAllText(fileName), settings);

                if (obj != null)
                    return obj;
            }
            catch
            {

            }

            return (T)Activator.CreateInstance(typeof(T));
        }
    }
}