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
            #else
                 Application.Quit();
            #endif
        }

        if (Input.GetKeyDown(KeyCode.F12))
        {
            string time = System.DateTime.UtcNow.ToString("dd MMMM yyyy, HH-mm-ss");

            ScreenCapture.CaptureScreenshot($"C://Users/Gaga/Desktop/Screenshots/save {time}.png");
        }
    }
}
