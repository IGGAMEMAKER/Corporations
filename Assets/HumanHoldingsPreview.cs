﻿using Assets.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HumanHoldingsPreview : View
{
    public Text NameLabel;
    public Text SharesLabel;
    public Hint SharesAmountHint;

    public LinkToProjectView LinkToCompanyPreview;

    GameEntity company;

    public void SetEntity(GameEntity company)
    {
        this.company = company;

        Render();
    }

    void AddLinkIfPossible()
    {
        LinkToCompanyPreview.CompanyId = company.company.Id;
    }

    void Render()
    {
        NameLabel.text = company.company.Name;
        SharesLabel.text = CompanyUtils.GetShareSize(GameContext, company.company.Id, SelectedHuman.shareholder.Id) + "%";

        SharesAmountHint.SetHint("");

        AddLinkIfPossible();
    }
}
