using Assets.Core;
using UnityEngine;
using UnityEngine.EventSystems;

public class DevelopmentTabView : View, IPointerExitHandler
{
    void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("On Pointer Exit DevelopmentTabView");
        //ScheduleUtils.ResumeGame(Q);

        OpenUrl("/Holding/Main");

        //FindObjectOfType<CompanyTaskTypeRelay>().OnDevelopmentTabLeave();
    }

    private void OnEnable()
    {
        ScheduleUtils.PauseGame(Q);
    }
}
