using UnityEngine.EventSystems;

public class ShowDevelopmentTab : View, IPointerEnterHandler
{
    void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
    {
        FindObjectOfType<CompanyTaskTypeRelay>().OnDevelopmentTabHover();
    }
}
