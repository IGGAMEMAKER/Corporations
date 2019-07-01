using Assets.Utils;
using Assets.Utils.Formatting;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GroupCompanyTableView : View
{
    [SerializeField] Text CompanyTypeName;
    [SerializeField] Text NicheFocus;
    [SerializeField] Text IndustryFocus;

    GameEntity entity;

    public void SetEntity(GameEntity company)
    {
        entity = company;

        Render();
    }

    public void Render()
    {
        if (entity == null)
            return;

        CompanyTypeName.text = $"({EnumUtils.GetFormattedCompanyType(entity.company.CompanyType)}";

        RenderNichesAmount();
        RenderIndustriesAmount();
    }

    void RenderIndustriesAmount()
    {
        var industries = entity.companyFocus.Industries;

        var text = "";

        if (industries.Count < 3)
            text = System.String.Join("\n", industries);
        else
            text = industries.Count.ToString();

        IndustryFocus.text = text;
    }

    void RenderNichesAmount()
    {
        var niches = entity.companyFocus.Niches;

        var text = "";

        if (niches.Count < 3)
            text = System.String.Join("\n", niches);
        else
            text = niches.Count.ToString();

        NicheFocus.text = text;
    }
}
