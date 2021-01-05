using UnityEngine.EventSystems;

public class ShowDevelopmentTab : View, IPointerEnterHandler, IPointerClickHandler
{
    void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
    {
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        // FindObjectOfType<CompanyTaskTypeRelay>().OnDevelopmentTabHover();
    }
}
