using Assets.Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RectTransform))]
public class UIHintable: MonoBehaviour {
    GameObject hint;
    bool isHover = false;
    public string text;

    // Use this for initialization
    void Start () {
        UpdateAll();

        BoxCollider b = gameObject.transform.parent.gameObject.AddComponent<BoxCollider>();
        UIHintControl c = gameObject.transform.parent.gameObject.AddComponent<UIHintControl>();
        c.SetHintableChild(this);

        SetHintObject(text);
        if (!text.Equals(""))
            SetHintObject(text);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateAll();
    }

    private void ShowHint()
    {
        hint.SetActive(true);
    }

    private void HideHint()
    {
        hint.SetActive(false);
    }

    void UpdateAll()
    {
        if (isHover)
        {
            ShowHint();
            UpdatePosition();
        }
    }

    void UpdatePosition()
    {
        var offset = new Vector3(100, 0);

        gameObject.transform.SetParent(gameObject.transform.parent, false);
        gameObject.transform.position = Input.mousePosition + offset;
    }

    public void SetHintObject(string s)
    {
        Destroy(hint);
        hint = new GameObject(hint + s);
        hint.AddComponent<Text>().text = s;
        hint.AddComponent<Canvas>().sortingOrder = 1;

        hint.transform.SetParent(gameObject.transform.parent, false);
    }

    public void SetHintObject(GameObject hint)
    {
        Destroy(this.hint);
        this.hint = hint;
    }

    public void OnHover()
    {
        isHover = true;
    }

    public void OnExit()
    {
        isHover = false;
    }
}
