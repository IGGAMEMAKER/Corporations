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

        switch (state.Phase)
        {
            case NicheLifecyclePhase.Innovation:
                break;
            case NicheLifecyclePhase.Trending:
                break;
            case NicheLifecyclePhase.MassUse:
                break;
            case NicheLifecyclePhase.Decay:
                break;
        }

        if (value > 0.9f)
            UpdateNiche(niche);
    }

    bool IsNeedsPromotion(GameEntity niche)
    {
        var duration = niche.nicheState.Duration;
        var X = 6;

        switch (niche.nicheState.Phase)
        {
            case NicheLifecyclePhase.Innovation:
                return duration > X;

            case NicheLifecyclePhase.Trending:
                return duration > 4 * X;

            case NicheLifecyclePhase.MassUse:
                return duration > 10 * X;

            case NicheLifecyclePhase.Decay:
                return duration > 15 * X;

            default:
                return false;
        }
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
                return NicheLifecyclePhase.Death;

            default:
                return NicheLifecyclePhase.Death;
        }
    }

    void UpdateNiche(GameEntity niche)
    {
        var state = niche.nicheState;

        var phase = state.Phase;

        var next = GetNextPhase(phase);

        niche.ReplaceNicheState(state.Growth, next, state.Duration);
    }
}
