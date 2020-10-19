using Assets.Core;
using System.Linq;
using UnityEngine;

public class FillAcquisitionShareholders : ListView
{
    public override void SetItem<T>(Transform t, T entity)
    {
        int shareholderId = (int)(object)entity;
        t.GetComponent<VotingShareholderView>().SetEntity(shareholderId);



        var acquisitionComponent = t.GetComponent<RenderInvestorResponseToAcquisitionOffer>();
        if (acquisitionComponent != null)
            acquisitionComponent.SetEntity(shareholderId);

        var joinCorporationComponent = t.GetComponent<RenderInvestorResponseToJoinCorporationOffer>();
        if (joinCorporationComponent != null)
            joinCorporationComponent.SetEntity(shareholderId);



        var investor = Companies.GetInvestorById(Q, shareholderId);

        if (investor.hasHuman)
            t.gameObject.AddComponent<LinkToHuman>().SetHumanId(investor.human.Id);
        else
            t.gameObject.AddComponent<LinkToProjectView>().CompanyId = investor.company.Id;
    }



    public override void ViewRender()
    {
        base.ViewRender();

        SetItems(SelectedCompany.shareholders.Shareholders.Keys.ToArray());
    }
}
