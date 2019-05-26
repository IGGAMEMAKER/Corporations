using Assets.Utils;
using UnityEngine.UI;

public class MarketPotentialView : View
{
    NicheType NicheType;

    public Text PotentialMarketSize;
    public Text PotentialAudienceSize;
    public Text PotentialIncomeSize;

    public Text RiskLabel;
    public Hint RiskHint;

    private void OnEnable()
    {
        var niche = ScreenUtils.GetSelectedNiche(GameContext);

        SetEntity(niche);
    }

    public void SetEntity(NicheType nicheType)
    {
        var niche = NicheUtils.GetNicheEntity(GameContext, nicheType);

        NicheType = nicheType;

        PotentialMarketSize.text = "10M ... 100M";
        PotentialAudienceSize.text = "10M ... 100M";
        PotentialIncomeSize.text = "1$ ... 10$";

        var risk = NicheUtils.GetMarketDemandRisk(GameContext, NicheType);
        string riskText = NicheUtils.ShowRiskStatus(risk).ToString();

        RiskLabel.text = $"{risk}% ({NicheUtils.ShowRiskStatus(risk)})";
        //RiskHint.SetHint(riskText);
    }
}
