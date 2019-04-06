using Assets.Visuals;
using UnityEngine;
using UnityEngine.EventSystems;

public class CompanyDragController : View,
    IBeginDragHandler,
    IDragHandler,
    IEndDragHandler,
    IPointerEnterHandler
{
    public static GameObject itemBeingDragged;

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

        Debug.Log("OnEndDrag");
    }

    void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
    {
        string name = GetComponent<CompanyPreviewView>()._entity.company.Name;

        Debug.Log("Hovering company " + name);

        if (itemBeingDragged != null && itemBeingDragged != gameObject)
            gameObject.AddComponent<DroppableAnimation>();
    }
}
