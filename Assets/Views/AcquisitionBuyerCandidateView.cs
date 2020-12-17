using Assets.Core;
using UnityEngine.UI;

public class AcquisitionBuyerCandidateView : View
{
    public Text CompanyName;
    public Text Offer;

    public void SetEntity(GameEntity offer)
    {
        var investorId = offer.acquisitionOffer.BuyerId;
        var buyerCompanyId = Investments.GetCompanyByInvestorId(Q, investorId).company.Id;

        GetComponent<LinkToProjectView>().CompanyId = buyerCompanyId;
        Offer.text = Format.Money(offer.acquisitionOffer.BuyerOffer.Price);

        CompanyName.text = Companies.Get(Q, buyerCompanyId).company.Name;
    }
}
