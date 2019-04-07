using UnityEngine;
using UnityEngine.UI;

public class ValueAmplifierAnimation : MonoBehaviour
{
    [Tooltip("$, % e.t.c.")]
    public string MeasuringUnit;

    [Space(20)]
    float previous = 100;
    public float current;

    const float interval = 0.5f;

    float totalTime = interval;

    Text Text;

    bool animationDone = false;

    void Start()
    {
        Text = GetComponent<Text>();

        SetValue(500);
    }

    void Update()
    {
        Render();
    }

    void Render()
    {
        totalTime -= Time.deltaTime;

        if (animationDone)
            return;

        float result;

        if (totalTime >= 0)
        {
            float x = (interval - totalTime) / interval;
            result = Mathf.RoundToInt(previous + x * (current - previous));
        }
        else
        {
            result = current;
            animationDone = true;
        }

        Text.text = result.ToString() + MeasuringUnit;
    }

    void SetValue(float val)
    {
        totalTime = interval;
        animationDone = false;

        previous = current;
        current = val;
    }
}
