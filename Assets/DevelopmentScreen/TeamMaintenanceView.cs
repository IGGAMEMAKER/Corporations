using Assets.Utils;
using UnityEngine.UI;

public class TeamMaintenanceView : View
{
    void Render()
    {
        var maintenance = CompanyEconomyUtils.GetTeamMaintenance(MyProductEntity);

        GetComponent<Text>().text = $"${Format.Minify(maintenance)}";
    }
}
