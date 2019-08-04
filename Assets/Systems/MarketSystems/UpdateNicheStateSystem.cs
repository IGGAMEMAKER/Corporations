using System.Collections.Generic;
using Assets.Utils;
using Entitas;
using UnityEngine;

public partial class UpdateNicheStateSystem : OnMonthChange, IInitializeSystem
{
    public UpdateNicheStateSystem(Contexts contexts) : base(contexts)
    {
    }

    void CheckNiches()
    {
        var niches = NicheUtils.GetNiches(gameContext);

        foreach (var n in niches)
            CheckNiche(n);
    }

    protected override void Execute(List<GameEntity> entities)
    {
        CheckNiches();
    }

    void ActivateIfNecessary(GameEntity niche)
    {
        var date = ScheduleUtils.GetCurrentDate(gameContext);

        var state = NicheUtils.GetMarketState(niche);
        var nicheStartDate = niche.nicheLifecycle.OpenDate;

        if (date > nicheStartDate && state == NicheLifecyclePhase.Idle)
        {
            //Debug.Log($"Awake niche from idle {state} {date}: niche start date={nicheStartDate}");
            PromoteNiche(niche);
        }
    }

    void CheckNiche(GameEntity niche)
    {
        var phase = NicheUtils.GetMarketState(niche);

        if (phase == NicheLifecyclePhase.Death)
            return;

        ActivateIfNecessary(niche);

        if (NicheUtils.GetCompetitorsAmount(niche.niche.NicheType, gameContext) == 0)
            return;

        //var value = Random.Range(0, 1f);

        if (IsNeedsPromotion(niche)) //  && value > 0.9f
            PromoteNiche(niche);
        else
            DecrementDuration(niche);
    }

    void PromoteNiche(GameEntity niche)
    {
        NicheUtils.PromoteNicheState(niche);
    }


    bool IsNeedsPromotion(GameEntity niche)
    {
        var duration = niche.nicheState.Duration;

        var phase = NicheUtils.GetMarketState(niche);

        if (phase == NicheLifecyclePhase.Death || phase == NicheLifecyclePhase.Idle)
            return false;

        return duration <= 0;
    }

    void DecrementDuration(GameEntity niche)
    {
        var state = niche.nicheState;

        niche.ReplaceNicheState(state.Phase, Mathf.Max(state.Duration - 1, 0));
    }

    void IInitializeSystem.Initialize()
    {
        CheckNiches();
    }
}
