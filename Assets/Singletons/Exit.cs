using UnityEngine;

public class Exit : MonoBehaviour
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
    }
}
