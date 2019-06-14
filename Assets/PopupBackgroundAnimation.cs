using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupBackgroundAnimation : MonoBehaviour
{
    float animationLength = 0.8f;
    float duration;

    Image Image;

    Color color;

    void Start ()
    {
        Image = GetComponent<Image>();
        duration = 0;

        color = new Color(0, 0, 0, 1);
    }

    void Render()
    {
        color.a = 210f * duration / 256f / animationLength;

        Image.color = color;

        duration += Time.deltaTime;

        if (duration >= animationLength)
            Destroy(this);
    }

    void Update()
    {
        Render();
    }
}
