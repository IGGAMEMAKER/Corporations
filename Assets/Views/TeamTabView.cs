using UnityEngine.EventSystems;

public class TeamTabView : View, IPointerExitHandler
{
    void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
    {
        OpenUrl("/Holding/Main");
    }
}
