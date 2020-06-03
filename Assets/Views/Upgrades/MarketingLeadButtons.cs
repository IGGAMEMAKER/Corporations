using UnityEngine;

public class MarketingLeadButtons : RoleRelatedButtons
{
    public GameObject Channels;

    internal override void Render(GameEntity company)
    {
        base.Render(company);

        bool isCEO = HasWorker(WorkerRole.CEO, company);
        bool isMarketingLead = HasWorker(WorkerRole.MarketingLead, company);

        Draw(Channels, isMarketingLead || isCEO);
    }
}
