using Assets.Core;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class CompanyViewOnMainScreen : View
{
    public Text Name;
    public Hint CompanyHint;
    public Text Clients;
    public LinkToProjectView LinkToProjectView;

    public Image Image;
    public Image DarkImage;

    public Text Profitability;

    public RenderConceptProgress ConceptProgress;

    public HireWorker HireWorker;

    bool EnableDarkTheme;

    GameEntity company;

    public void SetEntity(GameEntity c, bool darkImage)
    {
        company = c;
        EnableDarkTheme = darkImage;

        bool hasControl = Companies.GetControlInCompany(MyCompany, c, GameContext) > 0;

        Name.text = c.company.Name; // .Substring(0, 1);
        Name.color = Visuals.GetColorFromString(hasControl ? VisualConstants.COLOR_CONTROL : VisualConstants.COLOR_NEUTRAL);
        SetEmblemColor();

        LinkToProjectView.CompanyId = c.company.Id;


        CompanyHint.SetHint(GetCompanyHint(hasControl));

        var clients = MarketingUtils.GetClients(company);
        Clients.text = Format.Minify(clients);

        var profit = Economy.GetProfit(GameContext, company.company.Id);
        Profitability.text = Format.Money(profit);
        Profitability.color = Visuals.GetColorFromString(profit > 0 ? VisualConstants.COLOR_POSITIVE : VisualConstants.COLOR_NEGATIVE);

        HireWorker.companyId = company.company.Id;
        var max = Economy.GetNecessaryAmountOfWorkers(company, GameContext);
        var workers = Teams.GetAmountOfWorkers(company, GameContext);
        HireWorker.GetComponentInChildren<Text>().text = $"Hire Worker ({workers}/{max})";
        HireWorker.gameObject.SetActive(workers < max);
    }

    string GetProfitDescription()
    {
        var profit = Economy.GetProfit(GameContext, company.company.Id);

        return profit > 0 ?
            Visuals.Positive($"Profit: +{Format.Money(profit)}") :
            Visuals.Negative($"Loss: {Format.Money(-profit)}");
    }
    
    void SetEmblemColor()
    {
        Image.color = Companies.GetCompanyUniqueColor(company.company.Id);

        var col = DarkImage.color;
        var a = EnableDarkTheme ? 219f : 126f;

        DarkImage.color = new Color(col.r, col.g, col.b, a / 255f);
        //DarkImage.gameObject.SetActive(DisableDarkTheme);
    }

    string GetCompanyHint(bool hasControl)
    {
        StringBuilder hint = new StringBuilder(company.company.Name);

        var position = Markets.GetPositionOnMarket(GameContext, company);

        // quality description
        var conceptStatus = Products.GetConceptStatus(company, GameContext);

        var concept = "???";

        switch (conceptStatus)
        {
            case ConceptStatus.Leader: concept = "Sets Trends!"; break;
            case ConceptStatus.Outdated: concept = "Outdated"; break;
            case ConceptStatus.Relevant: concept = "Relevant"; break;
        }

        //
        var level = Products.GetProductLevel(company);

        var clients = MarketingUtils.GetClients(company);

        var brand = (int)company.branding.BrandPower;

        hint.AppendLine($"\n\n");
        hint.AppendLine($"Clients: {Format.Minify(clients)} (#{position + 1})");
        hint.AppendLine($"Brand: {brand}");
        hint.AppendLine($"\nConcept: {level}LVL ({concept})");

        hint.AppendLine();
        hint.AppendLine();

        var profitDescription = GetProfitDescription();
        hint.AppendLine(profitDescription);

        if (hasControl)
            hint.AppendLine(Visuals.Colorize("\nWe control this company", VisualConstants.COLOR_CONTROL));

        return hint.ToString();
    }
}