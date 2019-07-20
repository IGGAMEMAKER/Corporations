using Assets.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CompanyViewOnMarket : View
{
    public Text Name;
    public Hint CompanyHint;
    public LinkToProjectView LinkToProjectView;

    public Image Image;

    GameEntity company;

    public void SetEntity(GameEntity c)
    {
        company = c;

        int companyId = c.company.Id;
        //var c = CompanyUtils.GetCompanyById(GameContext, companyId);

        var name = c.company.Name;
        Name.text = name.Substring(0, 1);

        CompanyHint.SetHint(GetCompanyHint());

        LinkToProjectView.CompanyId = companyId;


        // control
        bool hasControl = CompanyUtils.GetControlInCompany(MyCompany, c, GameContext) > 0;
        var a = hasControl ? 1f : 0.25f;

        var color = Image.color;

        Image.color = new Color(color.r, color.g, color.b, a);

        Name.color = Visuals.Color(hasControl ? VisualConstants.COLOR_GOLD : VisualConstants.COLOR_NEUTRAL);
    }

    string GetCompanyHint()
    {
        var hint = $"{company.company.Name}\n{}";

        return hint;
    }
}
