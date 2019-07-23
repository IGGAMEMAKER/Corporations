using Assets.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenderMarketState : UpgradedParameterView
{
    public override string RenderHint()
    {
        var state = State;
        switch (state)
        {
            case NicheLifecyclePhase.Death:
                return "This market is dead. People don't need these apps anymore!";

            case NicheLifecyclePhase.Decay:
                return "This market's peak has gone. Reduce your expenses and prepare for market death";

            case NicheLifecyclePhase.Idle:
                return "We don't know if people need this or not. Maybe you'll be the innovator";

            case NicheLifecyclePhase.Innovation:
                return "Some people had shown their interest in these apps! Be first in new market!";

            case NicheLifecyclePhase.Trending:
                return "This market grows extremely fast";

            case NicheLifecyclePhase.MassUse:
                return "People need these apps, but will it last long?";

            default:
                return "??? " + state.ToString();
        }
    }

    public override string RenderValue()
    {
        return State.ToString();
    }

    NicheLifecyclePhase State => NicheUtils.GetMarketState(GameContext, SelectedNiche);
}
