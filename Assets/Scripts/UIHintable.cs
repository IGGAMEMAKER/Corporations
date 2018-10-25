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
        hint = gameObject;
        UpdateAll();

        //BoxCollider2D b = gameObject.transform.parent.gameObject.AddComponent<BoxCollider2D>();
        //b.isTrigger = true;
        //UIHintControl c = gameObject.transform.parent.gameObject.AddComponent<UIHintControl>();
        //c.SetHintableChild(this);

        //SetHintObject(text);
        if (text.Length > 0)
            SetHintObject(text);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateAll();
    }

    void UpdateAll()
    {
        UpdatePosition();
    }

    void UpdatePosition()
    {
        var offset = new Vector3(100, 0);

        //gameObject.transform.SetParent(gameObject.transform.parent, false);
        gameObject.transform.position = Input.mousePosition + offset;
        SetHintObject(String.Format("Current Time: {0}", new DateTime()));
    }

    public void SetHintObject(string s)
    {
        gameObject.GetComponent<Text>().text = s;
    }

    public void OnHover()
    {
        gameObject.SetActive(true);
    }

    public void OnExit()
    {
        gameObject.SetActive(false);
    }
}
