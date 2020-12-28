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
        var f = GetComponent<LinkToProjectView>();
        
        ScreenUtils.SetSelectedCompany(Q, f.CompanyId);
        // FriendsPanelAnim.OnPointerEnter(null);
        
        Debug.Log("OpenCompanyInteractionPanel");
        FindObjectOfType<FriendsPanelAnim>().OnPointerEnter(null);
        
        PlaySound(Sound.Bubble1);
    }
}
