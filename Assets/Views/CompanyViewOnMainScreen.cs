using Assets.Core;
using System.Text;
using UnityEngine.UI;

public class CompanyViewOnMainScreen : View
{
    public Text Expertise;

    GameEntity company;

    public override void ViewRender()
    {
        base.ViewRender();

        var flagship = Companies.GetFlagship(Q, MyCompany);

        if (flagship == null)
            return;

        company = flagship;

        // team speed
        var effeciency = Products.GetTeamEffeciency(Q, company);
        var max = Products.GetNecessaryAmountOfWorkers(company, Q);
        var workers = Teams.GetTeamSize(company, Q);


        Expertise.text = $"{effeciency}%";
        Expertise.color = Visuals.GetGradientColor(0, 100, effeciency);
    }

    // useless
    string GetCompanyHint()
    {
        StringBuilder hint = new StringBuilder(company.company.Name);

        var position = Markets.GetPositionOnMarket(Q, company);

        // quality description
        var conceptStatus = Products.GetConceptStatus(company, Q);

        var concept = "???";

        switch (conceptStatus)
        {
            case ConceptStatus.Leader: concept = "Sets Trends!"; break;
            case ConceptStatus.Outdated: concept = "Outdated"; break;
            case ConceptStatus.Relevant: concept = "Relevant"; break;
        }

        //
        var level = Products.GetProductLevel(company);

        var clients = Marketing.GetClients(company);

        var brand = (int)company.branding.BrandPower;

        hint.AppendLine();
        hint.AppendLine();

        hint.AppendLine($"Clients: {Format.Minify(clients)} (#{position + 1})");
        hint.AppendLine($"Brand: {brand}");

        hint.AppendLine();

        hint.AppendLine($"Concept: {level}LVL ({concept})");

        hint.AppendLine();
        hint.AppendLine();

        hint.AppendLine(GetProfitDescription());

        return hint.ToString();
    }

    // useless
    string GetProfitDescription()
    {
        var profit = Economy.GetProfit(Q, company.company.Id);

        return profit > 0 ?
            Visuals.Positive($"Profit: +{Format.Money(profit)}") :
            Visuals.Negative($"Loss: {Format.Money(-profit)}");
    }
}