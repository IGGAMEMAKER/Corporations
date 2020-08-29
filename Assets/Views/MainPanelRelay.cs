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

    public void ResetTabs()
    {
        Show(AudiencePanel);
        Show(AudienceLabel);

        Show(InvestorsPanel);
        Show(InvestmentsLabel);

        Show(TeamPanel);
        Show(TeamLabel);
    }

    public void ShowAudiencesAndInvestors()
    {
        ResetTabs();

        AudiencesOnMainScreenListView.HideButtons();
        ShareholdersOnMainScreenListView.HideButtons();
        TeamsPanelListView.HideButtons();
    }

    private void OnEnable()
    {
        ResetTabs();

        AudiencesOnMainScreenListView.HideButtons();
        ShareholdersOnMainScreenListView.HideButtons();
        TeamsPanelListView.HideButtons();
    }
}
