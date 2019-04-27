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

        bool isSelected = c.targetUserType.UserType == userType;

        IsSelectedNiche.enabled = isSelected;

        LoyaltyLabel.value = loyalty;
        LevelLabel.text = $"{level}LVL";
        Income.text = $"+${ValueFormatter.Shorten(segmentIncome)}";

        string actionString = isSelected ? formattedSegmentName + " will receive improvements automatically, when platform updates" : $"Focus on {formattedSegmentName} and your app will be better for them";

        SegmentHint.SetHint($"{formattedSegmentName}\n{actionString}");
        LoyaltyHint.SetHint(MarketingUtils.GetClientLoyaltyDescription(GameContext, companyId, userType));
    }
}
