using Assets.Utils;

public partial class AIProductSystems : OnDateChange
{
    void FixLoyalty(GameEntity product)
    {
        Print("Terrible loyalty", product);

        // decrease prices
        DecreasePrices(product);

        // improve segments
        ImproveSegments(product);
    }

    long GetSegmentImportance(GameEntity product, UserType userType)
    {
        var loyalty = MarketingUtils.GetClientLoyalty(gameContext, product.company.Id, userType);

        var income = CompanyEconomyUtils.GetIncomeBySegment(gameContext, product.company.Id, userType);
        var totalIncome = CompanyEconomyUtils.GetCompanyIncome(product, gameContext);

        // TODO 1 DISLOYAL CORE USER IS WAY MORE IMPORTANT THAN THOUSANDS OF PAYING REGULAR USERS
        // WHICH IS NOT ALWAYS WORTH IT
        var badLoyaltyPenalty = loyalty < 0 ? 500 : 0;
        var incomeBonus = (long)income * 100 / (totalIncome + 1);

        // TODO COUNT COMPANY GOAL TOO!
        return badLoyaltyPenalty + incomeBonus;
    }

    UserType GetMoreImportantSegment(GameEntity product)
    {
        var CorePriority = 5 + GetSegmentImportance(product, UserType.Core);
        var RegularPriority = 4 + GetSegmentImportance(product, UserType.Regular);

        return CorePriority > RegularPriority ? UserType.Core : UserType.Regular;
    }

    void ImproveSegments(GameEntity product)
    {
        var core = MarketingUtils.GetClientLoyalty(gameContext, product.company.Id, UserType.Core);
        var regular = MarketingUtils.GetClientLoyalty(gameContext, product.company.Id, UserType.Regular);

        var segment = GetMoreImportantSegment(product);

        UpgradeSegment(product, segment);
    }

    void UpgradeSegment(GameEntity product, UserType userType)
    {
        ProductUtils.UpdateSegment(product, gameContext, userType);
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
}
