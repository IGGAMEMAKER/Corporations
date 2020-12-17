using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RenderCompanyGoalDropDown : View
{
    public Dropdown Dropdown;
    public GameObject ChooseGoalLabel;

    void Start()
    {
        var options = new List<Dropdown.OptionData>();

        //foreach (InvestorGoalType goal in (InvestorGoalType[])Enum.GetValues(typeof(InvestorGoalType)))
            //options.Add(new Dropdown.OptionData(Investments.GetFormattedInvestorGoal(goal)));

        Dropdown.ClearOptions();
        Dropdown.AddOptions(options);

        Dropdown.onValueChanged.AddListener(SetCompanyGoal);
    }

    void OnEnable()
    {
        bool isControlled = SelectedCompany.isControlledByPlayer;

        Dropdown.gameObject.SetActive(isControlled);
        ChooseGoalLabel.SetActive(isControlled);
    }

    private void SetCompanyGoal(int arg0)
    {
        var investorGoal = (InvestorGoalType) arg0;

        Debug.Log("SetCompanyGoal");
        //InvestmentUtils.SetCompanyGoal(GameContext, MyProductEntity, investorGoal);
    }
}
