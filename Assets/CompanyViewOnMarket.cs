using Assets.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CompanyViewOnMarket : View
{
    public Text Name;
    public Hint CompanyHint;
    
    public void SetEntity(GameEntity c)
    {
        int companyId = c.company.Id;
        //var c = CompanyUtils.GetCompanyById(GameContext, companyId);

        var name = c.company.Name;
        Name.text = name.Substring(0, 1);

        CompanyHint.SetHint(name);

        GetComponent<LinkToProjectView>().CompanyId = companyId;
    }
}
