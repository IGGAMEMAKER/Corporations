using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

using Newtonsoft.Json;
using System.Collections.Generic;
using Entitas;
using System.Linq;
using System.Text;

//[Serializable]
//public partial class GameEntity { }

////namespace Entitas
////{
////    [Serializable]
////    public partial class Entity { }
////}

namespace Assets.Core
{
    partial class Companies
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
            //}
            ////using (FileStream fs = new FileStream("entities.dat", FileMode.OpenOrCreate))
            //{
                var entityData = new Dictionary<int, Dictionary<int, IComponent>>();

                foreach (var e in entities)
                {
                    var comps = e.GetComponents().ToArray();
                    var indices = e.GetComponentIndices();

                    entityData[e.creationIndex] = new Dictionary<int, IComponent> { };

                    for (var i = 0; i < indices.Count(); i++)
                    {
                        var index = indices[i];
                        entityData[e.creationIndex][index] = comps[i];
                    }
                }

                if (entityData.Count > 0)
                {
                    //var newEntityData = JsonConvert.DeserializeObject < Dictionary<int, Dictionary<int, IComponent[]>> >
                    serializer.Serialize(writer, entityData, typeof(Dictionary<int, Dictionary<int, IComponent>>));

                    //var str = serializer.Serialize( JsonConvert.SerializeObject(entityData, Formatting.Indented);
                    //Debug.Log(str);

                    //fs.Write(Encoding.ASCII.GetBytes(str), 0, str.Length);
                }
            }

            //LoadEntities(gameContext);

            return;
            foreach (var e in entities)
                SaveEntity(e);
        }

        public static void LoadEntities(GameContext gameContext)
        {
            using (FileStream fs = new FileStream("entities.dat", FileMode.Open, FileAccess.Read))
            {
                // Read the source file into a byte array.
                byte[] bytes = new byte[fs.Length];
                int numBytesToRead = (int)fs.Length;
                int numBytesRead = 0;
                while (numBytesToRead > 0)
                {
                    // Read may return anything from 0 to numBytesToRead.
                    int n = fs.Read(bytes, numBytesRead, numBytesToRead);

                    // Break when the end of the file is reached.
                    if (n == 0)
                        break;

                    numBytesRead += n;
                    numBytesToRead -= n;
                }
                numBytesToRead = bytes.Length;

                var str = Encoding.ASCII.GetString(bytes);
                Debug.Log("Reading from file ...");

                Debug.Log(str);

                var newEntityData = JsonConvert.DeserializeObject<Dictionary<int, Dictionary<int, object>>>(str);
                Debug.Log("Deserialized");

                //Debug.Log(JsonConvert.SerializeObject(newEntityData, Formatting.Indented));
                Contexts.sharedInstance.game.DestroyAllEntities();

                Debug.Log("Cleaning entities");
                Debug.Log(Contexts.sharedInstance.game.GetEntities().Length);

                Debug.Log("Restoring data");

                foreach (var kvp in newEntityData)
                {
                    var e = gameContext.CreateEntity();

                    var entityIndex = kvp.Key;
                    var components = kvp.Value;

                    foreach (var c in components)
                    {
                        var componentIndex = c.Key;
                        var component = c.Value;

                        Debug.Log("Read component: " + GameComponentsLookup.componentNames[componentIndex]);
                        Debug.Log(component);

                        //e.AddComponent(componentIndex, component);
                    }
                }
            }
        }

        public static void SaveEntity(GameEntity e)
        {


            //Debug.Log("Saving Entity: " + e.creationIndex);
            //var str = JsonConvert.SerializeObject(e);

            //Debug.Log("Serialized: " + str);

            //foreach (var i in e.GetComponentIndices())
            //{
            //    var t = GameComponentsLookup.componentTypes[i];
            //    var name = GameComponentsLookup.componentNames[i];



            //    //Debug.Log(name + "[" + i + "]= " + str);
            //}
        }

        public static void AcceptInvestmentProposal(GameContext gameContext, int companyId, int investorId)
        {
            var p = GetInvestmentProposal(gameContext, companyId, investorId);

            //if (p == null)
            //    return;

            long cost = Economy.GetCompanyCost(gameContext, companyId);

            var allShares = (long)GetTotalShares(gameContext, companyId);
            long shares = allShares * p.Offer / cost;



            AddShareholder(gameContext, companyId, investorId, (int)shares);

            Economy.IncreaseCompanyBalance(gameContext, companyId, p.Offer);
            Economy.DecreaseInvestmentFunds(gameContext, investorId, p.Offer);

            MarkProposalAsAccepted(gameContext, companyId, investorId);
        }

        static void MarkProposalAsAccepted(GameContext gameContext, int companyId, int investorId)
        {
            var c = Get(gameContext, companyId);

            var proposals = GetInvestmentProposals(gameContext, companyId);

            var index = proposals.FindIndex(p => p.ShareholderId == investorId);

            proposals[index].WasAccepted = true;

            c.ReplaceInvestmentProposals(proposals);
        }
    }
}
