using Assets.Utils;
using UnityEngine.UI;

public class TeamMaintenanceView : View
    , ITeamListener
{
    Text Text;

    void Start()
    {
        Text = GetComponent<Text>();
    }

    void OnEnable()
    {
        ListenProductChanges()
    }
    
    void Render()
    {
        Text.text = ValueFormatter.ShortenValueMockup(CompanyEconomyUtils.GetTeamMaintenance(MyProductEntity)) + "$";
    }

    void ITeamListener.OnTeam(GameEntity entity, int programmers, int managers, int marketers, int morale)
    {
        throw new System.NotImplementedException();
    }
}
