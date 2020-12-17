public class AddSupportFeature : ButtonController
{
    public override void Execute()
    {
        var supportFeature = GetComponent<SupportView>().SupportFeature;
        var name = supportFeature.Name;

        var product = Flagship;

        var relay = FindObjectOfType<FlagshipRelayInCompanyView>();

        var task = new TeamTaskSupportFeature(supportFeature);

        relay.AddPendingTask(task);
    }
}
