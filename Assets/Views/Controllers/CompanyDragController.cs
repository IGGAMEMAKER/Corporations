using Assets.Core;
using Assets.Visuals;
using UnityEngine;
using UnityEngine.EventSystems;

public class CompanyDragController : View,
    IBeginDragHandler,
    IDragHandler,
    IEndDragHandler,
    IPointerEnterHandler,
    IPointerUpHandler,
    IPointerExitHandler
{
    public static GameObject itemBeingDragged;
    public static GameObject targetItem;

    string GetCompanyName()
    {
        return GetComponent<CompanyPreviewView>().Company.company.Name;
    }

    void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
    {
        itemBeingDragged = gameObject;
    }

    void IDragHandler.OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    int GetCompanyIdByGameObject(GameObject obj)
    {
        return obj.GetComponent<LinkToProjectView>().CompanyId;
    }

    void IEndDragHandler.OnEndDrag(PointerEventData eventData)
    {
        if (targetItem)
        {
            Debug.Log("We will merge companies!");

            int parent = GetCompanyIdByGameObject(targetItem);
            int subsidiary = GetCompanyIdByGameObject(itemBeingDragged);

            Companies.AttachToGroup(Q, Companies.Get(Q, parent), Companies.Get(Q, subsidiary));
        }

        itemBeingDragged = null;
        Debug.Log("OnEndDrag " + GetCompanyName());
    }

    void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("Hovering company " + GetCompanyName());

        if (itemBeingDragged != null && itemBeingDragged != gameObject)
        {
            gameObject.AddComponent<DroppableAnimation>();
            targetItem = gameObject;
        }
    }

    void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("OnPointerExit " + GetCompanyName());

        targetItem = null;
    }

    void IPointerUpHandler.OnPointerUp(PointerEventData eventData)
    {
        Debug.Log("OnPointerUp " + GetCompanyName());
    }
}
