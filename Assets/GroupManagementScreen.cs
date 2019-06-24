using System.Collections.Generic;
using Assets.Utils;
using UnityEngine;
using UnityEngine.UI;

public class GroupManagementScreen : View
{
    public Text GroupBalance;
    public CompanyPreviewView CompanyPreviewView;

    public Text SelectedCompanyName;
    public Text SelectedCompanyBalance;
    public ColoredValuePositiveOrNegative SelectedCompanyROI;

    void RenderROI()
    {
        if (CompanyEconomyUtils.IsROICounable(SelectedCompany, GameContext))
            SelectedCompanyROI.UpdateValue(CompanyEconomyUtils.GetBalanceROI(SelectedCompany, GameContext));
        else
            SelectedCompanyROI.GetComponent<Text>().text = "???";
    }

    void RemoveLinkIfPossible()
    {
        var link = CompanyPreviewView.GetComponent<LinkToProjectView>();

        if (link)
            Destroy(link);
    }

    public override void ViewRender()
    {
        base.ViewRender();

        CompanyPreviewView.SetEntity(MyGroupEntity);
        RemoveLinkIfPossible();

        GroupBalance.text = Format.Shorten(MyGroupEntity.companyResource.Resources.money);

        SelectedCompanyName.text = MyGroupEntity.company.Name;

        RenderROI();
    }
}
