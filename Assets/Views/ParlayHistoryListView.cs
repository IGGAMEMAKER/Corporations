using Assets.Core;
using System.Linq;
using UnityEngine;

public class ParlayHistoryListView : ListView
{
    public override void SetItem<T>(Transform t, T entity)
    {
        t.GetComponent<MockText>().SetEntity(entity as string);
    }

    AcquisitionConditions Conditions => AcquisitionOffer.BuyerOffer;

    AcquisitionOfferComponent AcquisitionOffer
    {
        get
        {
            var offer = Companies.GetAcquisitionOffer(Q, SelectedCompany, MyCompany);

            return offer?.acquisitionOffer ?? null;
        }
    }

    public override void ViewRender()
    {
        base.ViewRender();

        var offers = Companies.GetAcquisitionOffersToCompany(Q, SelectedCompany)
            .Select(o => $"{Companies.GetName(Q, o.acquisitionOffer.CompanyId)}: {Format.Money(o.acquisitionOffer.BuyerOffer)}")
            .ToList();

        var acquisitionOffer = AcquisitionOffer;

        if (acquisitionOffer != null)
        {
            var conditions = acquisitionOffer.BuyerOffer;
            var seller = acquisitionOffer.SellerOffer;
            long price = conditions.Price;

            var cost = Economy.CostOf(SelectedCompany, Q);


            var sellerPrice = $"They want {Format.Money(seller.Price)} (Real valuation = {Format.Money(cost)})";

            offers.Add(sellerPrice);
        }

        SetItems(offers);
    }
}
