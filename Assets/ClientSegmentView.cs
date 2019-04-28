using Assets.Utils;
using Assets.Utils.Formatting;
using UnityEngine.UI;

public class ClientSegmentView : View
{
    public Image IsSelectedNiche;

    public ColoredValuePositiveOrNegative LoyaltyLabel;
    public Text LevelLabel;
    public Text Income;

    public Text AudienceSize;
    public ColoredValueGradient ChurnRate;

    public Hint SegmentHint;
    public Hint LoyaltyHint;

    public void Render(UserType userType, int companyId)
    {
        var c = CompanyUtils.GetCompanyById(GameContext, companyId);

        bool isSelected = c.targetUserType.UserType == userType;

        IsSelectedNiche.enabled = isSelected;


        LoyaltyLabel.value = MarketingUtils.GetClientLoyalty(GameContext, companyId, userType);

        LevelLabel.text = $"{c.product.Segments[userType]}LVL";

        AudienceSize.text = $"{ValueFormatter.Shorten(c.marketing.Segments[userType])}";
        ChurnRate.value = -5;

        RenderSegmentIncome(companyId, userType);

        RenderSegmentHint(isSelected, userType);

        RenderLoyaltyHint(companyId, userType);
    }

    void RenderSegmentIncome(int companyId, UserType userType)
    {
        long segmentIncome = CompanyEconomyUtils.GetIncomeBySegment(GameContext, companyId, userType);

        Income.text = $"+${ValueFormatter.Shorten(segmentIncome)}";
    }

    void RenderLoyaltyHint(int companyId, UserType userType)
    {
        LoyaltyHint.SetHint(MarketingUtils.GetClientLoyaltyDescription(GameContext, companyId, userType));
    }

    void RenderSegmentHint(bool isSelected, UserType userType)
    {
        string formattedSegmentName = EnumUtils.GetFormattedUserType(userType);

        string actionString = isSelected ?
            formattedSegmentName + " will receive improvements automatically, when platform updates"
            :
            $"Focus on {formattedSegmentName} and your app will be better for them";

        SegmentHint.SetHint($"{formattedSegmentName}\n\n{actionString}");
    }
}
