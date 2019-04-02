using UnityEngine;
using UnityEngine.EventSystems;

public class EnlargeOnHover : MonoBehaviour
    , IPointerEnterHandler
    , IPointerExitHandler
{
    readonly float animationDuration = 0.15f;
    readonly float amplification = 0.2f;

    float duration = 0;
    bool hovered;

    void Update()
    {
        Render();
    }

    void Render()
    {
        if (hovered)
            duration += Time.deltaTime;
        else
            duration -= Time.deltaTime;

        if (duration < 0)
            duration = 0;

        if (duration > animationDuration)
            duration = animationDuration;

        float scale = (1 + amplification * duration / animationDuration);

        transform.localScale = new Vector3(scale, scale, 1);
    }

    void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
    {
        hovered = true;
    }

    void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
    {
        hovered = false;
    }
}
