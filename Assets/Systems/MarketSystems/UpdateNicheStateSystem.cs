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

        //var value = Random.Range(0, 1f);

        if (IsNeedsPromotion(niche)) //  && value > 0.9f
            UpdateNiche(niche);
        else
            DecrementDuration(niche);
    }

    void UpdateNiche(GameEntity niche)
    {
        NicheUtils.PromoteNicheState(niche);
    }


    bool IsNeedsPromotion(GameEntity niche)
    {
        var duration = niche.nicheState.Duration;

        var phase = niche.nicheState.Phase;

        if (phase == NicheLifecyclePhase.Death || phase == NicheLifecyclePhase.Idle)
            return false;

        return duration <= 0;
    }

    void DecrementDuration(GameEntity niche)
    {
        var state = niche.nicheState;

        niche.ReplaceNicheState(state.Phase, state.Duration - 1);
    }
}
