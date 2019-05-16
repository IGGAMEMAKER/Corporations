using UnityEngine.UI;

public class TeamSizeView : View
{
    public Text MaxSize;
    public Text CurrentSize;

    void Render(TeamComponent team)
    {
        MaxSize.text = (team.Managers + 1) * 7 + "";

        int size = team.Managers + team.Marketers + team.Programmers + 1;
        CurrentSize.text = size.ToString(); // 1 - CEO
    }
}
