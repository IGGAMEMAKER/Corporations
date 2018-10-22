using Assets.Classes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Schedule : MonoBehaviour {
    public GameObject MenuObject;
    int day;
    World world;

    bool IsTimerRunning = false;

    public const float interval = 5f;

    float totalTime = interval;

    // Use this for initialization
    void Start () {
        world = gameObject.GetComponent<Model>().GetWorld();
    }

    void CheckPressedButtons()
    {
        // we need to stop game asap if it is running
        if (Input.GetKeyDown(KeyCode.Space) && IsTimerRunning)
            IsTimerRunning = false;

        if (Input.GetKeyUp(KeyCode.Space))
            IsTimerRunning = !IsTimerRunning;
    }

    void UpdateWorld()
    {
        bool needsRedraw = world.PeriodTick(1);

        if (needsRedraw)
        {

        }
    }

    // Update is called once per frame
    void Update () {
        totalTime -= Time.deltaTime;

        if (totalTime < 0)
        {
            totalTime = interval;

            if (IsTimerRunning)
                UpdateWorld();
        }
    }
}
