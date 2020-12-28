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
        var f = GetComponentInParent<CompanyViewOnAudienceMap>();
        // var f = GetComponent<LinkToProjectView>();
        
        ScreenUtils.SetSelectedCompany(Q, f.company.company.Id);
        
        FindObjectOfType<FriendsPanelAnim>().OnPointerEnter(null);
        
        PlaySound(Sound.Bubble1);
    }
}
