using Assets;
using Assets.Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// if this will crash read
// https://answers.unity.com/questions/1095047/detect-mouse-events-for-ui-canvas.html

//[RequireComponent(typeof(RectTransform))]
//[RequireComponent(typeof(Text))]
//[RequireComponent(typeof(Mask))]
public class UIHint: MonoBehaviour {
    public string text;

    Canvas canvas;

    Text Text;

    SoundManager soundManager;

    // Use this for initialization
    void Start () {
        canvas = GetComponent<Canvas>();
        //Text = GetComponent<Text>();

        soundManager = new SoundManager();

        SetHintObject(text);

        // attach this script to parent object, so we can handle mouse movement events
        UIHintControl c = transform.parent.gameObject.AddComponent<UIHintControl>();
        c.SetHintableChild(this);

        Disable();
    }

    // Update is called once per frame
    void Update()
    {
        UpdatePosition();
    }

    void RenderText()
    {
        if (!Text)
            Text = GetComponent<Text>();

        Text.text = text;
    }

    void UpdatePosition()
    {
        var offset = new Vector3(40, -35);

        //gameObject.transform.SetParent(gameObject.transform.parent, false);
        transform.position = Input.mousePosition + offset;
        //SetHintObject(String.Format("Current Time: {0}", DateTime.Now));
    }

    void SetText(string s)
    {
        text = s.Replace("\\n", "\n");
    }

    public void SetHintObject(string s)
    {
        SetText(s);

        RenderText();
    }

    void Enable()
    {
        canvas.enabled = true;
    }

    void Disable()
    {
        canvas.enabled = false;
    }

    public void OnHover()
    {
        soundManager.PlayOnHintHoverSound();
        Enable();
    }

    public void OnExit()
    {
        Disable();
    }

    internal void Rotate(float angle)
    {
        transform.Rotate(new Vector3(0, 0, angle));
    }
}
