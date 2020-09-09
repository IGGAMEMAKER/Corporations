using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainPanelRelay : View
{
    public Text InvestmentsLabel;
    public GameObject InvestorsPanel;
    public ShareholdersOnMainScreenListView ShareholdersOnMainScreenListView;

    public Text AudienceLabel;
    public GameObject AudiencePanel;
    public AudiencesOnMainScreenListView AudiencesOnMainScreenListView;

    public Text TeamLabel;
    public GameObject TeamPanel;
    public TeamsPanelListView TeamsPanelListView;

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
        ShareholdersOnMainScreenListView.HideButtons();
        TeamsPanelListView.HideButtons();
    }

    public void ExpandAudiences()
    {
        ResetTabs();

        HideInvestmentTab();
        HideTeamTab();
    }

    public void ExpandInvestors()
    {
        ResetTabs();

        HideAudienceTab();
        HideTeamTab();
    }

    public void ExpandTeams()
    {
        ResetTabs();

        HideAudienceTab();
        HideInvestmentTab();
    }

    void HideAudienceTab()
    {
        AudiencesOnMainScreenListView.HideButtons();

        Hide(AudiencePanel);
        Hide(AudienceLabel);
    }

    void HideInvestmentTab()
    {
        ShareholdersOnMainScreenListView.HideButtons();

        Hide(InvestorsPanel);
        Hide(InvestmentsLabel);
    }

    void HideTeamTab()
    {
        TeamsPanelListView.HideButtons();

        Hide(TeamPanel);
        Hide(TeamLabel);
    }

    public void ResetTabs()
    {
        bool hadFirstMarketingCampaign = Marketing.GetClients(Flagship) > 50;
        bool hadBankruptcyWarning = true; // NotificationUtils.GetPopupContainer(Q).seenPopups.PopupTypes.Contains(PopupType.BankruptcyThreat);

        if (hadFirstMarketingCampaign)
        {
            Show(AudiencePanel);
            Show(AudienceLabel);
        }
        else
        {
            HideAudienceTab();
        }

        if (hadBankruptcyWarning)
        {
            Show(InvestorsPanel);
            Show(InvestmentsLabel);
        }
        else
        {
            HideInvestmentTab();
        }

        Show(TeamPanel);
        Show(TeamLabel);
    }
}
