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
        return GetComponent<CompanyPreviewView>()._entity.company.Name;
    }

    void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
    {
        itemBeingDragged = gameObject;
    }

    void IDragHandler.OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    void IEndDragHandler.OnEndDrag(PointerEventData eventData)
    {
        itemBeingDragged = null;
        Debug.Log("OnEndDrag " + GetCompanyName());

        if (targetItem)
            Debug.Log("We need to merge companies!");

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
