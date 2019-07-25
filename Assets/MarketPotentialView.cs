using Assets.Utils;
using UnityEngine.UI;

public class MarketPotentialView : View
{
    public Text PotentialMarketSize;
    public Text PotentialAudienceSize;

    public SetAmountOfStars SetAmountOfStars;

    public Text RiskLabel;
    public Hint RiskHint;

    public Text Demand;
    public Text Maintenance;

    private void OnEnable()
    {
        Render();
    }

    public void Render()
    {
        NicheType nicheType = ScreenUtils.GetSelectedNiche(GameContext);
        var niche = NicheUtils.GetNicheEntity(GameContext, nicheType);

        var rating = NicheUtils.GetMarketRating(niche);
        SetAmountOfStars.SetStars(rating);

        PotentialMarketSize.text = Format.Money(NicheUtils.GetMarketPotential(niche));
        PotentialAudienceSize.text = Format.MinifyToInteger(NicheUtils.GetMarketAudiencePotential(niche));

        var risk = NicheUtils.GetMarketDemandRisk(GameContext, nicheType);
        string riskText = NicheUtils.ShowRiskStatus(risk).ToString();

        RiskLabel.text = $"{risk}% ({riskText})";

        var demand = MarketingUtils.GetCurrentClientBatch(GameContext, nicheType) * MarketingUtils.GetMarketingFinancingAudienceReachModifier(MarketingFinancing.High) * 30;
        Demand.text = Format.MinifyToInteger(demand) + " monthly";

        var maintenance = NicheUtils.GetMaintenanceCost(niche);
        Maintenance.text = Format.MoneyToInteger(maintenance) + " / month";
    }
}
