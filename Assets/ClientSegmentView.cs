using Assets.Utils;
using Assets.Utils.Formatting;
using System;
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

    public ColoredValueGradient Churn;
    public Hint ChurnHint;

    public Hint SegmentHint;
    public Hint LoyaltyHint;

    public GameObject IdeaIcon;
    public GameObject BrandIcon;

    public SetTargetUserType SetTargetUserType;

    public Text SegmentBonus;

    UserType userType;
    int companyId;

    public void SetEntity(UserType userType, int companyId)
    {
        this.userType = userType;
        this.companyId = companyId;

        Render();
    }

    public void Render()
    {
        var c = CompanyUtils.GetCompanyById(GameContext, companyId);

        UserTypeLabel.text = EnumUtils.GetFormattedUserType(userType);

        LoyaltyLabel.value = MarketingUtils.GetClientLoyalty(GameContext, companyId, userType);

        SetTargetUserType.SetUserType(userType);

        LevelLabel.text = $"{c.product.Segments[userType]}LVL";

        RenderAudience(userType, c);

        RenderChurn(userType, c);
        
        RenderSegmentIncome(companyId, userType);

        RenderSegmentHint(userType);

        RenderLoyaltyHint(companyId, userType);

        IdeaIcon.SetActive(userType != UserType.Regular);
        BrandIcon.SetActive(userType == UserType.Regular);

        SegmentBonus.text = $"+25";
    }



    private void RenderChurn(UserType userType, GameEntity c)
    {
        int churn = MarketingUtils.GetChurnRate(GameContext, c.company.Id, userType);
        int baseValue = MarketingUtils.GetUserTypeBaseValue(userType);
        int fromLoyalty = MarketingUtils.GetChurnRateLoyaltyPart(GameContext, c.company.Id, userType);

        Churn.minValue = baseValue;
        Churn.maxValue = baseValue + 10;
        Churn.value = churn;

        BonusContainer bonus = new BonusContainer(new BonusDescription { Name = "Churn rate", Value = churn }, true);
        bonus.Append(new BonusDescription { Name = $"Base for {EnumUtils.GetFormattedUserType(userType)}", Value = baseValue, Dimension = "%" });
        bonus.Append(new BonusDescription { Name = "From loyalty", Value = fromLoyalty, Dimension = "%" });

        ChurnHint.SetHint(bonus.ToString(true));
    }

    void RenderAudience(UserType userType, GameEntity c)
    {
        AudienceSize.text = ValueFormatter.Shorten(c.marketing.Segments[userType]);

        StringBuilder hint = new StringBuilder();

        int churn = MarketingUtils.GetChurnRate(GameContext, c.company.Id, userType);

        hint.AppendFormat("Due to our churn rate ({0}%)", churn);
        hint.AppendFormat(" we lose <color={0}>{1}</color> clients each month\n", VisualConstants.COLOR_NEGATIVE, 500);

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
