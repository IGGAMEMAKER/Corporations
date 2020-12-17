using Entitas;
using System.Collections.Generic;

class CampaignStatsInitializerSystem : IInitializeSystem
{
    readonly GameContext context;

    public CampaignStatsInitializerSystem(Contexts contexts)
    {
        context = contexts.game;
    }

    void IInitializeSystem.Initialize()
    {
        var statsContainer = context.CreateEntity();

        // TODO move to better place
        statsContainer.AddCampaignStats(new Dictionary<CampaignStat, int>
        {
            [CampaignStat.Acquisitions] = 0,
            [CampaignStat.Bankruptcies] = 0,
            [CampaignStat.PromotedCompanies] = 0,
            [CampaignStat.SpawnedFunds] = 0
        });
    }
}