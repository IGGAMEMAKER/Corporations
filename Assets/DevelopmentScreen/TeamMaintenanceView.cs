using Assets.Utils;
using UnityEngine.UI;

public class TeamMaintenanceView : View
{
    void Render()
    {
        var maintenance = EconomyUtils.GetTeamMaintenance(MyProductEntity);

        GetComponent<Text>().text = $"${Format.Minify(maintenance)}";
    }
}
