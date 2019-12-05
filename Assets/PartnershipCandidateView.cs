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

        CompanyName.text = gameEntity.company.Name;
    }
}
