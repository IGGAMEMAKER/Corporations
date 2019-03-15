using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CooldownView : View
{
    public TaskType TaskType;
    Button Target;
    Color BaseColor;

    // Start is called before the first frame update
    void Start()
    {
        Target = GetComponent<Button>();
        BaseColor = Target.colors.disabledColor;
    }

    void SetDisabledColor(Color color)
    {
        ColorBlock cb = Target.colors;
        cb.disabledColor = color;
        Target.colors = cb;
    }

    void AnimateButtonBackground(TaskComponent taskComponent)
    {
        float part = GetTaskCompletionPercentage(taskComponent) / 100f;
        Color color = new Color(255, 255, 255, 0.2f + 0.3f * part);

        SetDisabledColor(color);
        Target.interactable = false;
    }

    void ResetButton()
    {
        Target.interactable = true;
        SetDisabledColor(BaseColor);
    }

    void ResetColor()
    {
        var t = GetTask(TaskType);

        if (t == null)
            ResetButton();
        else
            AnimateButtonBackground(t);
    }

    // Update is called once per frame
    void Update()
    {
        ResetColor();
    }
}
