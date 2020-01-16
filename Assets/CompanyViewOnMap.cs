using Assets.Core;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class CompanyViewOnMap : View
{
    public Text Name;
    public Hint CompanyHint;
    public Text Concept;
    public LinkToProjectView LinkToProjectView;

    public Image Image;
    public Image DarkImage;
    public Image RelevancyImage;

    public Text Profitability;

    public RenderDumpingHint Dumping;

    public RenderConceptProgress ConceptProgress;

    public Text PositionOnMarket;

    public GameObject AggressiveMarketing;

    public ShowProductChanges ShowProductChanges;

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
        ShowProductChanges.SetEntity(company);


        var isRelatedToPlayer = Companies.IsRelatedToPlayer(GameContext, c);
        ConceptProgress.SetCompanyId(c.company.Id);
        //ConceptProgress.gameObject.SetActive(isRelatedToPlayer);

        CompanyHint.SetHint(GetCompanyHint(hasControl));

        var clients = MarketingUtils.GetClients(company);
        Concept.text = Format.Minify(clients); // Products.GetProductLevel(c) + "LVL";

        var position = Markets.GetPositionOnMarket(GameContext, company);
        PositionOnMarket.text = $"#{position + 1}";
        var level = Products.GetProductLevel(company);
        PositionOnMarket.text = $"{level}LVL";


        Dumping.SetCompanyId(c.company.Id);

        if (Profitability != null)
        {
            var profit = Economy.GetProfit(GameContext, company.company.Id);

            var marketShare = Companies.GetMarketShareOfCompanyMultipliedByHundred(c, GameContext);
            var shareChange = 1;
            bool isGrowing = Companies.IsCompanyGrowing(company, GameContext);

            //Profitability.text = Visuals.DescribeValueWithText(shareChange, marketShare + "%", marketShare + "%", "");
            Profitability.text = Visuals.Colorize(marketShare + "%", isGrowing);
            Profitability.text = Visuals.Positive(marketShare + "%");
            //Profitability.GetComponent<Hint>().SetHint(
            //    profit > 0 ?
            //    Visuals.Positive($"This company is profitable!\nProfit: +{Format.Money(profit)}") :
            //    Visuals.Negative($"This company loses {Format.Money(-profit)} each month!")
            //    );
        }

        var financing = Economy.GetMarketingFinancing(company);
        AggressiveMarketing.SetActive(financing == Products.GetMaxFinancing);
    }

    string GetProfitDescription()
    {
        var profit = Economy.GetProfit(GameContext, company.company.Id);

        return profit > 0 ?
            Visuals.Positive($"Profit: +{Format.Money(profit)}") :
            Visuals.Negative($"Loss: {Format.Money(-profit)}");
    }

    Color GetMarketRelevanceColor()
    {
        var profit = Economy.GetProfit(GameContext, company);

        if (profit > 0)
            return Visuals.GetColorFromString(VisualConstants.COLOR_POSITIVE);

        if (profit == 0)
            return Visuals.GetColorFromString(VisualConstants.COLOR_NEUTRAL);

        if (profit < 0)
            return Visuals.GetColorFromString(VisualConstants.COLOR_NEGATIVE);

        var concept = "";
        switch (Products.GetConceptStatus(company, GameContext))
        {
            case ConceptStatus.Leader: concept = VisualConstants.COLOR_POSITIVE; break;
            case ConceptStatus.Outdated: concept = VisualConstants.COLOR_NEGATIVE; break;
            case ConceptStatus.Relevant: concept = VisualConstants.COLOR_NEUTRAL; break;
        }

        return Visuals.GetColorFromString(concept);
    }

    void SetEmblemColor()
    {
        Image.color = Companies.GetCompanyUniqueColor(company.company.Id);

        var col = DarkImage.color;
        var a = EnableDarkTheme ? 219f : 126f;

        DarkImage.color = new Color(col.r, col.g, col.b, a / 255f);
        //DarkImage.gameObject.SetActive(DisableDarkTheme);

        if (RelevancyImage != null)
            RelevancyImage.color = GetMarketRelevanceColor();
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
            case ConceptStatus.Leader:      concept = "Sets Trends!"; break;
            case ConceptStatus.Outdated:    concept = "Outdated"; break;
            case ConceptStatus.Relevant:    concept = "Relevant"; break;
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

        //var posTextual = Markets.GetCompanyPositioning(company, GameContext);
        //hint.AppendLine($"\nPositioning: {posTextual}");

        ////var expertise = CompanyUtils.GetCompanyExpertise(company);
        //var expertise = company.expertise.ExpertiseLevel + " LVL";
        //hint.AppendLine($"\nExpertise: {expertise}");


        if (hasControl)
            hint.AppendLine(Visuals.Colorize("\nWe control this company", VisualConstants.COLOR_CONTROL));

        return hint.ToString();
    }
}
