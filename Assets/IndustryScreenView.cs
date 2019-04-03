using Assets.Utils;
using Entitas;
using System;
using UnityEngine;
using UnityEngine.UI;

public class IndustryScreenView : View
{
    IndustryType industryType;

    public Text IndustryName;
    public GameObject NichePrefab;
    public GameObject LinkPrefab;

    private void OnEnable()
    {
        Debug.Log("OnEnable IndustryScreenView");

        var niches = GetNiches();

        foreach (var n in niches)
        {
            Debug.Log("niche: " + n.niche.NicheType);
        }
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
