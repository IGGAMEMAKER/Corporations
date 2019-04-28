using Assets.Utils;
using Assets.Utils.Formatting;
using System.Text;
using UnityEngine.UI;

public class ClientSegmentView : View
{
    public Image IsSelectedNiche;

    public ColoredValuePositiveOrNegative LoyaltyLabel;
    public Text LevelLabel;
    public Text Income;

    public Text AudienceSize;
    public Hint AudienceHint;

    public Hint SegmentHint;
    public Hint LoyaltyHint;

    public void Render(UserType userType, int companyId)
    {
        var c = CompanyUtils.GetCompanyById(GameContext, companyId);

        bool isSelected = c.targetUserType.UserType == userType;

        IsSelectedNiche.enabled = isSelected;


        LoyaltyLabel.value = MarketingUtils.GetClientLoyalty(GameContext, companyId, userType);

        LevelLabel.text = $"{c.product.Segments[userType]}LVL";

        RenderAudience(userType, c);
        
        RenderSegmentIncome(companyId, userType);

        RenderSegmentHint(isSelected, userType);

        RenderLoyaltyHint(companyId, userType);
    }

    void RenderAudience(UserType userType, GameEntity c)
    {
        AudienceSize.text = $"{ValueFormatter.Shorten(c.marketing.Segments[userType])}";

        StringBuilder hint = new StringBuilder();

        hint.AppendLine("Due to 5% churn rate");
        hint.AppendLine("We lose 500 clients each month");
        hint.AppendLine("And 35 of them are promoted to next segment");

        AudienceHint.SetHint(hint.ToString());
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
