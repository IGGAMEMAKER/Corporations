using UnityEngine;
using UnityEngine.EventSystems;

public abstract class BlinkOrDestroyOnSomeCondition : View
    , IPointerClickHandler
{
    public abstract bool NeedsToBeRemoved();

    public abstract void OnPointerClick(PointerEventData eventData);


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

        if (NeedsToBeRemoved())
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
}
