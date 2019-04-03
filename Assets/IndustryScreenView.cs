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

    Predicate<GameEntity> FilterIndustries(IndustryType industry)
    {
        return n => n.niche.IndustryType == industry && n.niche.NicheType != NicheType.None;
    }

    GameEntity[] GetNiches()
    {
        var industry = MenuUtils.GetIndustry(GameContext);


        return Array.FindAll(GameContext.GetEntities(GameMatcher.Niche), FilterIndustries(industry));
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
