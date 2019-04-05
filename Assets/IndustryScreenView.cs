﻿using Assets.Utils;
using Assets.Utils.Formatting;
using Entitas;
using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class IndustryScreenView : View, IMenuListener
{
    IndustryType industryType;

    public Text IndustryName;
    public NicheListView NicheListView;

    void Start()
    {
        ListenMenuChanges(this);
    }

    Predicate<GameEntity> FilterNichesByIndustry(IndustryType industry)
    {
        return n => n.niche.IndustryType == industry && n.niche.NicheType != NicheType.None;
    }

    GameEntity[] GetNiches()
    {
        var niches = GameContext.GetEntities(GameMatcher.Niche);

        return Array.FindAll(niches, FilterNichesByIndustry(industryType));
    }

    void Render()
    {
        industryType = MenuUtils.GetIndustry(GameContext);
        Debug.Log("IndustryScreen Render " + industryType.ToString());

        var niches = GetNiches();

        string concat = String.Join(",", niches.Select(e => e.niche.NicheType.ToString()).ToArray());

        Debug.Log("Found niches: " + concat);

        NicheListView.SetItems(niches);

        IndustryName.text = MarketFormattingUtils.GetFormattedIndustryName(industryType) + " Industry";
    }

    void IMenuListener.OnMenu(GameEntity entity, ScreenMode screenMode, object data)
    {
        if (screenMode == ScreenMode.IndustryScreen)
            Render();
    }
}
