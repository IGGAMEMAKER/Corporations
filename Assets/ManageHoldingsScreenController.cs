using Assets.Core;
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

        string shareholderStatus = Companies.GetShareholderStatus(size);

        ControlValue.text = $"{size}% ({shareholderStatus})";
    }

    int GetSizeOfShares()
    {
        return Companies.GetShareSize(Q, SelectedCompany.company.Id, MyGroupEntity.shareholder.Id);
    }
}
