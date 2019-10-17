using Assets.Utils;
using Entitas;
using System.Collections.Generic;
using System.Linq;

public class ProcessAcquisitionOffersSystem : OnWeekChange
{
    public ProcessAcquisitionOffersSystem(Contexts contexts) : base(contexts)
    {
    }

    protected override void Execute(List<GameEntity> entities)
    {
        var offers = gameContext.GetEntities(GameMatcher.AcquisitionOffer);

        // companies, who are acquisition targets
        var targets = new List<int>();

        foreach (var o in offers)
        {
            if (!targets.Contains(o.acquisitionOffer.CompanyId))
                targets.Add(o.acquisitionOffer.CompanyId);
        }

        foreach (var t in targets)
            AnalyzeOffers(t);
    }

    void AnalyzeOffers (int companyId)
    {
        var offers = CompanyUtils.GetAcquisitionOffersToCompany(gameContext, companyId);

        var maxOffer = offers
            .OrderByDescending(o => o.acquisitionOffer.BuyerOffer.Price)
            .First();

        var maxCost = maxOffer.acquisitionOffer.BuyerOffer.Price;

        for (var i = 0; i < offers.Count(); i++)
        {
            var o = offers[i];
            var offer = o.acquisitionOffer;
            //o.ReplaceAcquisitionOffer(offer.CompanyId, offer.BuyerId, offer.RemainingTries, offer.RemainingDays, offer.AcquisitionConditions.)
        }
    }
}