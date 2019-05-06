using Assets.Utils;
using UnityEngine.UI;

public class TeamMaintenanceView : View
    , ITeamListener
{
    Text Text;

    void Start()
    {
        Text = GetComponent<Text>();

        MyProductEntity.AddTeamListener(this);

        Render();
    }

    void Render()
    {
        Text.text = $"${ValueFormatter.Shorten(CompanyEconomyUtils.GetTeamMaintenance(MyProductEntity))}";
    }

    void ITeamListener.OnTeam(GameEntity entity, int programmers, int managers, int marketers, int morale)
    {
        Render();
    }
}
