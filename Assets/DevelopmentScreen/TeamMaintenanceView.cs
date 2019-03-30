using Assets.Utils;
using UnityEngine.UI;

public class TeamMaintenanceView : View
{
    Text Text;
    // Start is called before the first frame update
    void Start()
    {
        Text = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        Text.text = ValueFormatter.ShortenValue(ProductEconomicsUtils.GetTeamMaintenance(myProductEntity)) + "$";
    }
}
