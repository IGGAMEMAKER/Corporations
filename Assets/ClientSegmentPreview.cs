using Assets.Utils;
using Assets.Utils.Formatting;
using System;
using UnityEngine.UI;

public class ClientSegmentPreview : View
{
    public Text UserTypeLabel;

    public ColoredValuePositiveOrNegative LoyaltyLabel;
    public Text Income;

    public Text AudienceSize;
    public Hint AudienceHint;

    public Hint LoyaltyHint;

    public UpdateSegmentController UpdateSegmentController;

    public CooldownView SegmentCooldownView;

    UserType UserType;
    int CompanyId;

    public void SetEntity(UserType userType, int companyId)
    {
        UserType = userType;
        CompanyId = companyId;

        SegmentCooldownView.SetSegmentForImprovements(userType);

        Render();
    }

    public override void ViewRender()
    {
        base.ViewRender();

        Render();
    }

    public void Render()
    {
        var c = CompanyUtils.GetCompanyById(GameContext, CompanyId);

        UserTypeLabel.text = EnumUtils.GetFormattedUserType(UserType);


        RenderAudience(UserType, c);

        RenderSegmentIncome(CompanyId, UserType);

        RenderLoyalty(CompanyId, UserType);

        UpdateSegmentController.SetSegment(UserType);
        UpdateSegmentController.gameObject.GetComponent<CheckSegmentImprovementResources>().SetSegment(UserType);
    }

    void RenderAudience(UserType userType, GameEntity c)
    {
        AudienceSize.text = $"{ValueFormatter.Shorten(c.marketing.Segments[userType])}";

        var hint = MarketingUtils.GetAudienceHint(GameContext, userType, c);

        AudienceHint.SetHint(hint);
    }

    void RenderSegmentIncome(int companyId, UserType userType)
    {
        var income = CompanyEconomyUtils.GetIncomeBySegment(GameContext, companyId, userType);

        Income.text = $"+${ValueFormatter.Shorten(Convert.ToInt64(income))}";
    }

    void RenderLoyalty(int companyId, UserType userType)
    {
        LoyaltyLabel.UpdateValue(MarketingUtils.GetClientLoyalty(GameContext, CompanyId, UserType));

        var hint = MarketingUtils.GetClientLoyaltyDescription(GameContext, companyId, userType);

        hint += "\n" + String.Join("\n", NicheUtils.GetCompetitorSegmentLevels(MyProductEntity, GameContext, userType));

        LoyaltyHint.SetHint(hint);
    }
}
