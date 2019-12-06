using Assets.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PartnershipCandidateView : View
{
    GameEntity GameEntity;

    public Text CompanyName;
    public Text BrandPowerGain;
    public Text Opinion;
    public Text TargetIndustry;

    public void SetEntity(GameEntity gameEntity)
    {
        GameEntity = gameEntity;

        var partnerability = CompanyUtils.GetPartnerability(MyCompany, gameEntity, GameContext);
        var opinion = partnerability.Sum();

        CompanyName.text = gameEntity.company.Name;
        Opinion.text = Visuals.PositiveOrNegativeMinified(opinion);
        Opinion.gameObject.GetComponent<Hint>().SetHint(partnerability.ToString());

        GetComponent<LinkToProjectView>().CompanyId = gameEntity.company.Id;
    }
}
