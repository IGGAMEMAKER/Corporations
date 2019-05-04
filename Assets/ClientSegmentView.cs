using Assets.Utils;
using Assets.Utils.Formatting;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class ClientSegmentView : View
{
    public Text UserTypeLabel;

    public ColoredValuePositiveOrNegative LoyaltyLabel;
    public Text LevelLabel;
    public Text Income;

    public Text AudienceSize;
    public Hint AudienceHint;

    public Hint SegmentHint;
    public Hint LoyaltyHint;

    public GameObject IdeaIcon;
    public GameObject BrandIcon;

    public Text SegmentBonus;

    public void Render(UserType userType, int companyId)
    {
        var c = CompanyUtils.GetCompanyById(GameContext, companyId);

        UserTypeLabel.text = EnumUtils.GetFormattedUserType(userType);

        LoyaltyLabel.value = MarketingUtils.GetClientLoyalty(GameContext, companyId, userType);

        LevelLabel.text = $"{c.product.Segments[userType]}LVL";

        RenderAudience(userType, c);
        
        RenderSegmentIncome(companyId, userType);

        RenderSegmentHint(userType);

        RenderLoyaltyHint(companyId, userType);

        IdeaIcon.SetActive(userType != UserType.Regular);
        BrandIcon.SetActive(userType == UserType.Regular);

        SegmentBonus.text = $"+25";
    }

    void RenderAudience(UserType userType, GameEntity c)
    {
        AudienceSize.text = $"{ValueFormatter.Shorten(c.marketing.Segments[userType])}";

        StringBuilder hint = new StringBuilder();

        hint.AppendLine("Due to 5% churn rate");
        hint.AppendFormat("We lose <color={0}>500</color> clients each month\n", VisualConstants.COLOR_NEGATIVE);

        if (userType != UserType.Core)
        {
            UserType next = userType == UserType.Newbie ? UserType.Regular : UserType.Core;

            hint.AppendFormat("<color={0}>Also, {2} clients will be promoted to {1}</color>",
                VisualConstants.COLOR_POSITIVE,
                EnumUtils.GetFormattedUserType(next),
                35);
        }

        hint.AppendLine();

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

    string GetSegmentHint(UserType userType)
    {
        string formattedSegmentName = EnumUtils.GetFormattedUserType(userType);

        return $"{formattedSegmentName}\n";
    }

    void RenderSegmentHint(UserType userType)
    {
        SegmentHint.SetHint(GetSegmentHint(userType));
    }
}
