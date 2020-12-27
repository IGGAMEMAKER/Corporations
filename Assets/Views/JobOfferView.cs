using Assets.Core;
using UnityEngine.UI;

public class JobOfferView : View
{
    public Text CompanyTitle;
    public Text Offer;
    public Text Opinion;
    public Hint OpinionHint;

    internal void SetEntity(ExpiringJobOffer entity)
    {
        var human = SelectedHuman;

        CompanyTitle.text = RenderName(Companies.Get(Q, entity.CompanyId));
        Offer.text = Format.Money(entity.JobOffer.Salary, true) + " / week";

        var opinionBonus = Teams.GetOpinionAboutOffer(human, entity);
        var opinion = (int)opinionBonus.Sum();

        Opinion.text = Format.Sign(opinion);
        Opinion.color = Visuals.GetColorPositiveOrNegative(opinion);
        OpinionHint.SetHint(opinionBonus.ToString());
    }
}
