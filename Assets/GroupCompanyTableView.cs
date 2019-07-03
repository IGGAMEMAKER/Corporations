using Assets.Utils;
using Assets.Utils.Formatting;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

        CompanyTypeName.text = EnumUtils.GetFormattedCompanyType(entity.company.CompanyType);

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
        {
            text = System.String.Join("\n", industries.Select(n => EnumUtils.GetFormattedIndustryName(n)).ToArray(), 0, 3);
            text += $"\nand {industries.Count - 3} more";
        }

        IndustryFocus.text = text;
    }

    void RenderNichesAmount()
    {
        var niches = entity.companyFocus.Niches;

        var text = "";

        if (niches.Count < 3)
        {
            text = System.String.Join("\n", niches);
        }
        else
        {
            text = System.String.Join("\n", niches.Select(n => EnumUtils.GetFormattedNicheName(n)).ToArray(), 0, 2);
            text += $"\nand {niches.Count - 3} more";
        }

        NicheFocus.text = text;
    }
}
