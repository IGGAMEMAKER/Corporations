using Assets.Utils;
using UnityEngine.EventSystems;
using UnityEngine;

[RequireComponent(typeof(ListenTutorialChanges))]
public class ClickOnMeIfNeverClicked : BlinkOrDestroyOnSomeCondition
{
    public TutorialFunctionality TutorialFunctionality;

    public override bool NeedsToBeRemoved()
    {
        return TutorialUtils.IsOpenedFunctionality(GameContext, TutorialFunctionality);
    }

    public override void OnPointerClick(PointerEventData eventData)
    {
        TutorialUtils.Unlock(GameContext, TutorialFunctionality);
    }
}
