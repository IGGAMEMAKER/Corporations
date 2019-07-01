using Assets.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HumanHoldingsPreview : View
{
    int ShareholderId
    {
        get
        {
            return SelectedHuman.shareholder.Id;
        }
    }

    GameEntity company;

    int TotalShares;

    public Text NameLabel;
    public Text SharesLabel;
    public Hint SharesAmountHint;

    public LinkToProjectView LinkToCompanyPreview;

    GameEntity ShareholderEntity
    {
        get
        {
            return CompanyUtils.GetInvestorById(GameContext, ShareholderId);
        }
    }

    public void SetEntity(GameEntity company)
    {
        //int shareholderId, BlockOfShares shares
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
        SharesLabel.text = CompanyUtils.GetShareSize(GameContext, company.company.Id, ShareholderId) + "%";

        SharesAmountHint.SetHint("");

        AddLinkIfPossible();
    }
}
