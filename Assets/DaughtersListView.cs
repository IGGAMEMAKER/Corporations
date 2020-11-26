using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class DaughtersListView : ListView, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject FlagshipTasks;
    public RectTransform DaughtersScrollView;

    public GameObject Label;

    public override void SetItem<T>(Transform t, T entity)
    {
        t.GetComponent<CompanyViewOnMainScreen>().SetEntity((GameEntity)(object)entity);
    }

    public override void ViewRender()
    {
        base.ViewRender();

        var daughters = Companies.GetDaughters(MyCompany, Q);

        bool drawCompanies = daughters.Count() > 1;

        Draw(Label, drawCompanies);
        SetItems(daughters.Take(drawCompanies ? daughters.Count() : 0));
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
