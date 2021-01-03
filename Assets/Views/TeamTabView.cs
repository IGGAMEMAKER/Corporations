using UnityEngine.EventSystems;

public class TeamTabView : View, IPointerExitHandler
{
    void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
    {
        FindObjectOfType<CompanyTaskTypeRelay>().OnMarketingTabLeave();
    }
}
