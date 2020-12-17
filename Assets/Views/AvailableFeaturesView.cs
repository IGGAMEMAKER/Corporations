public class AvailableFeaturesView : ParameterView
{
    public override string RenderValue()
    {
        var company = Flagship;
        return "?? AvailableFeaturesView";

        //var activeMarkets = Products.GetAmountOfUpgradingFeatures(company, Q);
        //var maxChannels = Products.GetAmountOfFeaturesThatYourTeamCanUpgrade(company);

        //bool maxedOut = activeMarkets >= maxChannels;

        //var helpPhrase = maxedOut ? "(hire more teams)" : "";

        //return $"{activeMarkets} / {maxChannels} features upgrading {helpPhrase}";
    }
}
