using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RectTransform))]
public class UIAvailable : MonoBehaviour {

    // Use this for initialization
    void Start () {
        UpdateAll();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateAll();
    }

    void UpdateAll()
    {
        ResetPosition();
        ShrinkWidth();
    }

    float GetNewWidth (float width)
    {
        return width + 6f;
    }

    void ResetPosition()
    {
        gameObject.transform.SetParent(gameObject.transform.parent, false);
    }

    void ShrinkWidth ()
    {
        GameObject parent = gameObject.transform.parent.gameObject;
        RectTransform parentTransform = parent.GetComponent<RectTransform>();
        float width = GetNewWidth(parentTransform.sizeDelta.x);
        float height = GetNewWidth(parentTransform.sizeDelta.y);

        RectTransform rect = gameObject.GetComponent<RectTransform>();
        rect.sizeDelta = new Vector2(width, height);
    }
}
