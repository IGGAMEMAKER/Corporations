using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManualExit : ButtonController
{
    public override void Execute()
    {
        // https://answers.unity.com/questions/10808/how-to-force-applicationquit-in-web-player-and-edi.html
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
             Application.Quit();
#endif
        //Application.Quit();
    }
}
