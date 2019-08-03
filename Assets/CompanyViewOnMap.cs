using Assets.Utils;
using Assets.Utils.Formatting;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class CompanyViewOnMap : View
{
    public Text Name;
    public Hint CompanyHint;
    public LinkToProjectView LinkToProjectView;

    public Image Image;
    public Image DarkImage;

    bool EnableDarkTheme;

    GameEntity company;

    public void SetEntity(GameEntity c, bool darkImage)
    {
        company = c;
        EnableDarkTheme = darkImage;

        bool hasControl = CompanyUtils.GetControlInCompany(MyCompany, c, GameContext) > 0;

        Name.text = c.company.Name.Substring(0, 1);
        Name.color = Visuals.Color(hasControl ? VisualConstants.COLOR_CONTROL : VisualConstants.COLOR_NEUTRAL);

        LinkToProjectView.CompanyId = c.company.Id;

        CompanyHint.SetHint(GetCompanyHint(hasControl));

        SetEmblemColor();
    }

    void SetEmblemColor()
    {
        Image.color = CompanyUtils.GetCompanyUniqueColor(company.company.Id);

        var col = DarkImage.color;
        var a = EnableDarkTheme ? 219f : 126f;

        DarkImage.color = new Color(col.r, col.g, col.b, a / 255f);
        //DarkImage.gameObject.SetActive(DisableDarkTheme);
    }

    string GetCompanyHint(bool hasControl)
    {
        StringBuilder hint = new StringBuilder(company.company.Name);

        var position = NicheUtils.GetPositionOnMarket(GameContext, company);
        var nicheName = EnumUtils.GetFormattedNicheName(company.product.Niche);
        hint.AppendFormat("\n\n#{0} on market", position + 1, nicheName);

        var level = ProductUtils.GetProductLevel(company);
        hint.AppendLine($"\nApp quality: {level}");

        var clients = MarketingUtils.GetClients(company);
        hint.AppendLine($"Clients: {clients}");

        var posTextual = NicheUtils.GetCompanyPositioning(company, GameContext);
        hint.AppendLine($"\nPositioning: {posTextual}");
        var expertise = CompanyUtils.GetCompanyExpertise(company);
        hint.AppendLine($"\nExpertise: {expertise}%");
        var brand = company.branding.BrandPower;
        hint.AppendLine($"Brand strength: {brand}");

        if (hasControl)
            hint.AppendLine(Visuals.Colorize("\nWe control this company", VisualConstants.COLOR_CONTROL));

        return hint.ToString();
    }
}
