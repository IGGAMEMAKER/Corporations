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

    string FormatNiches(NicheType n) => EnumUtils.GetFormattedNicheName(n);
    string FormatIndustries(IndustryType n) => EnumUtils.GetFormattedIndustryName(n);

    int AmountOfNiches => 3;

    void RenderIndustriesAmount()
    {
        var industries = entity.companyFocus.Industries;

        var text = "";

        if (industries.Count < AmountOfNiches)
            text = System.String.Join("\n", industries.Select(FormatIndustries));
        else
        {
            text = System.String.Join("\n", industries.Select(FormatIndustries).ToArray(), 0, AmountOfNiches);
            text += $"\nand {industries.Count - AmountOfNiches} more";
        }

        IndustryFocus.text = text;
    }

    void RenderNichesAmount()
    {
        var niches = entity.companyFocus.Niches;

        var text = "";

        if (niches.Count < AmountOfNiches)
        {
            text = System.String.Join("\n", niches.Select(FormatNiches));
        }
        else
        {
            text = System.String.Join("\n", niches.Select(FormatNiches).ToArray(), 0, AmountOfNiches);
            text += $"\nand {niches.Count - AmountOfNiches} more";
        }

        NicheFocus.text = text;
    }
}
