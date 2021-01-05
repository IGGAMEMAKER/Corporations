using UnityEngine;
using UnityEngine.EventSystems;

public class DevelopmentTabView : View, IPointerExitHandler
{
    void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("On Pointer Exit DevelopmentTabView");
        FindObjectOfType<CompanyTaskTypeRelay>().OnDevelopmentTabLeave();
    }
}
