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

    public void Render(UserType userType, int companyId)
    {
        var c = CompanyUtils.GetCompanyById(GameContext, companyId);

        int loyalty = MarketingUtils.GetClientLoyalty(GameContext, companyId, userType);
        int level = c.product.Segments[userType];

        LoyaltyLabel.value = loyalty;
        LevelLabel.text = $"{level}LVL";

        long segmentIncome = CompanyEconomyUtils.GetIncomeBySegment(GameContext, companyId, userType);

        Income.text = $"+${ValueFormatter.Shorten(segmentIncome)}";

        string formattedSegmentName = EnumUtils.GetFormattedUserType(userType);

        SegmentHint.SetHint($"{formattedSegmentName}\n");
        LoyaltyHint.SetHint($"Negative loyalty drastically inceases churn rate of clients in this segment!");
    }
}
