using Assets.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum TeamUpgrade
{
    Prototype, // match market level
    Release, // monetised

    Multiplatform, // way more payments

    //MarketingBase, // +1
    //MarketingAggressive, // +3
    //MarketingAllPlatform, // bigger maintenance and reach when getting clients

    //ClientSupport, // -1% churn fixed cost
    //ClientSupportImproved, // -1% churn scaling cost
}

public class TeamUpgradeView : View
{
    public TeamUpgrade UpgradeType;

    public Text UpgradeName;
    public Image UpgradeActivated;
    public Text RequiredWorkers;
    public Text Description;

    public Image Panel;

    

    public void SetEntity(TeamImprovement improvement)
    {
        UpgradeType = improvement.TeamUpgrade;

        UpgradeName.text = improvement.Name;

        var maintenance = TeamUtils.GetImprovementCost(GameContext, SelectedCompany, improvement.TeamUpgrade);
        RequiredWorkers.text = "Maintenance: " + Format.Money(maintenance);
        //RequiredWorkers.text = "Required workers: " + improvement.Workers;

        Description.text = improvement.Description;

        GetComponent<SetTeamUpgrade>().SetTeanUpgrade(UpgradeType);

        var activated = TeamUtils.IsUpgradePicked(SelectedCompany, UpgradeType);
        UpgradeActivated.gameObject.SetActive(false);

        Panel.color = GetPanelColor(activated);
    }
}
