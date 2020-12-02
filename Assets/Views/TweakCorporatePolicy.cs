using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[RequireComponent(typeof(LazyUpdate))]
public class TweakCorporatePolicy : ButtonController
{
    public CorporatePolicy CorporatePolicy;
    public bool Increment = true;

    public override void Execute()
    {
        if (Increment)
            Companies.IncrementCorporatePolicy(Q, MyCompany, CorporatePolicy);
        else
            Companies.DecrementCorporatePolicy(Q, MyCompany, CorporatePolicy);

        DescribeChange();
    }

    public void SetSettings(CorporatePolicy policy, bool increment)
    {
        CorporatePolicy = policy;
        Increment = increment;
    }

    void DescribeChange()
    {
        if (CorporatePolicy == CorporatePolicy.DoOrDelegate)
        {
            DescribeDelegationChanges();
        }
    }

    void DescribeDelegationChanges()
    {
        var newValue = Companies.GetPolicyValue(MyCompany, CorporatePolicy);

        var text = "";

        if (Increment)
        {
            switch (newValue)
            {
                case 1: text = "You can add TEAMS now!"; break;
                case 2: text = "You can promote teams to BIG TEAMS!"; break;
                case 3: text = "You can promote big teams to DEPARTMENTS!"; break;
                case 4: text = "You can change corporate culture now!"; break;
                case 5: text = "You can buy companies now!"; break;
                case 6: text = "You can have up to 4 companies now!"; break;
                case 7: text = "You can own groups"; break;
                case 8: text = "You can form a CORPORATION!"; break;
                case 9: text = "You can run an IPO!"; break;
            }

            NotificationUtils.AddSimplePopup(Q, Visuals.Positive(text), "Corporate culture changed");
        }

    }
}
