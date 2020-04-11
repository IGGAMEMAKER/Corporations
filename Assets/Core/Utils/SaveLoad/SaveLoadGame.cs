using System;
using System.IO;
using UnityEngine;

using Newtonsoft.Json;
using System.Collections.Generic;
using Entitas;
using System.Linq;
using System.Text;

namespace Assets.Core
{
    public partial class State
    {
        public static void SaveGame(GameContext Q)
        {
            var entities = Q.GetEntities()
                .Where(e => !e.hasAnyCompanyGoalListener)
                .Where(e => !e.hasAnyCompanyListener)
                .Where(e => !e.hasAnyDateListener)
                .Where(e => !e.hasAnyNotificationsListener)
                .ToArray()
                ;

            SaveEntities(entities, Q);
        }

        public static void SaveEntities(GameEntity[] entities, GameContext gameContext)
        {
            var fileName = "entities.dat";

            Newtonsoft.Json.JsonSerializer serializer = new Newtonsoft.Json.JsonSerializer();
            serializer.Converters.Add(new Newtonsoft.Json.Converters.JavaScriptDateTimeConverter());
            serializer.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
            serializer.TypeNameHandling = Newtonsoft.Json.TypeNameHandling.Auto;
            serializer.Formatting = Newtonsoft.Json.Formatting.Indented;

            Newtonsoft.Json.Serialization.ITraceWriter traceWriter = new Newtonsoft.Json.Serialization.MemoryTraceWriter();
            var converterSettings = new JsonSerializerSettings
            {
                TraceWriter = traceWriter,
                Converters = { new Newtonsoft.Json.Converters.JavaScriptDateTimeConverter() },
                //TypeNameHandling = TypeNameHandling.All,
                Formatting = Formatting.None,
                MissingMemberHandling = MissingMemberHandling.Ignore
            };


            var entityData = new Dictionary<int, IComponent[]>();

            foreach (var e in entities)
            {
                var comps = e.GetComponents();
                //Debug.Log("Serializing entity #" + e.creationIndex + ": " + string.Join(",", comps.Select(c => c.GetType())));

                entityData[e.creationIndex] = comps;
            }

            var accurateData = new Dictionary<int, string>();

            foreach (var e in entityData)
            {
                var index = e.Key;
                var components = e.Value;

                Debug.Log($"Serializing entity #{index} ...");
                foreach (var c in components)
                {
                    Debug.Log("     Serializing component " + c.GetType());
                    var data = JsonConvert.SerializeObject(c, converterSettings);

                    Debug.Log("     " + data);
                }

                Debug.Log($"DONE entity #{index}");

                //accurateData[index] = data;
            }

            //var str =
            //    JsonConvert.SerializeObject(
            //    entityData,
            //    converterSettings
            //    );

            //Debug.Log("Serializing: " + str);
            //Debug.Log(traceWriter);


            //using (StreamWriter sw = new StreamWriter(fileName))
            //using (Newtonsoft.Json.JsonWriter writer = new Newtonsoft.Json.JsonTextWriter(sw))
            //{
            //    if (entityData.Count > 0)
            //    {
            //        Debug.Log("Serializing data " + entityData.Count);
            //        serializer.Serialize(writer, entityData);

            //        Debug.Log("Serialized " + entityData.Count);
            //    }
            //}
        }

        public static void ClearEntities()
        {
            Contexts.sharedInstance.game.DestroyAllEntities();
        }

        public static void LoadGame(GameContext gameContext)
        {
            ClearEntities();

            LoadEntities(gameContext);
        }

        public static void LoadEntities(GameContext gameContext)
        {
            var fileName = "entities.dat";

            Dictionary<int, IComponent[]> obj = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<int, IComponent[]>>(File.ReadAllText(fileName), new Newtonsoft.Json.JsonSerializerSettings
            {
                TypeNameHandling = Newtonsoft.Json.TypeNameHandling.Auto,
                NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore,
            });


            var componentTypes = new Dictionary<Type, int>();

            for (var i = 0; i < GameComponentsLookup.componentTypes.Length; i++)
            {
                var t = GameComponentsLookup.componentTypes[i];

                componentTypes[t] = i;
            }


            foreach (var kvp in obj)
            {
                var e = gameContext.CreateEntity();

                var entityIndex = kvp.Key;
                var components = kvp.Value;

                foreach (var c in components)
                {
                    Debug.Log("Read component: " + c);

                    var componentIndex = componentTypes[c.GetType()];

                    e.AddComponent(componentIndex, c);
                }
            }
        }
    }
}
