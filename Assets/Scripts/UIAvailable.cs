using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Canvas))]
[RequireComponent(typeof(RectTransform))]
public class UIAvailable : MonoBehaviour {
    // Dirty hacks
    int ticks = 5;
    GameObject image;
    Canvas c;

    private void OnEnable()
    {
        OverrideSortingOrder();
    }

    // Use this for initialization
    void Start () {
        image = new GameObject("background");
        Image background = image.AddComponent<Image>();
        c = image.AddComponent<Canvas>();

        c.sortingOrder = -1;

        image.transform.SetParent(this.gameObject.transform, false);

        ShrinkWidth();

        background.sprite = Resources.Load<Sprite>("interrupt-danger");
    }

    float GetNewWidth (float width)
    {
        return width + 6f;
    }

    void ShrinkWidth ()
    {
        RectTransform parentTransform = this.gameObject.GetComponent<RectTransform>();
        float width = GetNewWidth(parentTransform.sizeDelta.x);
        float height = GetNewWidth(parentTransform.sizeDelta.y);

        RectTransform rect = image.GetComponent<RectTransform>();
        //rect.rect.Set(0, 0, width, height);
        rect.sizeDelta = new Vector2(width, height);
    }

    void OverrideSortingOrder ()
    {
        c.overrideSorting = true;
    }

    // Update is called once per frame
    void Update () {
        //if (ticks > 0)
        //{
        //    OverrideSortingOrder();
        //    ticks--;
        //}
        OverrideSortingOrder();

        ShrinkWidth();
	}
}
