using Assets.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(ListenMenuChanges))]
public class ManageHoldingsScreenController : View
{
    // managed
    public Text ControlValue;
    public Button InvestButton;

    public Text SelectedCompanyBalance;

    public override void ViewRender()
    {
        base.ViewRender();

        RenderControlValue();

        RenderCEOButtons();
    }

    void ToggleCEOButtons(bool show)
    {
        return;
        InvestButton.interactable = show;
    }

    void RenderCEOButtons()
    {
        if (IsHasShares() && IsDomineering())
            ToggleCEOButtons(true);
        else
            ToggleCEOButtons(false);
    }

    void RenderControlValue()
    {
        int size = GetSizeOfShares();

        string shareholderStatus = GetShareholderStatus(size);

        ControlValue.text = $"{size}% ({shareholderStatus})";
    }

    bool IsHasShares()
    {
        return CompanyUtils.GetTotalShares(SelectedCompany.shareholders.Shareholders) > 0;
    }

    int GetSizeOfShares()
    {
        return CompanyUtils.GetShareSize(GameContext, SelectedCompany.company.Id, MyGroupEntity.shareholder.Id);
    }

    bool IsDomineering()
    {
        return GetSizeOfShares() > 50;
    }

    string GetShareholderStatus(int percent)
    {
        return CompanyUtils.GetShareholderStatus(percent);
    }
}
