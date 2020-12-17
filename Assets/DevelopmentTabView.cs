using UnityEngine.EventSystems;

public class DevelopmentTabView : View, IPointerExitHandler
{
    void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
    {
        FindObjectOfType<CompanyTaskTypeRelay>().OnDevelopmentTabLeave();
    }
}
