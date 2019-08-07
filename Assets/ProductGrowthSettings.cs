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
        Debug.Log("SetNormalDev: " + SelectedCompany.company.Name);
        SelectedCompany.isAggressiveMarketing = false;
        

        Note.text = Visuals.Positive($"Company will develop normally and " +
            $"pay dividends, when they will become profitable");
    }

    public void SetAggressiveMarketing()
    {
        Debug.Log("SetAggressiveDev: " + SelectedCompany.company.Name);
        SelectedCompany.isAggressiveMarketing = true;

        var maintenance = NicheUtils.GetAggressiveMarketingMaintenance(GameContext, SelectedCompany.product.Niche).money;

        Note.text = Visuals.Negative($"This company will need additional {Format.Money(maintenance)} on balance for marketing." +
            $"\nAlso, they will not pay dividends!");
    }

    public override void ViewRender()
    {
        base.ViewRender();

        Debug.Log("ViewRender: " + SelectedCompany.company.Name);

        if (SelectedCompany.isAggressiveMarketing)
        {
            var maintenance = NicheUtils.GetAggressiveMarketingMaintenance(GameContext, SelectedCompany.product.Niche).money;

            Note.text = Visuals.Negative($"This company will need additional {Format.Money(maintenance)} on balance for marketing." +
                $"\nAlso, they will not pay dividends!");

            return;
            Debug.Log("ViewRender: Is aggressive" + SelectedCompany.company.Name);
            Aggressive.isOn = true;

            // useless? cause toggle group does that anyway
            Normal.isOn = false;
        } else
        {
            Note.text = Visuals.Positive($"Company will develop normally and pay dividends, when they will become profitable");
            return;
            Debug.Log("ViewRender: Is Normal" + SelectedCompany.company.Name);
            Normal.isOn = true;

            // useless? cause toggle group does that anyway
            Aggressive.isOn = false;
        }
    }
}
