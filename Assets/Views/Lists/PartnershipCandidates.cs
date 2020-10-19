using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PartnershipCandidates : ListView
{
    public override void SetItem<T>(Transform t, T entity)
    {
        t.GetComponent<PartnershipCandidateView>().SetEntity(entity as GameEntity);
    }

    // Start is called before the first frame update
    void OnEnable()
    {
        var possiblePartners = Companies.GetPartnershipCandidates(MyCompany, Q)
                //.OrderByDescending(c => CompanyUtils.GetPartnerability(MyCompany, c, GameContext).Sum());
                .OrderByDescending(c => Companies.GetCompanyBenefitFromTargetCompany(MyCompany, c, Q));

        SetItems(possiblePartners.ToArray());
    }
}
