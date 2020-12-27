using Assets.Core;
using UnityEngine.UI;

public class CompanyViewInSegmentTab : View
{
    public Text CompanyValue;
    public Text AudienceLoyalty;

    public Image CompanyLogo;
    public Hint CompanyHint;
    public Image Border;

    public Text CompanyName;
    public Text CompanyStatsDescription;

    public LinkToProjectView LinkToProjectView;

    public void SetEntity(GameEntity company, ProductPositioning positioning)
    {
        var appQuality = (int)Marketing.GetPositioningQuality(company).Sum(); // (int)Marketing.GetSegmentLoyalty(company, segmentId); // Random.Range(-5, 10);

        AudienceLoyalty.text = appQuality.ToString();
        AudienceLoyalty.color = Visuals.GetColorPositiveOrNegative(appQuality);

        var change = Marketing.GetAudienceChange(company, Q);
        CompanyValue.text = Format.SignOf(change) + Format.MinifyToInteger(change);
        //CompanyValue.color = Visuals.GetColorPositiveOrNegative(change);

        CompanyLogo.color = Companies.GetCompanyUniqueColor(company.company.Id);

        bool playerControlled = Companies.IsDirectlyRelatedToPlayer(Q, company);
        Border.color = Visuals.GetColorFromString(playerControlled ? Colors.COLOR_CONTROL : Colors.COLOR_CONTROL_NO);

        var income = Economy.GetProductCompanyMaintenance(company, true);
        var marketingBudget = income.Only("Marketing in").Sum();

        CompanyHint.SetHint(
            $"{RenderName(company)}" +
            $"\n\n" +
            $"<size=40>App quality={Visuals.Positive(appQuality.ToString())}</size>\n\n"+
            $"Weekly Audience Growth:\n{Visuals.Colorize(Format.Minify(change), Visuals.GetColorPositiveOrNegative(change))}" +
            $"\n" +
            $"Marketing Budget:\n{Format.Money(marketingBudget, true)}"
            );

        CompanyName.text = company.company.Name;
        CompanyStatsDescription.text = $"{Format.SignOf(change) + Format.MinifyToInteger(change)} users weekly";
        CompanyStatsDescription.color = Visuals.GetColorPositiveOrNegative(change);

        var clients = Marketing.GetUsers(company);
        CompanyValue.text = Format.MinifyToInteger(clients);

        LinkToProjectView.CompanyId = company.company.Id;
    }
}
