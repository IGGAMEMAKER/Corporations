using Assets.Utils;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(ListenMenuChanges))]
public class ManageHoldingsScreenController : View
{
    // managed
    public Text ControlValue;

    public override void ViewRender()
    {
        base.ViewRender();

        RenderControlValue();
    }

    void RenderControlValue()
    {
        int size = GetSizeOfShares();

        string shareholderStatus = CompanyUtils.GetShareholderStatus(size);

        ControlValue.text = $"{size}% ({shareholderStatus})";
    }

    int GetSizeOfShares()
    {
        return CompanyUtils.GetShareSize(GameContext, SelectedCompany.company.Id, MyGroupEntity.shareholder.Id);
    }
}
