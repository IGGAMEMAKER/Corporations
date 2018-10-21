using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Canvas))]
[RequireComponent(typeof(RectTransform))]
public class UIAvailable : MonoBehaviour {

    // Use this for initialization
    void Start () {
        ResetPosition();
        ShrinkWidth();
    }

    float GetNewWidth (float width)
    {
        return width + 6f;
    }

    void ResetPosition()
    {
        this.gameObject.transform.SetParent(this.gameObject.transform.parent, false);
    }

    void ShrinkWidth ()
    {
        GameObject parent = gameObject.transform.parent.gameObject;
        RectTransform parentTransform = parent.GetComponent<RectTransform>();
        float width = GetNewWidth(parentTransform.sizeDelta.x);
        float height = GetNewWidth(parentTransform.sizeDelta.y);

        RectTransform rect = gameObject.GetComponent<RectTransform>();
        //rect.rect.Set(0, 0, width, height);
        rect.sizeDelta = new Vector2(width, height);
    }

    // Update is called once per frame
    void Update () {
        ShrinkWidth();
    }
}
