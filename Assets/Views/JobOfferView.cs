using Assets.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
        Offer.text = Format.MinifyMoney(entity.JobOffer.Salary) + " / week";

        var opinionBonus = Teams.GetOpinionAboutOffer(human, entity);
        var opinion = opinionBonus.Sum();

        Opinion.text = Format.Sign(opinion);
        Opinion.color = Visuals.GetColorPositiveOrNegative(opinion);
        OpinionHint.SetHint(opinionBonus.ToString());
    }
}
