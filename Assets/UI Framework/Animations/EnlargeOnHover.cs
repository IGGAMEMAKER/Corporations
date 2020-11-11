using Assets;
using UnityEngine;
using UnityEngine.EventSystems;

public class EnlargeOnHover : MonoBehaviour
    , IPointerEnterHandler
    , IPointerExitHandler
{
    readonly float animationDuration = 0.15f;
    public float amplification = 0.2f;

    float duration = 0;
    bool hovered;

    [SerializeField] bool DisableSound = false;

    Texture2D _cursorTexture;
    Texture2D clickCursor
    {
        get
        {
            if (_cursorTexture == null)
            {
                _cursorTexture = Resources.Load<Texture2D>("cursor"); // clickCursor
            }

            return _cursorTexture;
        }
    }
    Texture2D _normalCursorTexture;
    Texture2D normalCursor
    {
        get
        {
            return clickCursor;
            if (_normalCursorTexture == null)
            {
                _normalCursorTexture = Resources.Load<Texture2D>("normalCursor");
            }

            return _normalCursorTexture;
        }
    }
    CursorMode cursorMode = CursorMode.Auto;
    Vector2 hotSpot = Vector2.zero;

    void Update()
    {
        if (!hovered && duration == 0)
            return;

        Render();
    }

    void OnDisable()
    {
        //Debug.Log("EnlargeOnHover disabled");

        // reset scale
        duration = 0;

        hovered = false;
        SetNormalCursor();

        SetScale();
    }

    void SetScale()
    {
        float scale = 1 + amplification * duration / animationDuration;

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
        SetClickCursor();
    }

    void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
    {
        hovered = false;
        SetNormalCursor();
    }

    void SetClickCursor()
    {
        Cursor.SetCursor(clickCursor, hotSpot, cursorMode);
    }

    void SetNormalCursor()
    {
        Cursor.SetCursor(normalCursor, hotSpot, cursorMode);
    }
}
