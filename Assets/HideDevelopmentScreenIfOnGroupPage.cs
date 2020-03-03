using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideDevelopmentScreenIfOnGroupPage : View
{
    public override void ViewRender()
    {
        base.ViewRender();

        var company = SelectedCompany;
        var flagship = Companies.GetFlagship(Q, MyCompany);

        var flagshipId = -1; // flagship?.company.Id ?? -1;
        if (flagship != null)
            flagshipId = flagship.company.Id;

        bool isOnFlagshipScreen = CurrentScreen == ScreenMode.ProjectScreen && company.company.Id == flagshipId;

        GetComponent<Animator>().enabled = isOnFlagshipScreen;

        if (!isOnFlagshipScreen)
        {
            var group = GetComponent<CanvasGroup>();
            group.alpha = 0;
            group.blocksRaycasts = false;
            group.interactable = false;
        }
    }
}
