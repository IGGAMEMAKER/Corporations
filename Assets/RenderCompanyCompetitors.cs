using Assets.Core;
using UnityEngine;
using UnityEngine.UI;

public class RenderCompanyCompetitors : ListView
{
    public override void SetItem<T>(Transform t, T entity, object data = null)
    {
        // previous prefab: StrategicPartnershipView

        var company = entity as GameEntity;
        var companyId = company.company.Id;

        //t.GetComponent<LinkToProjectView>().CompanyId = companyId;
        //t.GetComponent<RenderPartnerName>().SetCompanyId(companyId);

        t.gameObject.AddComponent<Button>();
        t.gameObject.AddComponent<LinkToProjectView>().CompanyId = companyId;

        string text = company.company.Name;

        if (Companies.IsRelatedToPlayer(Q, company))
        {
            text = Visuals.Colorize(text, Colors.COLOR_GOLD);
        }

        if (company.hasProduct)
        {
            var innovativeness = Products.GetInnovationChance(company, Q);
            text += $" (Innovativeness: {innovativeness}%)";
        }
        else
        {
            var cost = Economy.GetCompanyCost(Q, company);
            text += $"({Format.Money(cost)})";
        }

        t.gameObject.GetComponent<MockText>().SetEntity(text);
    }

    public override void ViewRender()
    {
        base.ViewRender();

        var competitors = Companies.GetCompetitorsOfCompany(SelectedCompany, Q, true);

        SetItems(competitors);
    }
}