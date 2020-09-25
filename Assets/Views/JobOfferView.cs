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

    internal void SetEntity(ExpiringJobOffer entity)
    {
        var human = SelectedHuman;

        CompanyTitle.text = RenderName(Companies.Get(Q, entity.CompanyId));
        Offer.text = Format.MinifyMoney(entity.JobOffer.Salary) + " / week";

        var opinion = (long)Teams.GetOpinionAboutOffer(human, entity);
        Opinion.text = Format.Sign(opinion);
        Opinion.color = Visuals.GetColorPositiveOrNegative(opinion);
    }
}
