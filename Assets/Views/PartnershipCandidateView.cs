﻿using Assets.Core;
using System.Linq;
using UnityEngine.UI;

public class PartnershipCandidateView : View
{
    GameEntity company;

    public Text CompanyName;
    public Text BrandPowerGain;
    public Text Opinion;
    public Text TargetIndustry;

    public void SetEntity(GameEntity gameEntity)
    {
        company = gameEntity;

        var partnerability = Companies.GetPartnerability(MyCompany, company, Q);
        var opinion = partnerability.Sum();

        CompanyName.text = company.company.Name;
        Opinion.text = Visuals.PositiveOrNegativeMinified(opinion);
        Opinion.gameObject.GetComponent<Hint>().SetHint(partnerability.ToString());

        GetComponent<LinkToProjectView>().CompanyId = company.company.Id;

        var industries = company.companyFocus.Industries
            .Select(i => Visuals.Colorize(
                Enums.GetFormattedIndustryName(i),
                MyCompany.companyFocus.Industries.Contains(i) ? Colors.COLOR_CONTROL : Colors.COLOR_WHITE)
                );

        TargetIndustry.text = string.Join(", ", industries);

        BrandPowerGain.text = Companies.GetCompanyBenefitFromTargetCompany(MyCompany, company, Q).ToString();
    }
}
