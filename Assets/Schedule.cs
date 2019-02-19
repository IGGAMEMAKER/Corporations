using UnityEngine;

public class Schedule : MonoBehaviour {
    public GameObject MenuObject;
    Application2 application;

    bool isTimerRunning = false;

    float totalTime;
    float currentSpeed = 1;

    // Use this for initialization
    void Start () {
        application = gameObject.GetComponent<Model>().GetApplication();
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
        application.PeriodTick(1);
        application.RedrawResources();
        application.RedrawTeam();
    }

    void ResetTimer()
    {
        totalTime = 0.25f / currentSpeed;
    }

    // Update is called once per frame
    void Update () {
        CheckPressedButtons();

        totalTime -= Time.deltaTime;
        if (totalTime < 0 && isTimerRunning)
        {
            ResetTimer();
            UpdateWorld();
        }
    }
}
