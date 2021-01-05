using UnityEngine.EventSystems;

public class ShowMarketingTab : View, IPointerEnterHandler, IPointerClickHandler
{
    void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
    {
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        // FindObjectOfType<CompanyTaskTypeRelay>().OnMarketingTabHover();
    }
}
