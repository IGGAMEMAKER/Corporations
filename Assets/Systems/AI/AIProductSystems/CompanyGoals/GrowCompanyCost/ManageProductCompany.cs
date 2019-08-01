using Assets.Classes;
using Assets.Utils;
using UnityEngine;

public partial class AIProductSystems : OnDateChange
{
    void ManageProductCompany(GameEntity company)
    {
        //Debug.Log("Manage Product Company ")

        ManageProductTeam(company);

        ManageProductDevelopment(company);

        ManageInvestors(company);
    }

    void ManageProductDevelopment(GameEntity company)
    {
        UpgradeSegment(company);
    }

    void ManageInvestors(GameEntity company)
    {
        // taking investments
        TakeInvestments(company);
    }
}
