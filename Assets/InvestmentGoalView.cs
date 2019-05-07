using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InvestmentGoalView : MonoBehaviour
{
    public Text Importance;
    public Text Goal;
    public ProgressBar ProgressBar;

    void Start()
    {
        Render();
    }

    void Render()
    {
        Importance.text = "58%";
        Goal.text = VisualFormattingUtils.Colorize("Become market fit", VisualConstants.COLOR_NEGATIVE);
        ProgressBar.SetValue(7, 8);
    }
}
