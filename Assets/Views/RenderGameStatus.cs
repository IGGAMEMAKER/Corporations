using Assets;
using Assets.Core;
using UnityEngine;
using UnityEngine.UI;

public class RenderGameStatus : View
{
    public GameObject PausedText;

    public override void ViewRender()
    {
        base.ViewRender();

        bool isRunning = ScheduleUtils.IsTimerRunning(Q);

        GetComponent<Image>().color = Visuals.GetColorPositiveOrNegative(isRunning);
        Draw(PausedText, !isRunning);

        if (isRunning)
            SoundManager.Play(Sound.Timer);
    }
}
