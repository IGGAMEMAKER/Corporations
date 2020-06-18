using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetFastCash : CompanyUpgradeButton
{
    public Text AmountOfLoans;

    public override void Execute()
    {
        Companies.AddResources(MyCompany, sum);
    }

    int fraction = 2;

    long sum => Economy.GetCompanyCost(Q, MyCompany) * fraction / 100;

    public override string GetBenefits()
    {
        return $"Get {Format.Money(sum)}";
        //return $"Get {Format.Money(sum)} for {fraction}% of company";
    }

    public override string GetButtonTitle()
    {
        AmountOfLoans.text = "2";

        return "Raise Investments";
    }

    public override string GetHint()
    {
        return "";
    }

    public override bool GetState()
    {
        return true;
    }
}
