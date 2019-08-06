using Assets.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProductGrowthSettings : View
{
    public Text Note;

    public Toggle Aggressive;
    public Toggle Normal;

    public void SetNormalDevelopment()
    {
        SelectedCompany.isAggressiveMarketing = false;

        //var maintenance = NicheUtils.GetAggressiveMarketingMaintenance(GameContext, SelectedCompany.product.Niche);

        Note.text = Visuals.Positive($"Company will develop normally and " +
            $"pay dividends, when they will become profitable");
    }

    public void SetAggressiveMarketing()
    {
        SelectedCompany.isAggressiveMarketing = true;

        var maintenance = NicheUtils.GetAggressiveMarketingMaintenance(GameContext, SelectedCompany.product.Niche).money;

        Note.text = Visuals.Negative($"This company will need additional {Format.Money(maintenance)} on balance for marketing." +
            $"\nAlso, they will not pay dividends!");
    }

    public override void ViewRender()
    {
        base.ViewRender();

        if (SelectedCompany.isAggressiveMarketing)
        {
            Aggressive.isOn = true;
            
            // useless? cause toggle group does that anyway
            Normal.isOn = false;
        } else
        {
            Normal.isOn = true;
            
            // useless? cause toggle group does that anyway
            Aggressive.isOn = false;
        }
    }
}
