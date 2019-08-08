using Assets.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenderMarketState : UpgradedParameterView
{
    public override string RenderHint()
    {
        return "";
    }

    public override string RenderValue()
    {
        var str = "";

        switch(State)
        {
            case NicheLifecyclePhase.Idle: str = "???"; break;
            case NicheLifecyclePhase.Innovation: str = "Innovation"; break;
            case NicheLifecyclePhase.Trending: str = "Trending"; break;
            case NicheLifecyclePhase.MassUse: str = "Mass use"; break;
            case NicheLifecyclePhase.Decay: str = "Decay"; break;
            case NicheLifecyclePhase.Death: str = "Death"; break;
        }

        return str + " phase"; // State.ToString();
    }

    NicheLifecyclePhase State => NicheUtils.GetMarketState(GameContext, SelectedNiche);
}
