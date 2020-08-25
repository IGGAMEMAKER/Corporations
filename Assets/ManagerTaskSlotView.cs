using Assets.Core;
using UnityEngine;
using UnityEngine.UI;

public class ManagerTaskSlotView : View
{
    int SlotId;
    public RawImage TaskImage;
    public Blinker Blinker;

    public Texture2D None;
    public Texture2D Bureaucracy;
    public Texture2D SearchInvestments;
    public Texture2D Recruiting;
    public Texture2D ProxyTasks;

    public void SetEntity(int index)
    {
        SlotId = index;

        ManagerTask task = RandomEnum<ManagerTask>.GenerateValue();

        switch (task)
        {
            case ManagerTask.None:
                TaskImage.texture = None;
                Blinker.enabled = true;
                break;

            case ManagerTask.Documentation:
                TaskImage.texture = Bureaucracy;
                Blinker.enabled = false;
                break;

            case ManagerTask.Investments:
                TaskImage.texture = SearchInvestments;
                Blinker.enabled = false;
                break;

            case ManagerTask.Recruiting:
                TaskImage.texture = Recruiting;
                Blinker.enabled = false;
                break;

            case ManagerTask.ProxyTasks:
                TaskImage.texture = ProxyTasks;
                Blinker.enabled = false;
                break;
        }
    }
}
