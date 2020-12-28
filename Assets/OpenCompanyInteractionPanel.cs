using System.Collections;
using System.Collections.Generic;
using Assets;
using Assets.Core;
using Michsky.UI.Frost;
using UnityEngine;

public class OpenCompanyInteractionPanel : ButtonController
{
    public override void Execute()
    {
        // var f = GetComponentInParent<CompanyViewOnAudienceMap>();
        var f = GetComponent<LinkToProjectView>();

        // var companyId = f.company.company.Id;
        var companyId = f.CompanyId;
        
        // Debug.Log("Open company interaction panel: " + companyId);
        
        // ScreenUtils.SetIntegerWithoutUpdatingScreen(Q, companyId, C.MENU_SELECTED_COMPANY);
        ScreenUtils.SetSelectedCompany(Q, companyId);
        
        FindObjectOfType<FriendsPanelAnim>().OnPointerEnter(null);
        
        PlaySound(Sound.Bubble1);
    }
}
