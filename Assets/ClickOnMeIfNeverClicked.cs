using Assets.Core.Tutorial;
using UnityEngine.EventSystems;
using UnityEngine;

[RequireComponent(typeof(ListenTutorialChanges))]
public class ClickOnMeIfNeverClicked : BlinkOrDestroyOnSomeCondition
{
    public TutorialFunctionality TutorialFunctionality;

    public override bool NeedsToBeRemoved()
    {
        return TutorialUtils.IsOpenedFunctionality(Q, TutorialFunctionality);
    }

    public override void OnPointerClick(PointerEventData eventData)
    {
        TutorialUtils.Unlock(Q, TutorialFunctionality);
    }
}
