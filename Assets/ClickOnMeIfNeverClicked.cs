using Assets.Utils.Tutorial;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public enum TutorialFunctionality
{
    MarketingMenu,
    CompetitorView,
    PossibleInvestors
}

public class ClickOnMeIfNeverClicked : View
    , IPointerClickHandler
{
    public TutorialFunctionality TutorialFunctionality;

    float period = 0.15f;

    float amplification = 0.2f;

    float duration = 0;
    bool animated;

    void Update()
    {
        Render();
    }

    void OnEnable()
    {
        duration = 0;

        var isOpenedFunctionality = TutorialUtils.IsOpenedFunctionality(GameContext, TutorialFunctionality);

        if (isOpenedFunctionality)
            Destroy(this);
        else
            animated = true;
    }

    private void OnDisable()
    {
        animated = false;
    }

    void Render()
    {
        if (!animated)
            return;

        duration += Time.deltaTime;

        float phase;

        if (duration < period)
            phase = duration / period;
        else
            phase = 2 - duration / period;

        float scale = 1 + amplification * phase;

        transform.localScale = new Vector3(scale, scale, 1);

        if (duration >= period * 2)
            duration = 0;
    }

    void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
    {
        TutorialUtils.Unlock(GameContext, TutorialFunctionality);
    }
}
