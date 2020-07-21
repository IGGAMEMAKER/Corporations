using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CompanyViewInSegmentTab : View
{
    public Text CompanyValue;
    public Text AudienceLoyalty;

    public Image CompanyLogo;
    public Hint CompanyHint;
    public Image Border;

    public LinkToProjectView LinkToProjectView;

    public void SetEntity(GameEntity company)
    {
        var loyalty = Random.Range(-5, 10);
        AudienceLoyalty.text = Format.Sign(loyalty);
        AudienceLoyalty.color = Visuals.GetColorPositiveOrNegative(loyalty);

        var change = Marketing.GetAudienceChange(company, Q);
        CompanyValue.text = Format.SignOf(change) + Format.MinifyToInteger(change);
        //CompanyValue.color = Visuals.GetColorPositiveOrNegative(change);

        CompanyLogo.color = Companies.GetCompanyUniqueColor(company.company.Id);

        bool playerControlled = Companies.IsRelatedToPlayer(Q, company);
        Border.color = Visuals.GetColorFromString(playerControlled ? Colors.COLOR_CONTROL : Colors.COLOR_CONTROL_NO);

        var income = Economy.GetProductCompanyMaintenance(company, Q, true);
        var marketingBudget = income.Only("Marketing in").Sum();

        CompanyHint.SetHint(
            $"{RenderName(company)}" +
            $"\n\n" +
            $"Weekly Audience Growth:\n{Visuals.Colorize(Format.Minify(change), Visuals.GetColorPositiveOrNegative(change))}" +
            $"\n" +
            $"Marketing Budget:\n{Format.MinifyMoney(marketingBudget)}"
            );

        LinkToProjectView.CompanyId = company.company.Id;
    }
}
