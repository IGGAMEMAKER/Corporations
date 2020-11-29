using UnityEngine.EventSystems;

public class ShowMarketingTab : View, IPointerEnterHandler
{
    void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
    {
        FindObjectOfType<CompanyTaskTypeRelay>().OnMarketingTabHover();
    }
}
