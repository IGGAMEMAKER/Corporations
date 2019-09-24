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

    void UpgradeSegment(GameEntity product)
    {
        ProductUtils.UpdgradeProduct(product, gameContext);

        ProductUtils.UpgradeExpertise(product, gameContext);
    }

    void StayInMarket(GameEntity product)
    {
        if (ProductUtils.IsInMarket(product, gameContext))
            UpgradeSegment(product);
    }

    void Innovate(GameEntity product)
    {
        UpgradeSegment(product);
    } 

    void StartTargetingCampaign(GameEntity company)
    {
        //MyProductEntity.AddEventMarketingEnableTargeting(productId);
        MarketingUtils.EnableTargeting(company);
    }

    void StartBrandingCampaign(GameEntity company)
    {

        //if (company.branding.BrandPower < 90)
        //    MarketingUtils.StartBrandingCampaign(gameContext, company);
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
