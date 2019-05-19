using Assets.Utils;
using Assets.Utils.Formatting;
using System;
using System.Text;
using UnityEngine.UI;

public class ClientSegmentPreview : View
{
    public Text UserTypeLabel;

    public ColoredValuePositiveOrNegative LoyaltyLabel;
    public Text Income;

    public Text AudienceSize;
    public Hint AudienceHint;

    public Hint SegmentHint;
    public Hint LoyaltyHint;

    public void Render(UserType userType, int companyId)
    {
        var c = CompanyUtils.GetCompanyById(GameContext, companyId);

        UserTypeLabel.text = EnumUtils.GetFormattedUserType(userType);

        LoyaltyLabel.value = MarketingUtils.GetClientLoyalty(GameContext, companyId, userType);

        RenderAudience(userType, c);

        RenderSegmentIncome(companyId, userType);

        RenderSegmentHint(userType);

        RenderLoyaltyHint(companyId, userType);
    }

    void RenderAudience(UserType userType, GameEntity c)
    {
        AudienceSize.text = $"{ValueFormatter.Shorten(c.marketing.Segments[userType])}";

        StringBuilder hint = new StringBuilder();

        int churn = MarketingUtils.GetChurnRate(GameContext, c.company.Id, userType);

        hint.AppendFormat("Due to our churn rate ({0}%)", churn);
        hint.AppendFormat(" we lose <color={0}>{1}</color> clients each month\n", VisualConstants.COLOR_NEGATIVE, MarketingUtils.GetChurnClients(GameContext, c.company.Id, userType));

        if (userType != UserType.Core)
        {
            UserType next = userType == UserType.Newbie ? UserType.Regular : UserType.Core;

            hint.AppendFormat("<color={0}>Also, {2} clients will be promoted to {1}</color>",
                VisualConstants.COLOR_POSITIVE,
                EnumUtils.GetFormattedUserType(next),
                MarketingUtils.GetPromotionClients(GameContext, c.company.Id, userType)
                );
        }

        hint.AppendLine();

        AudienceHint.SetHint(hint.ToString());
    }

    void RenderSegmentIncome(int companyId, UserType userType)
    {
        long segmentIncome = Convert.ToInt64(CompanyEconomyUtils.GetIncomeBySegment(GameContext, companyId, userType));

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
