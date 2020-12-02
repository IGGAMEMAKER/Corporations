using UnityEngine.EventSystems;

public class ShowTeamTab : View, IPointerEnterHandler
{
    void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
    {
        FindObjectOfType<CompanyTaskTypeRelay>().OnTeamTabHover();
    }
}
