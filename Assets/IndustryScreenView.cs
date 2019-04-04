using Assets.Utils;
using Entitas;
using System;
using UnityEngine;
using UnityEngine.UI;

public class IndustryScreenView : View
{
    IndustryType industryType;

    public Text IndustryName;
    public NicheListView NicheListView;

    private void OnEnable()
    {
        var niches = GetNiches();

        NicheListView.SetItems(niches);
    }

    Predicate<GameEntity> FilterNichesByIndustries(IndustryType industry)
    {
        return n => n.niche.IndustryType == industry && n.niche.NicheType != NicheType.None;
    }

    GameEntity[] GetNiches()
    {
        var industry = MenuUtils.GetIndustry(GameContext);

        var niches = GameContext.GetEntities(GameMatcher.Niche);

        return Array.FindAll(niches, FilterNichesByIndustries(industry));
    }

    void Update()
    {
        Render();
    }

    void Render()
    {
        industryType = MenuUtils.GetIndustry(GameContext);

        IndustryName.text = industryType.ToString() + " Industry";
    }

}
