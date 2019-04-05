using Assets.Utils;
using UnityEngine.UI;

public class MarketPotentialView : View
{
    NicheType NicheType;

    public Text PotentialMarketSize;
    public Text PotentialAudienceSize;
    public Text PotentialIncomeSize;
    public Text IterationCost;
    public Text Risk;
    public Hint RiskHint;

    public void SetEntity(NicheType niche)
    {
        NicheType = niche;

        PotentialMarketSize.text = "10M ... 100M";
        PotentialAudienceSize.text = "10M ... 100M";
        PotentialIncomeSize.text = "1$ ... 10$";

        IterationCost.text = "100";

        string text = RandomEnum<Risk>.GenerateValue().ToString();

        Risk.text = text;
        RiskHint.SetHint("Current risk is 66%! (" + text + ")" +
            "\nUnknown demand: +33%" +
            "\nUnknown payments: +33%" +
            "\nStrong competitors: +33%"
            );
    }
}
