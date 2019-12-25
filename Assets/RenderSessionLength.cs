using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RenderSessionLength : MonoBehaviour
{
    public Text text;

    private void Update()
    {
        text.text = SessionDuration.FormatTime();
    }
}
