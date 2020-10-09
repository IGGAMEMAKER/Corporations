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

        Hide(AddTeamButton);
        Hide(TeamPanel);
        Hide(TeamLabel);
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

        if (hadBankruptcyWarning)
        {
            Show(InvestorsPanel);
            Show(InvestmentsLabel);
        }
        else
        {
            HideInvestmentTab();
        }

        // 1, cause servers are added automatically
        bool anyTaskWasAdded = Flagship.team.Teams[0].Tasks.Count > 2;
        bool hasMoreThanOneTeam = Flagship.team.Teams.Count > 1;


        if (anyTaskWasAdded || hasMoreThanOneTeam)
        {
            //Show(AddTeamButton);
            //Show(TeamPanel);
            //Show(TeamLabel);
        }
        else
        {
            HideTeamTab();
            //Hide(TeamPanel);
            //Hide(TeamLabel);
        }
    }
}
