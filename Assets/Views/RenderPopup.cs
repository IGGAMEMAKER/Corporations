using UnityEngine.EventSystems;
using Assets.Core;

public class RenderPopup : View, IPointerClickHandler
{
    void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
    {
        NotificationUtils.ClosePopup(Q);
    }
}
