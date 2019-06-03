using Assets.Utils.Tutorial;
using UnityEngine.EventSystems;

public enum TutorialFunctionality
{
    MarketingMenu,
    CompetitorView,
    PossibleInvestors,
    LinkToProjectViewInInvestmentRounds,
    FirstAdCampaign
}

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
