using Assets.Core;
using UnityEngine;

public class RenderNextCorporateUpgradeDate : ParameterView
{
    public GameObject NextTweakLabel;

    public override string RenderValue()
    {
        var task = Cooldowns.GetTask(Q, new CompanyTaskUpgradeCulture(MyCompany.company.Id));

        if (task == null)
        {
            NextTweakLabel.SetActive(false);
            return "";
        }

        NextTweakLabel.SetActive(true);

        var days = task.EndTime - CurrentIntDate;

        return $"{days} days";
    }
}
