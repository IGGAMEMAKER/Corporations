using UnityEngine.UI;

public class HireButtonsController : View
{
    public Button[] HireIfPair;
    public Button[] HireIfSmallTeam;
    public Button[] HireIfDepartment;
    public Button[] HireIfBigTeam;

    public override void ViewRender()
    {
        base.ViewRender();

        var teamStatus = MyProductEntity.team.TeamStatus;

        switch (teamStatus)
        {
            case TeamStatus.Solo:
                ToggleVisibility(HireIfPair, false);
                ToggleVisibility(HireIfSmallTeam, false);
                ToggleVisibility(HireIfDepartment, false);
                ToggleVisibility(HireIfBigTeam, false);
                break;

            case TeamStatus.Pair:
                ToggleVisibility(HireIfPair, true);
                ToggleVisibility(HireIfSmallTeam, false);
                ToggleVisibility(HireIfDepartment, false);
                ToggleVisibility(HireIfBigTeam, false);
                break;

            case TeamStatus.SmallTeam:
                ToggleVisibility(HireIfPair, true);
                ToggleVisibility(HireIfSmallTeam, true);
                ToggleVisibility(HireIfDepartment, false);
                ToggleVisibility(HireIfBigTeam, false);
                break;

            case TeamStatus.Department:
                ToggleVisibility(HireIfPair, true);
                ToggleVisibility(HireIfSmallTeam, true);
                ToggleVisibility(HireIfDepartment, true);
                ToggleVisibility(HireIfBigTeam, false);
                break;

            case TeamStatus.BigTeam:
                ToggleVisibility(HireIfPair, true);
                ToggleVisibility(HireIfSmallTeam, true);
                ToggleVisibility(HireIfDepartment, true);
                ToggleVisibility(HireIfBigTeam, true);
                break;
        }
    }

    void ToggleVisibility(Button[] buttons, bool show)
    {
        foreach (var b in buttons)
            b.gameObject.SetActive(show);
    }
}
