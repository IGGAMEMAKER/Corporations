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
        Show(AudiencePanel);
        Show(AudienceLabel);

        AudiencesOnMainScreenListView.HideButtons();
    }
}
