using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public struct GoalViewInfo
{
    public int importance;
    public string goal;

    public long have;
    public long need;
}

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
        Goal.text = VisualFormattingUtils.Colorize("Become market fit", VisualConstants.COLOR_POSITIVE);
        ProgressBar.SetValue(7, 8);
    }
}
