using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BlinkIfGoalIsCompleted : BlinkOnSomeCondition
{
    public override bool ConditionCheck()
    {
        throw new System.NotImplementedException();
    }

    public override void OnPointerClick(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }
}

public class Blinker : MonoBehaviour
{
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
    }

    private void OnDisable()
    {
        animated = false;
    }

    void Render()
    {
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
}

public abstract class BlinkOnSomeCondition : View
    , IPointerClickHandler
{
    public abstract bool ConditionCheck();

    public abstract void OnPointerClick(PointerEventData eventData);

    public override void ViewRender()
    {
        base.ViewRender();

        if (ConditionCheck())
            gameObject.AddComponent<Blinker>();
    }
}
