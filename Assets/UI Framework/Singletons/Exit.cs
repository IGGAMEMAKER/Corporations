using Assets.Core;
using UnityEngine;

public class Exit : View
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // https://answers.unity.com/questions/10808/how-to-force-applicationquit-in-web-player-and-edi.html
            #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
            //#else
            //     Application.Quit();
            #endif
        }

        if (Input.GetKeyDown(KeyCode.F12))
        {
            MakeAScreenshot();
        }

        if (Input.GetKeyDown(KeyCode.F11) || Input.GetKeyDown(KeyCode.Tab))
        {
            State.SaveGame(Q);
            Debug.Log("Game Saved");
        }

        if (Input.GetKeyDown(KeyCode.F10) || Input.GetKeyDown(KeyCode.LeftControl))
        {
            State.LoadGame(Q);
            Debug.Log("Game Loaded");
        }
    }

    private void OnApplicationQuit()
    {
        Debug.Log("SESSION TIME: " + (int)Time.time + " seconds");

        PrintSessionLength();
        PrintSessionLength();
        PrintSessionLength();
        PrintSessionLength();
    }

    void PrintSessionLength()
    {
        Debug.Log("SESSION TIME: " + SessionDuration.FormatTime(Time.time));
    }

    void MakeAScreenshot()
    {
        string time = System.DateTime.UtcNow.ToString("dd MMMM yyyy, HH-mm-ss");

        ScreenCapture.CaptureScreenshot($"C://Users/Gaga/Desktop/Screenshots/save {time}.png");
    }
}

public static class SessionDuration
{
    public static string FormatTime()
    {
        return FormatTime(Time.time);
    }
    public static string FormatTime(float time)
    {
        int hours = (int)time / 3600;
        int minutes = (int)time / 60;
        int seconds = (int)time - 60 * minutes;
        int milliseconds = (int)(1000 * (time - minutes * 60 - seconds));

        return string.Format("{0:00}min {1:00}sec", minutes, seconds, milliseconds, hours);
        //return string.Format("{3:00}h {0:00}min {1:00}sec", minutes, seconds, milliseconds, hours);
        //return string.Format("{3:00}:{0:00}:{1:00}:{2:000}", minutes, seconds, milliseconds, hours);
    }
}