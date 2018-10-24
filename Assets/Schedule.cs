using Assets.Classes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Schedule : MonoBehaviour {
    public GameObject MenuObject;
    int day;
    Application application;

    bool IsTimerRunning = false;

    public const float interval = 2f;

    float totalTime = interval;

    // Use this for initialization
    void Start () {
        application = gameObject.GetComponent<Model>().GetWorld();
    }

    void TogglePause()
    {
        Debug.Log("Toggle Pause");
        IsTimerRunning = !IsTimerRunning;
    }

    void CheckPressedButtons()
    {
        // we need to stop game asap if it is running
        //if (Input.GetKeyDown(KeyCode.Space) && IsTimerRunning)
        //IsTimerRunning = false;

        if (Input.GetKeyUp(KeyCode.Space))
            TogglePause();
    }

    void UpdateWorld()
    {
        application.PeriodTick(1);
        application.RedrawResources();
    }

    // Update is called once per frame
    void Update () {
        CheckPressedButtons();

        totalTime -= Time.deltaTime;
        if (totalTime < 0 && IsTimerRunning)
        {
            totalTime = interval;
            UpdateWorld();
        }
    }
}
