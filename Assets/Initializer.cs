using UnityEngine;
using Unity.Entities;

public class Initializer : MonoBehaviour
{

}

public class InitializerSystem : ComponentSystem
{
    internal EntityManager Manager;

    protected override void OnCreateManager()
    {
        Manager = World.Active.GetOrCreateManager<EntityManager>();
    }

    void CreateProducts()
    {
        //Entity AI = Manager.CreateEntity(
        //    ComponentType.Create<Assets.Classes.Product>()
        //);

        //for (var i = 0; i < 4; i++)
        //{
        //    Manager.SetSharedComponentData(AI, new Assets.Classes.Product
        //    {
        //        Id = 0,
        //        BrandPower = 0,
        //        Clients = 100,

        //        Programmers = 1,
        //        Marketers = 0,
        //        Managers = 0,

        //        Level = 1,
        //        ExplorationLevel = 1,

        //        Analytics = 0,
        //        ExperimentCount = 0,

        //        Name = $"Company name {i}",
        //        //Niche = Assets.Classes.Niche.Messenger,
        //        //Ads = new System.Collections.Generic.List<Assets.Classes.Advert>(),
        //        //Resources = new Assets.Classes.TeamResource(100, 100, 100, 100, 10000)
        //    });
        //}

        //Debug.Log("Created 4 entities");

        ////foreach (var e in GetEntities<Assets.Classes.Product>())
        ////{
        ////    Debug.Log($"entity {e.Name}");
        ////}
    }

    protected override void OnUpdate()
    {
        CreateProducts();

        //var spawners = GetEntities<Assets.Classes.Product>();

        //foreach entity with (Transform, DestroyEntity) Destroy(entity.transform.gameObject);

        //GameObjectEntity.Destroy(this);
        //EntityManager.DestroyEntity();
    }
}
