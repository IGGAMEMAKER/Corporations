using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class CompanyViewOnAudienceMap : View
{
    public Text Name;
    public Hint CompanyHint;
    public LinkToProjectView LinkToProjectView;

    public Image Image;

    public Text Loyalty;
    public Hint LoyaltyHint;

    GameEntity company;

    public void SetEntity(GameEntity c, int segmentId)
    {
        company = c;

        bool hasControl = Companies.GetControlInCompany(MyCompany, c, Q) > 0;

        var shortName = c.company.Name.Substring(0, 1) + c.company.Name.FirstOrDefault(char.IsDigit);

        Name.text = shortName;
        Name.color = Visuals.GetColorFromString(hasControl ? Colors.COLOR_CONTROL : Colors.COLOR_NEUTRAL);
        SetEmblemColor();

        LinkToProjectView.CompanyId = c.company.Id;

        var loyalty = Marketing.GetSegmentLoyalty(Q, company, segmentId, true);
        Loyalty.text = loyalty.Sum().ToString("0");
        LoyaltyHint.SetHint(loyalty.SortByModule().ToString());

        CompanyHint.SetHint(GetCompanyHint(hasControl));

        var position = Markets.GetPositionOnMarket(Q, company);

        var max = 1.8f;
        var scale = Mathf.Clamp(max / (1 + position), 0.8f, max);

        transform.localScale = new Vector3(scale, scale, 1);
    }

    string GetProfitDescription()
    {
        var profit = Economy.GetProfit(Q, company.company.Id);

        return profit > 0 ?
            Visuals.Positive($"Profit: +{Format.Money(profit)}") :
            Visuals.Negative($"Loss: {Format.Money(-profit)}");
    }

    void SetEmblemColor()
    {
        Image.color = Companies.GetCompanyUniqueColor(company.company.Id);
    }

    string GetCompanyHint(bool hasControl)
    {
        StringBuilder hint = new StringBuilder(company.company.Name);

        var position = Markets.GetPositionOnMarket(Q, company);

        var clients = Marketing.GetClients(company);

        var change = Marketing.GetAudienceChange(company, Q);

        var changeFormatted = $"<b>{Format.SignOf(change) + Format.Minify(change)}</b> weekly";

        hint.Append($" <b>#{position + 1}</b>");
        hint.AppendLine($"\n\n");
        hint.AppendLine($"Users: <b>{Format.Minify(clients)}</b> {Visuals.Colorize(changeFormatted, change >=0)}");
        //hint.AppendLine($"Audience change: ");

        hint.AppendLine(GetProfitDescription());

        if (hasControl)
            hint.AppendLine(Visuals.Colorize("\nYou control this company", Colors.COLOR_CONTROL));

        return hint.ToString();
    }
}
