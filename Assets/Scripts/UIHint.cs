﻿using Assets;
using Assets.Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// if this will crash read
// https://answers.unity.com/questions/1095047/detect-mouse-events-for-ui-canvas.html

[RequireComponent(typeof(RectTransform))]
public class UIHint: MonoBehaviour {
    bool isHover = false;
    public string text;

    Canvas canvas;

    SoundManager soundManager;

    // Use this for initialization
    void Start () {
        UpdateAll();

        UIHintControl c = gameObject.transform.parent.gameObject.AddComponent<UIHintControl>();
        c.SetHintableChild(this);

        canvas = GetComponent<Canvas>();

        if (text.Length > 0)
            SetHintObject(text.Replace("\\n", "\n"));

        Disable();

        soundManager = new SoundManager();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateAll();
    }

    void UpdateAll()
    {
        UpdatePosition();
        UpdateText();
    }

    void UpdateText()
    {
        gameObject.GetComponent<Text>().text = text;
    }

    void UpdatePosition()
    {
        var offset = new Vector3(40, -35);

        //gameObject.transform.SetParent(gameObject.transform.parent, false);
        gameObject.transform.position = Input.mousePosition + offset;
        //SetHintObject(String.Format("Current Time: {0}", DateTime.Now));
    }

    public void SetHintObject(string s)
    {
        text = s;
    }

    void Enable()
    {
        soundManager.PlayOnHintHoverSound();
        canvas.enabled = true;
        //gameObject.SetActive(true);
    }
    void Disable()
    {
        canvas.enabled = false;
        //gameObject.SetActive(false);
    }

    public void OnHover()
    {
        Enable();
    }

    public void OnExit()
    {
        Disable();
    }

    internal void Rotate(float angle)
    {
        gameObject.transform.Rotate(new Vector3(0, 0, angle));
    }
}
