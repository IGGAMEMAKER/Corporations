using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DevelopmentTabView : View, IPointerExitHandler
{
    void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
    {
        FindObjectOfType<CompanyTaskTypeRelay>().OnDevelopmentTabLeave();
    }
}
