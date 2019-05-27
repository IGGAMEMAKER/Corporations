using Assets.Utils;
using UnityEngine.UI;

public class TeamSizeView : View
{
    public Text MaxSize;
    public Text CurrentSize;

    void Render(TeamComponent team)
    {
        MaxSize.text = TeamUtils.GetTeamSize(MyProductEntity) + "";

        int size = TeamUtils.GetTeamMaxSize(MyProductEntity);

        CurrentSize.text = size.ToString();
    }
}
