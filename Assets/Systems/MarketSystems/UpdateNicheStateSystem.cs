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

        var filledPromotableMarkets = niches
            .Where(n => Markets.GetMarketState(n) > MarketState.Idle && Markets.GetMarketState(n) < MarketState.Death)
            // has companies
            .Where(n => Markets.GetCompetitorsAmount(n.niche.NicheType, gameContext) > 0)
            ;

        // activate idle markets
        foreach (var n in idleNiches)
            PromoteNiche(n);

        foreach (var n in filledPromotableMarkets)
            CheckNiche(n);
    }

    void CheckNiche(GameEntity niche)
    {
        bool needsPromotion = niche.nicheClientsContainer.Clients[0] <= 0;

        if (needsPromotion)
            PromoteNiche(niche);
        else
            DecrementDuration(niche);
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

        NotifyIfNecessary(niche);
    }


    void NotifyIfNecessary(GameEntity niche)
    {
        var player = Companies.GetPlayerCompany(gameContext);

        if (player == null)
            return;

        if (!Companies.IsInSphereOfInterest(player, niche.niche.NicheType))
            return;

        NotificationUtils.AddPopup(gameContext, new PopupMessageMarketPhaseChange(niche.niche.NicheType));
    }

    void DecrementDuration(GameEntity niche)
    {
        Markets.DecrementDuration(niche);
    }
}
