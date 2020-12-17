using Assets.Core;
using UnityEngine;

public class HideDevelopmentScreenIfOnGroupPage : View
{
    public override void ViewRender()
    {
        base.ViewRender();
        // hide if we are not on player flagship page

        var flagshipId = Companies.GetPlayerFlagshipID(Q);

        bool isOnPlayerFlagshipScreen = CurrentScreen == ScreenMode.ProjectScreen && SelectedCompany.company.Id == flagshipId;


        // hide
        GetComponent<Animator>().enabled = isOnPlayerFlagshipScreen;

        if (!isOnPlayerFlagshipScreen)
        {
            var group = GetComponent<CanvasGroup>();
            group.alpha = 0;
            group.blocksRaycasts = false;
            group.interactable = false;
        }
    }
}
