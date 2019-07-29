using Assets.Utils;
using Assets.Utils.Tutorial;

// actions used in multiple strategies
public partial class AIProductSystems
{
    void Crunch(GameEntity product)
    {
        if (!product.isCrunching)
            TeamUtils.ToggleCrunching(gameContext, product.company.Id);
    }

    void DisableCrunches(GameEntity product)
    {
        if (product.isCrunching)
            TeamUtils.ToggleCrunching(gameContext, product.company.Id);
    }

    void UpgradeSegment(GameEntity product, UserType userType)
    {
        ProductUtils.UpdateSegment(product, gameContext, userType);
    }

    void StayInMarket(GameEntity product)
    {
        if (ProductUtils.IsInMarket(product, gameContext))
            UpgradeSegment(product, UserType.Core);
    }

    void Innovate(GameEntity product)
    {
        UpgradeSegment(product, UserType.Core);
    } 

    void DecreasePrices(GameEntity product)
    {
        var price = product.finance.price;

        switch (price)
        {
            case Pricing.High: price = Pricing.Medium; break;
            case Pricing.Medium: price = Pricing.Low; break;
        }

        ProductUtils.SetPrice(product, price);
    }

    void IncreasePrices(GameEntity product)
    {
        var price = product.finance.price;

        switch (price)
        {
            case Pricing.Medium: price = Pricing.High; break;
            case Pricing.Low: price = Pricing.Medium; break;
            case Pricing.Free: price = Pricing.Low; break;
        }

        ProductUtils.SetPrice(product, price);
    }

    void HireWorker(GameEntity company, WorkerRole workerRole)
    {
        if (IsCanAffordWorker(company, workerRole))
        {
            TeamUtils.HireWorker(company, workerRole);

            Print($"Hire {workerRole.ToString()}", company);
        }
    }

    void GrabTestClients(GameEntity company)
    {
        //if (company.isControlledByPlayer && !TutorialUtils.IsOpenedFunctionality(gameContext, TutorialFunctionality.FirstAdCampaign))
        //    return;

        //Print("Start test campaign", company);

        MarketingUtils.StartTestCampaign(gameContext, company);
    }

    void StartTargetingCampaign(GameEntity company)
    {
        //MyProductEntity.AddEventMarketingEnableTargeting(productId);
        MarketingUtils.EnableTargeting(company);
    }

    void StartBrandingCampaign(GameEntity company)
    {
        MarketingUtils.StartBrandingCampaign(gameContext, company);
    }

    void UpgradeTeam(GameEntity company)
    {
        var status = company.team.TeamStatus;

        TeamUtils.Promote(company);

        Print($"Upgrade team from {status.ToString()}", company);

        if (status == TeamStatus.Pair)
        {
            Print($"Set universal worker as CEO", company);

            TeamUtils.SetRole(company, company.cEO.HumanId, WorkerRole.Business, gameContext);
        }

        if (status == TeamStatus.SmallTeam)
        {

        }
    }
}
