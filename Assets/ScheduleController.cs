using UnityEngine;

public class ScheduleController : MonoBehaviour {
    public GameObject MenuObject;

    bool isTimerRunning = false;

    float totalTime;
    float currentSpeed = 1;

    // Use this for initialization
    void Start () {
        ResetTimer();
    }

    void ToggleTimer ()
    {
        isTimerRunning = !isTimerRunning;
    }

    void CheckPressedButtons()
    {
        // on right click
        if (Input.GetMouseButtonUp(1))
            ToggleTimer();

        if (Input.GetKeyUp(KeyCode.Space))
            ToggleTimer();

        if (Input.GetKeyUp(KeyCode.KeypadPlus) && currentSpeed < 3)
            currentSpeed++;

        if (Input.GetKeyUp(KeyCode.KeypadMinus) && currentSpeed > 1)
            currentSpeed--;
    }

    void UpdateWorld()
    {
        //application.PeriodTick(1);
        Debug.Log("timer++");
    }

    void ResetTimer()
    {
        totalTime = 0.25f / currentSpeed;
    }

    // Update is called once per frame
    void Update ()
    {
        CheckPressedButtons();

        UpdateTimer();
    }

    private void UpdateTimer()
    {
        totalTime -= Time.deltaTime;
        if (totalTime < 0 && isTimerRunning)
        {
            ResetTimer();
            UpdateWorld();
        }
    }
}
