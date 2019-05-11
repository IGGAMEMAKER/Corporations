using Assets.Utils;
using System;
using System.Collections.Generic;
using UnityEngine.UI;

public class RenderCompanyGoalDropDown : View
{
    public Dropdown Dropdown;

    // Start is called before the first frame update
    void Start()
    {
        var options = new List<Dropdown.OptionData>();

        foreach (InvestorGoal goal in (InvestorGoal[])Enum.GetValues(typeof(InvestorGoal)))
            options.Add(new Dropdown.OptionData(InvestmentUtils.GetInvestorGoal(goal)));

        Dropdown.ClearOptions();
        Dropdown.AddOptions(options);

        Dropdown.onValueChanged.AddListener(UpdateCompanyGoal);
    }

    private void UpdateCompanyGoal(int arg0)
    {
        //var investorGoal = (InvestorGoal) Enum.Parse(typeof(InvestorGoal), arg0.ToString());
        var investorGoal = (InvestorGoal) arg0;


        MyProductEntity.ReplaceCompanyGoal(investorGoal);
    }
}

public abstract class DropdownView : View
{

}
