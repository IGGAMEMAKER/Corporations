using Assets.Utils;
using Assets.Utils.Formatting;
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

        var partnerability = CompanyUtils.GetPartnerability(MyCompany, company, GameContext);
        var opinion = partnerability.Sum();

        CompanyName.text = company.company.Name;
        Opinion.text = Visuals.PositiveOrNegativeMinified(opinion);
        Opinion.gameObject.GetComponent<Hint>().SetHint(partnerability.ToString());

        GetComponent<LinkToProjectView>().CompanyId = company.company.Id;

        var industries = company.companyFocus.Industries.Select(EnumUtils.GetFormattedIndustryName);

        TargetIndustry.text = string.Join(", ", industries);

        BrandPowerGain.text = CompanyUtils.GetCompanyBenefitFromTargetCompany(MyCompany, company, GameContext).ToString();
    }
}
