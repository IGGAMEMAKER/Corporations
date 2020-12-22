using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorporateCultureSettingsRelay : View
{
    public GameObject OrganizationTab;
    public GameObject RecruitingTab;
    public GameObject MotivationTab;

    private void OnEnable()
    {
        OnOrganizationTab();
    }

    public List<GameObject> Tabs => new List<GameObject> { OrganizationTab, RecruitingTab, MotivationTab };
    
    public void OnOrganizationTab()
    {
        ShowOnly(OrganizationTab, Tabs);
    }

    public void OnRecruitingTab()
    {
        ShowOnly(RecruitingTab, Tabs);
    }

    public void OnMotivationTab()
    {
        ShowOnly(MotivationTab, Tabs);
    }
}
