using System.Collections.Generic;
using Assets.Utils;
using Entitas;
using UnityEngine;

public partial class UpdateNicheStateSystem : OnMonthChange
{
    public UpdateNicheStateSystem(Contexts contexts) : base(contexts)
    {
    }

    protected override void Execute(List<GameEntity> entities)
    {
        var niches = NicheUtils.GetNiches(gameContext);

        foreach (var n in niches)
            CheckNiche(n);
    }

    void CheckNiche(GameEntity niche)
    {
        var state = niche.nicheState;

        if (state.Phase == NicheLifecyclePhase.Death)
            return;

        if (NicheUtils.GetCompetitorsAmount(niche.niche.NicheType, gameContext) == 0)
            return;

        var value = Random.Range(0, 1f);

        if (value > 0.9f && IsNeedsPromotion(niche))
            UpdateNiche(niche);
        else
            IncrementDuration(niche);
    }



    bool IsNeedsPromotion(GameEntity niche)
    {
        var duration = niche.nicheState.Duration;
        var nichePeriod = NicheUtils.GetNichePeriodDuration(niche);

        var phase = niche.nicheState.Phase;

        if (phase == NicheLifecyclePhase.Death || phase == NicheLifecyclePhase.Idle)
            return false;

        return duration > nichePeriod * NicheUtils.GetMinimumPhaseDurationInPeriods(phase);
    }

    NicheLifecyclePhase GetNextPhase(NicheLifecyclePhase phase)
    {
        switch(phase)
        {
            case NicheLifecyclePhase.Idle:
                return NicheLifecyclePhase.Innovation;

            case NicheLifecyclePhase.Innovation:
                return NicheLifecyclePhase.Trending;

            case NicheLifecyclePhase.Trending:
                return NicheLifecyclePhase.MassUse;

            case NicheLifecyclePhase.MassUse:
                return NicheLifecyclePhase.Decay;

            case NicheLifecyclePhase.Decay:
            default:
                return NicheLifecyclePhase.Death;
        }
    }

    void UpdateNiche(GameEntity niche)
    {
        var state = niche.nicheState;

        var phase = state.Phase;

        var next = GetNextPhase(phase);

        niche.ReplaceNicheState(state.Growth, next, 0);
    }

    void IncrementDuration(GameEntity niche)
    {
        var state = niche.nicheState;

        niche.ReplaceNicheState(state.Growth, state.Phase, state.Duration + 1);
    }
}
