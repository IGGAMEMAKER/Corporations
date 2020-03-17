using Assets.Core;
using UnityEngine;

public class RenderNextCorporateUpgradeDate : ParameterView
{
    public GameObject NextTweakLabel;

    public override string RenderValue()
    {
        //return "RenderNextCorporateUpgradeValue";
        var task = Cooldowns.GetTask(Q, new CompanyTaskUpgradeCulture(MyCompany.company.Id));
        //Cooldowns.Get(Q, new CooldownUpgradeCorporateCulture(MyCompany.company.Id), out var cooldown);

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
