using Assets.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum EventStatusType
{
    Opportunity,
    Warning,
    Danger,

    Message
}


public class EventView : View
{
    public Text Title;
    public Text Counter;
    public Image ProgressImage;


    public string title;
    public int counter;

    public Image EventStatus;
    public EventStatusType StatusType;

    public Color OpportunityColor;
    public Color WarningColor;
    public Color DangerColor;
    public Color InfoColor;

    public int Progress = -1;

    public void SetProgress(int num)
    {
        Progress = num;
        ProgressImage.fillAmount = (100f - num) / 100f;

        Draw(ProgressImage, Products.GetUpgradePoints(Flagship) < 1);
    }

    private void OnValidate()
    {
        Title.text = title;
        Counter.text = $"({counter})";

        if (Progress == -1)
            Hide(ProgressImage);
        /*if (Application.isEditor)
        {
            SetProgress(Progress);
        }*/

        if (StatusType == EventStatusType.Danger)
        {
            EventStatus.color = Visuals.GetColorFromString("#FF5858"); // DangerColor;
        }
        else if (StatusType == EventStatusType.Opportunity)
        {
            EventStatus.color = Visuals.GetColorFromString("#5EF15C"); // OpportunityColor;
        }
        else if (StatusType == EventStatusType.Warning)
        {
            EventStatus.color = Visuals.GetColorFromString("#FFC977"); // WarningColor;
        }
        else
        {
            EventStatus.color = Visuals.GetColorFromString("#5EF15C");  // InfoColor;
        }

        Draw(EventStatus, StatusType != EventStatusType.Message);
    }
}
