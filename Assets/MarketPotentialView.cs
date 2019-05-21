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

    public void SetEntity(NicheType niche)
    {
        var nicheType1 = NicheUtils.GetNicheEntity(GameContext, niche);

        NicheType = niche;

        PotentialMarketSize.text = "10M ... 100M";
        PotentialAudienceSize.text = "10M ... 100M";
        PotentialIncomeSize.text = "1$ ... 10$";

        var risk = NicheUtils.GetStartupRiskOnNiche(GameContext, NicheType);
        string riskText = NicheUtils.GetStartupRiskOnNicheDescription(GameContext, NicheType);

        RiskLabel.text = NicheUtils.ShowRiskStatus(risk).ToString();
        RiskHint.SetHint(riskText);
    }
}
