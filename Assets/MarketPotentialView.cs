using Assets.Utils;
using UnityEngine.UI;

public class MarketPotentialView : View
{
    public Text PotentialMarketSize;
    public Text PotentialAudienceSize;
    public Text PotentialIncomeSize;

    public Text RiskLabel;
    public Hint RiskHint;

    private void OnEnable()
    {
        Render();
    }

    public void Render()
    {
        NicheType nicheType = ScreenUtils.GetSelectedNiche(GameContext);
        var niche = NicheUtils.GetNicheEntity(GameContext, nicheType);

        PotentialMarketSize.text = "10M ... 100M";
        PotentialAudienceSize.text = "10M ... 100M";

        var price = niche.nicheCosts.BasePrice;
        PotentialIncomeSize.text = $"{price}$ ... {price * 2}$";

        var risk = NicheUtils.GetMarketDemandRisk(GameContext, nicheType);
        string riskText = NicheUtils.ShowRiskStatus(risk).ToString();

        RiskLabel.text = $"{risk}% ({NicheUtils.ShowRiskStatus(risk)})";
        //RiskHint.SetHint(riskText);
    }
}
