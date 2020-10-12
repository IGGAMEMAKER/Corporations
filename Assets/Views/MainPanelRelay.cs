using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainPanelRelay : View
{
    public Text AudienceLabel;
    public GameObject AudiencePanel;
    public AudiencesOnMainScreenListView AudiencesOnMainScreenListView;

    public GameObject AddTeamButton;

    void OnEnable()
    {
        ShowDefaultMode();
    }

    public override void ViewRender()
    {
        base.ViewRender();

        ShowDefaultMode();
    }

    public void ShowDefaultMode()
    {
        ResetTabs();

        AudiencesOnMainScreenListView.HideButtons();
    }

    void HideAudienceTab()
    {
        AudiencesOnMainScreenListView.HideButtons();

        Hide(AudiencePanel);
        Hide(AudienceLabel);
    }

    public void ResetTabs()
    {
        bool hadFirstMarketingCampaign = Marketing.GetClients(Flagship) > 50;
        bool hadBankruptcyWarning = CurrentIntDate > 60 || Economy.IsWillBecomeBankruptOnNextPeriod(Q, Flagship); // NotificationUtils.GetPopupContainer(Q).seenPopups.PopupTypes.Contains(PopupType.BankruptcyThreat);

        //if (hadFirstMarketingCampaign)
        //{
        //    Show(AudiencePanel);
        //    Show(AudienceLabel);
        //}
        //else
        //{
        //    HideAudienceTab();
        //}

        Show(AudiencePanel);
        Show(AudienceLabel);
    }
}
