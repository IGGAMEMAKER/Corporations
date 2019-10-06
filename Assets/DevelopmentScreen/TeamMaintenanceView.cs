using Assets.Utils;
using UnityEngine.UI;

public class TeamMaintenanceView : View
{
    void Render()
    {
        var maintenance = 0;

        GetComponent<Text>().text = Format.Money(maintenance);
    }
}
