using UnityEngine.UI;

public class TeamSizeView : View
{
    public Text Programmers;
    public Text Managers;
    public Text Marketers;
    public Text MaxSize;
    public Text CurrentSize;

    void Update()
    {
        Render(MyProductEntity.team);
    }

    void Render(TeamComponent team)
    {
        Programmers.text = team.Programmers.ToString();
        Managers.text = team.Managers.ToString();
        Marketers.text = team.Marketers.ToString();

        MaxSize.text = (team.Managers + 1) * 7 + "";

        int size = team.Managers + team.Marketers + team.Programmers + 1;
        CurrentSize.text = size.ToString(); // 1 - CEO
    }
}
