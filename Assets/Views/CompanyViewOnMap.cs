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

    public Text CompanyGrowth;

    public Image Image;
    public Image DarkImage;
    public Image RelevancyImage;

    public Text Profitability;

    public Text PositionOnMarket;

    public ShowProductChanges ShowProductChanges;

    public Transform AnimationPosition;

    bool EnableDarkTheme;

    GameEntity company;

    public void SetEntity(GameEntity c, bool darkImage, bool ShowIncome)
    {
        company = c;
        EnableDarkTheme = darkImage;

        bool hasControl = Companies.GetControlInCompany(MyCompany, c, Q) > 0;

        Name.text = c.company.Name; // .Substring(0, 1);
        Name.color = Visuals.GetColorFromString(hasControl ? Colors.COLOR_CONTROL : Colors.COLOR_NEUTRAL);
        SetEmblemColor();

        LinkToProjectView.CompanyId = c.company.Id;
        ShowProductChanges.SetEntity(company);

        var change = Marketing.GetAudienceChange(c, Q);

        var changeBonus = Marketing.GetAudienceChange(company, Q, true);


        CompanyGrowth.text = Format.SignOf(change) + Format.Minify(change);
        CompanyGrowth.color = Visuals.GetColorPositiveOrNegative(change);
        CompanyGrowth.GetComponent<Hint>().SetHint($"Audience change: <b>{Format.Minify(change)}</b>");

        CompanyHint.SetHint(GetCompanyHint(hasControl));

        var clients = Marketing.GetClients(company);
        Concept.text = ShowIncome ? Format.MinifyMoney(Economy.GetCompanyIncome(Q, company)) : Format.Minify(clients); // Products.GetProductLevel(c) + "LVL";

        var position = Markets.GetPositionOnMarket(Q, company);
        PositionOnMarket.text = $"#{position + 1}";

        var marketShare = Companies.GetMarketShareOfCompanyMultipliedByHundred(c, Q);
        var lastMetrics = CompanyStatisticsUtils.GetLastMetrics(company, 1);

        var lastShare = Random.Range(2f, 30f);
        var shareChange = marketShare - lastShare;

        Animate(Visuals.Colorize(Format.SignOf((long)shareChange) + shareChange.ToString("0.0%"), shareChange >= 0), AnimationPosition);

        if (Profitability != null)
        {

            //var shareChange = 1;
            //Profitability.text = Visuals.DescribeValueWithText(shareChange, marketShare + "%", marketShare + "%", "");

            Profitability.text = Visuals.Positive(marketShare + "%");
        }
    }

    string GetProfitDescription()
    {
        var profit = Economy.GetProfit(Q, company.company.Id);

        return profit > 0 ?
            Visuals.Positive($"Profit: +{Format.Money(profit)}") :
            Visuals.Negative($"Loss: {Format.Money(-profit)}");
    }

    Color GetMarketRelevanceColor()
    {
        var profit = Economy.GetProfit(Q, company);

        if (profit > 0)
            return Visuals.GetColorFromString(Colors.COLOR_POSITIVE);

        if (profit == 0)
            return Visuals.GetColorFromString(Colors.COLOR_NEUTRAL);

        if (profit < 0)
            return Visuals.GetColorFromString(Colors.COLOR_NEGATIVE);

        var concept = "";
        switch (Products.GetConceptStatus(company, Q))
        {
            case ConceptStatus.Leader: concept = Colors.COLOR_POSITIVE; break;
            case ConceptStatus.Outdated: concept = Colors.COLOR_NEGATIVE; break;
            case ConceptStatus.Relevant: concept = Colors.COLOR_NEUTRAL; break;
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

        var position = Markets.GetPositionOnMarket(Q, company);

        //
        var clients = Marketing.GetClients(company);

        var change = Marketing.GetAudienceChange(company, Q);

        //CompanyGrowth.text = Format.SignOf(change) + Format.Minify(change);
        //CompanyGrowth.color = Visuals.GetColorPositiveOrNegative(change);
        //CompanyGrowth.GetComponent<Hint>().SetHint($"Audience change: <b>{Format.Minify(change)}</b>");

        hint.AppendLine($"\n\n");
        hint.AppendLine($"<b>Users</b>: {Format.Minify(clients)} (<b>#{position + 1}</b>)");
        hint.AppendLine($"Audience change: <b>{Format.Minify(change)}</b>");

        hint.AppendLine(GetProfitDescription());

        if (hasControl)
            hint.AppendLine(Visuals.Colorize("\nYou control this company", Colors.COLOR_CONTROL));

        return hint.ToString();
    }
}
