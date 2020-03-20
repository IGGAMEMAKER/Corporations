using Assets.Core;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CompanyViewOnMainScreen : View
{
    public Text Profitability;

    public HireWorker HireWorker;
    public Text Expertise;

    GameEntity company;

    public void SetEntity(GameEntity c, bool darkImage)
    {
        company = c;

        Render();
    }

    public override void ViewRender()
    {
        base.ViewRender();

        var flagship = Companies.GetFlagship(Q, MyCompany);

        if (flagship == null)
            return;

        company = flagship;

        Render();
    }

    void Render()
    {
        if (company == null)
            return;

        var id = company.company.Id;

        var bonus = GetProfitDescriptionFull();
        var profit = Economy.GetProfit(Q, id);

        Profitability.text = Format.Money(profit);
        Profitability.color = Visuals.GetColorPositiveOrNegative(profit);
        Profitability.GetComponent<Hint>().SetHint(bonus.ToString());


        // team speed
        var effeciency = Products.GetTeamEffeciency(Q, company);
        var max = Products.GetNecessaryAmountOfWorkers(company, Q);
        var workers = Teams.GetAmountOfWorkers(company, Q);


        Expertise.text = $"Team Speed: {effeciency}%";
        Expertise.color = Visuals.GetGradientColor(0, 100, effeciency);

        HireWorker.companyId = id;
        HireWorker.GetComponentInChildren<TextMeshProUGUI>().text = $"Hire Worker ({workers}/{max})";
        HireWorker.GetComponentInChildren<Button>().interactable = workers < max;
        HireWorker.GetComponentInChildren<Hint>().SetHint(
            workers < max
            ?
            "Hiring workers will increase development speed\n\n" + Visuals.Positive("Press <b>LEFT SHIFT</b> to hire max amount of workers")
            :
            Visuals.Negative("You have reached max limit of workers") //+ "\n\nTo increase this limit, hire TOP managers"
            );
    }

    Bonus<long> GetProfitDescriptionFull()
    {
        var income = Economy.GetCompanyIncome(Q, company);

        var bonus = new Bonus<long>("Balance change")
            .Append("Income", income);

        var prodMnt = Economy.GetProductCompanyMaintenance(company, Q, true);

        foreach (var p in prodMnt.bonusDescriptions)
            bonus.AppendAndHideIfZero(p.Name, -p.Value);

        return bonus;
    }

    string GetProfitDescription()
    {
        var profit = Economy.GetProfit(Q, company.company.Id);

        return profit > 0 ?
            Visuals.Positive($"Profit: +{Format.Money(profit)}") :
            Visuals.Negative($"Loss: {Format.Money(-profit)}");
    }

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
}