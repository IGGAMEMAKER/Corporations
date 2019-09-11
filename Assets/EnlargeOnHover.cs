using Assets;
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

    [SerializeField] bool DisableSound = false;

    void Update()
    {
        if (!hovered && duration == 0)
            return;

        Render();
    }

    void OnDisable()
    {
        Debug.Log("EnlargeOnHover disabled");

        // reset scale
        duration = 0;

        hovered = false;

        SetScale();
    }

    void SetScale()
    {
        float scale = (1 + amplification * duration / animationDuration);

        transform.localScale = new Vector3(scale, scale, 1);
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

        SetScale();
    }

    void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
    {
        if (!hovered && !DisableSound)
            SoundManager.PlayOnHintHoverSound();

        hovered = true;
    }

    void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
    {
        hovered = false;
    }
}
