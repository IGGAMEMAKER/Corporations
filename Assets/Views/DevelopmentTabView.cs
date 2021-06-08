using UnityEngine;
using UnityEngine.EventSystems;

public class DevelopmentTabView : View, IPointerExitHandler
{
    void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("On Pointer Exit DevelopmentTabView");
        OpenUrl("/Holding/Main");

        //FindObjectOfType<CompanyTaskTypeRelay>().OnDevelopmentTabLeave();
    }
}
