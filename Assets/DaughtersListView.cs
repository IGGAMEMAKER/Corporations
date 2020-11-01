using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DaughtersListView : ListView, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject FlagshipTasks;
    public RectTransform DaughtersScrollView;

    public override void SetItem<T>(Transform t, T entity)
    {
        t.GetComponent<CompanyViewOnMainScreen>().SetEntity((GameEntity)(object)entity);
    }

    public override void ViewRender()
    {
        base.ViewRender();

        SetItems(Companies.GetDaughters(Q, MyCompany));
    }

    void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
    {
        ExpandList();
    }

    void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
    {
        ShrinkList();
    }

    public void ExpandList()
    {
        //Hide(FlagshipTasks);
        //DaughtersScrollView.sizeDelta = new Vector2(687, 765);
    }

    public void ShrinkList()
    {
        //Show(FlagshipTasks);
        //DaughtersScrollView.sizeDelta = new Vector2(687, 300);
    }

    private void OnEnable()
    {
        ShrinkList();
    }
}
