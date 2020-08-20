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

    public void ExpandAudiences()
    {
        ResetTabs();

        ShareholdersOnMainScreenListView.HideButtons();
        Hide(InvestorsPanel);
        Hide(InvestmentsLabel);
    }

    public void ExpandInvestors()
    {
        ResetTabs();

        AudiencesOnMainScreenListView.HideButtons();
        Hide(AudiencePanel);
        Hide(AudienceLabel);
    }

    public void ResetTabs()
    {
        Show(AudiencePanel);
        Show(AudienceLabel);

        Show(InvestorsPanel);
        Show(InvestmentsLabel);
    }

    public void ShowAudiencesAndInvestors()
    {
        ResetTabs();
    }

    private void OnEnable()
    {
        ResetTabs();

        AudiencesOnMainScreenListView.HideButtons();
        ShareholdersOnMainScreenListView.HideButtons();
    }
}
