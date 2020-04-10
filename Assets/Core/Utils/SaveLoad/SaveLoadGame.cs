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
            var entities = Q.GetEntities();

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

            using (StreamWriter sw = new StreamWriter(fileName))
            using (Newtonsoft.Json.JsonWriter writer = new Newtonsoft.Json.JsonTextWriter(sw))
            {
                var entityData = new Dictionary<int, IComponent[]>();

                foreach (var e in entities)
                {
                    var comps = e.GetComponents().ToArray();

                    entityData[e.creationIndex] = comps;
                }

                if (entityData.Count > 0)
                {
                    //var newEntityData = JsonConvert.DeserializeObject < Dictionary<int, Dictionary<int, IComponent[]>> >
                    serializer.Serialize(writer, entityData, typeof(Dictionary<int, IComponent[]>));

                    //var str = serializer.Serialize( JsonConvert.SerializeObject(entityData, Formatting.Indented);
                    //Debug.Log(str);

                    //fs.Write(Encoding.ASCII.GetBytes(str), 0, str.Length);
                }
            }
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
