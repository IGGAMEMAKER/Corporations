using Assets.Utils;
using Assets.Utils.Formatting;
using UnityEngine.UI;

public class ClientSegmentView : View
{
    public Image IsSelectedNiche;

    public ColoredValuePositiveOrNegative LoyaltyLabel;
    public Text LevelLabel;
    public Text Income;

    public Hint SegmentHint;
    public Hint LoyaltyHint;

    public void Render(NicheType nicheType, int companyId)
    {
        var c = CompanyUtils.GetCompanyById(GameContext, companyId);

        int loyalty = MarketingUtils.GetClientLoyalty(GameContext, companyId, nicheType);
        int level = c.product.Segments[nicheType];

        LoyaltyLabel.value = loyalty;
        LevelLabel.text = $"{level}LVL";

        long segmentIncome = CompanyEconomyUtils.GetIncomeBySegment(GameContext, companyId, nicheType);

        Income.text = $"+${ValueFormatter.Shorten(segmentIncome)}";

        string formattedSegmentName = EnumUtils.GetFormattedNicheName(nicheType);
        bool isPrimary = c.product.Niche == nicheType;

        IsSelectedNiche.enabled = !isPrimary;

        SegmentHint.SetHint($"Segment ({formattedSegmentName})\n");
        LoyaltyHint.SetHint($"Negative loyalty drastically inceases churn rate of clients in this segment!");
    }
}
