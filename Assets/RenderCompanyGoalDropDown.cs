﻿using Assets.Utils;
using System;
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

        foreach (InvestorGoal goal in (InvestorGoal[])Enum.GetValues(typeof(InvestorGoal)))
            options.Add(new Dropdown.OptionData(InvestmentUtils.GetFormattedInvestorGoal(goal)));

        Dropdown.ClearOptions();
        Dropdown.AddOptions(options);

        Dropdown.onValueChanged.AddListener(SetCompanyGoal);
    }

    void OnEnable()
    {
        bool isControlled = IsUnderPlayerControl(SelectedCompany.company.Id);

        Dropdown.gameObject.SetActive(isControlled);
        ChooseGoalLabel.SetActive(isControlled);
    }

    private void SetCompanyGoal(int arg0)
    {
        var investorGoal = (InvestorGoal) arg0;

        CompanyUtils.SetCompanyGoal(GameContext, MyProductEntity, investorGoal, 360);
    }
}
