using UnityEngine.EventSystems;

public class MarektingTabView : View, IPointerExitHandler
{
    void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
    {
        FindObjectOfType<CompanyTaskTypeRelay>().OnMarketingTabLeave();
    }
}
