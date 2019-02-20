using UnityEngine;
using UnityEngine.UI;

public class TextBlink : MonoBehaviour
{
    int size;
    float duration;
    bool active;

    Text Text;

    void Start()
    {
        Text = GetComponent<Text>();
        active = true;
        size = Text.fontSize;
        duration = 1;
    }

    void Update()
    {
        if (!active)
            return;

        Text.fontSize = (int) (size * (1 + 0.85f * duration));
        duration -= Time.deltaTime;

        if (duration < 0)
            Destroy(this);
    }
}