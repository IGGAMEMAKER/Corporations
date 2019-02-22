using Unity.Entities;
using UnityEngine;

public class Initializer: MonoBehaviour
{
    public int amountOfCompanies;

    internal EntityManager Manager;

    private void Start()
    {
        Manager = World.Active.GetOrCreateManager<EntityManager>();

        CreateProducts();

        CreateNiches();

        CreateCompanies();
    }

    void CreateNiches()
    {

    }

    void CreateCompanies()
    {

    }

    void CreateProducts()
    {
        for (var i = 0; i < amountOfCompanies; i++)
        {
            Entity AI = Manager.CreateEntity(
                ComponentType.Create<Product2>()
            );


            Manager.SetSharedComponentData(AI, new Product2
            {
                Id = 0,
                BrandPower = 0,
                Clients = 100,

                Programmers = 1,
                Marketers = 0,
                Managers = 0,

                Level = 1,
                ExplorationLevel = 1,

                Analytics = 0,
                ExperimentCount = 0,

                Name = $"Company name {i}",
                Niche = Niche.Messenger,
                Ads = new System.Collections.Generic.List<Assets.Classes.Advert>(),
                Resources = new Assets.Classes.TeamResource(100, 100, 100, 100, 10000)
            });


            if (i == 0)
            {
                Manager.AddComponent(AI, ComponentType.Create<IsPlayerControlled>());
                var p = Manager.GetSharedComponentData<Product2>(AI);
                p.BrandPower = 100;

                Manager.SetSharedComponentData(AI, p);
            }
        }
    }
}
