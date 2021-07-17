using Assets.Core;
using UnityEngine.EventSystems;

public class TeamTabView : View, IPointerExitHandler
{
    void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
    {
        OpenUrl("/Holding/Main");
        ScheduleUtils.ResumeGame(Q);
    }

    private void OnEnable()
    {
        ScheduleUtils.PauseGame(Q);
    }
}
