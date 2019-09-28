using Assets.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum TeamUpgrade
{
    DevelopmentPrototype, // match market level
    DevelopmentPolishedApp, // monetised

    DevelopmentCrossplatform, // way more payments

    MarketingBase, // +1
    MarketingAggressive, // +3
    AllPlatformMarketing, // bigger maintenance and reach when getting clients

    ClientSupport, // -1% churn fixed cost
    ImprovedClientSupport, // -1% churn scaling cost
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
        RequiredWorkers.text = "Required workers: " + improvement.Workers;
        Description.text = improvement.Description;

        GetComponent<SetTeamUpgrade>().SetTeanUpgrade(UpgradeType);

        var activated = TeamUtils.IsUpgradePicked(SelectedCompany, UpgradeType);
        UpgradeActivated.gameObject.SetActive(false);

        Panel.color = GetPanelColor(activated);
        if (!activated && !TeamUtils.HasEnoughAvailableWorkersForImprovement(SelectedCompany, UpgradeType))
        {
            Panel.color = new Color(0.75f, 0, 0);
        }
    }
}
