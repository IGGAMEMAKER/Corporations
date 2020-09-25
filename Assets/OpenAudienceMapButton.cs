public class OpenAudienceMapButton : ButtonController
{
    public override void Execute()
    {
        FindObjectOfType<FlagshipRelayInCompanyView>().ChooseAudiencePickingPanel();
    }
}
