using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextBlink : MonoBehaviour
{
    int size;
    float duration;
    bool active;

    void Start()
    {
        active = false;
        size = GetComponent<Text>().fontSize;
        duration = 1;
    }

    void Update()
    {
        if (!active)
            return;

        GetComponent<Text>().fontSize = (int) (size * (1 + 0.85f * duration));
        duration -= Time.deltaTime;

        if (duration < 0)
            active = false;
    }

    internal void Reset()
    {
        duration = 1;
        active = true;
    }
}