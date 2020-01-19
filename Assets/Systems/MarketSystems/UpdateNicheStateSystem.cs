using System.Collections.Generic;
using System.Linq;
using Assets.Core;
using Entitas;

public partial class UpdateNicheStateSystem : OnMonthChange, IInitializeSystem
{
    public UpdateNicheStateSystem(Contexts contexts) : base(contexts) {}

    void IInitializeSystem.Initialize()
    {
        CheckNiches();
    }

    protected override void Execute(List<GameEntity> entities)
    {
        CheckNiches();
    }

    void CheckNiches()
    {
        var niches = Markets.GetNiches(gameContext);

        var idleNiches = niches
            .Where(n => Markets.GetMarketState(n) == MarketState.Idle)
            .Where(IsReadyToBeActivated);

        var activePromotableMarkets = niches
            .Where(n => Markets.GetMarketState(n) > MarketState.Idle && Markets.GetMarketState(n) < MarketState.Death)
            // has companies
            .Where(n => Markets.GetCompetitorsAmount(n.niche.NicheType, gameContext) > 0)
            ;

        foreach (var n in activePromotableMarkets)
            CheckNiche(n);

        // activate idle markets
        foreach (var n in idleNiches)
            PromoteNiche(n);
    }

    void CheckNiche(GameEntity niche)
    {
        bool needsPromotion = niche.nicheClientsContainer.Clients[0] <= 0;

        if (needsPromotion)
            PromoteNiche(niche);
        else
            Markets.DecrementDuration(niche);
    }

    bool IsReadyToBeActivated(GameEntity niche)
    {
        var date = ScheduleUtils.GetCurrentDate(gameContext);

        var openDate = niche.nicheLifecycle.OpenDate;

        return date > openDate;
    }


    void PromoteNiche(GameEntity niche)
    {
        Markets.PromoteNicheState(niche);

        NotificationUtils.SendMarketStateChangePopup(gameContext, niche);
    }
}
