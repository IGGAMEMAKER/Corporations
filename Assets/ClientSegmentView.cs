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

        long segmentIncome = CompanyEconomyUtils.GetIncomeBySegment(GameContext, companyId, userType);

        string formattedSegmentName = EnumUtils.GetFormattedUserType(userType);

        IsSelectedNiche.enabled = c.targetUserType.UserType == userType;

        LoyaltyLabel.value = loyalty;
        LevelLabel.text = $"{level}LVL";
        Income.text = $"+${ValueFormatter.Shorten(segmentIncome)}";

        SegmentHint.SetHint($"{formattedSegmentName}\n");
        LoyaltyHint.SetHint($"Negative loyalty drastically inceases churn rate of clients in this segment!");
    }
}
