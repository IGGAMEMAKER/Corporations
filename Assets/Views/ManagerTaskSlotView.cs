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
    public Texture2D Organisation;
    public Texture2D ProductPolishing;
    public Texture2D ViralGrowth;

    public void SetEntity(int index)
    {
        SlotId = index;

        ManagerTask task = Flagship.team.Teams[SelectedTeam].ManagerTasks[index]; // RandomEnum<ManagerTask>.GenerateValue();

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

            case ManagerTask.Organisation:
                TaskImage.texture = Organisation;
                Blinker.enabled = false;
                break;

            case ManagerTask.Polishing:
                TaskImage.texture = ProductPolishing;
                Blinker.enabled = false;
                break;

            case ManagerTask.ViralSpread:
                TaskImage.texture = ViralGrowth;
                Blinker.enabled = false;
                break;



            case ManagerTask.ProxyTasks:
                TaskImage.texture = ProxyTasks;
                Blinker.enabled = false;
                break;
        }
    }
}
