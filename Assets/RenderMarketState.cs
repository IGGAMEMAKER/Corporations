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
        return State.ToString();
    }

    NicheLifecyclePhase State => NicheUtils.GetMarketState(GameContext, SelectedNiche);
}
