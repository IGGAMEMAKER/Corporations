using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class ChangePositioningButton : ButtonView, IPointerEnterHandler, IPointerExitHandler
{
    public int positioningId;

    public override void Execute()
    {
        Marketing.ChangePositioning(Flagship, positioningId);

        FindObjectOfType<PositioningVariantsListView2>().ViewRender();
    }

    public override void ViewRender()
    {
        base.ViewRender();

        var positioningTitle = "<b>" + Marketing.GetNichePositionings(Flagship)[positioningId].name + "</b>";
        if (positioningId == Flagship.productPositioning.Positioning)
        {
            positioningTitle += "\n" + Visuals.Colorize("Our positioning", Colors.COLOR_GOLD);
        }

        GetComponentInChildren<TextMeshProUGUI>().text = positioningTitle;
    }

    public void SetEntity(int positioningId)
    {
        this.positioningId = positioningId;

        ViewRender();
    }

    void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
    {
        // audiences loyalty change
        var positioning = Marketing.GetNichePositionings(Flagship);

        var currentBuff = positioning[positioningId].Loyalties;

        FindObjectOfType<AudiencesOnMainScreenListView>().ShowLoyaltyChanges(currentBuff);
    }

    void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
    {
        FindObjectOfType<AudiencesOnMainScreenListView>().HideLoyaltyChanges();
    }
}
