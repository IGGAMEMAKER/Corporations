using UnityEngine;
using UnityEngine.EventSystems;

public class EnlargeOnHover : MonoBehaviour
    , IPointerEnterHandler
    , IPointerExitHandler
{
    float duration;

    bool hovered;

    float animationDuration = 0.15f;
    float amplification = 0.2f;

    void Start()
    {
        duration = 0;
    }

    void Update()
    {
        Render();
    }

    void Render()
    {
        float scale = (1 + amplification * duration / animationDuration);
        transform.localScale = new Vector3(scale, scale, 1);

        if (hovered)
            duration += Time.deltaTime;
        else
            duration -= Time.deltaTime;

        if (duration < 0)
            duration = 0;

        if (duration > animationDuration)
            duration = animationDuration;
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
