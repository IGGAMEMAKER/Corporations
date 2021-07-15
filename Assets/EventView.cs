using Assets.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum EventStatusType
{
    Possibility,
    Warning,
    Danger,

    Message
}


public class EventView : View
{
    public Text Title;
    public Text Counter;

    public string title;
    public int counter;

    public Image EventStatus;
    public EventStatusType StatusType;

    private void OnValidate()
    {
        Title.text = title;
        Counter.text = $"({counter})";

        if (StatusType == EventStatusType.Danger)
        {
            EventStatus.color = Visuals.Negative();
        }
        else if (StatusType == EventStatusType.Possibility)
        {
            EventStatus.color = Visuals.Positive();
        }
        else if (StatusType == EventStatusType.Warning)
        {
            EventStatus.color = Color.magenta;
        }
        else
        {
            EventStatus.color = Visuals.Neutral();
        }

        Draw(EventStatus, StatusType != EventStatusType.Message);
    }
}
