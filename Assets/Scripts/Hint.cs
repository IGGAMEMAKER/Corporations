using Assets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Hint : MonoBehaviour
    , IPointerEnterHandler
    , IPointerExitHandler
{
    public string Text;

    HintSpawner HintSpawner;

    UIHint hint;

    void Start () {
        HintSpawner = GameObject.Find("HintSpawner").GetComponent<HintSpawner>();
        hint = HintSpawner.Spawn(gameObject).GetComponent<UIHint>();

        SetHint(Text);
    }

    public void SetHintObject(string text)
    {
        SetHint(text);
    }

    public void SetHint(string text)
    {
        hint.SetHintObject(Text);
    }

    public void Rotate(float angle)
    {
        hint.Rotate(angle);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        hint.OnHover();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        hint.OnExit();
    }
}
