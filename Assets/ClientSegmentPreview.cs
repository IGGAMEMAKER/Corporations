using Assets.Utils;
using System;
using UnityEngine.UI;

public class ClientSegmentPreview : View
{
    public Text UserTypeLabel;

    public ColoredValuePositiveOrNegative LoyaltyLabel;
    public ColoredValueGradient SegmentImprovements;
    public Text Income;
    public Text IncomeLabel;

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

        UserTypeLabel.text = ""; // EnumUtils.GetFormattedUserType(UserType);


        RenderAudience(UserType, c);

        RenderSegmentIncome(CompanyId, UserType);

        //RenderLoyalty(CompanyId, UserType);
        RenderSegmentLevel(UserType, c);

        RenderUpdateSegmentButton();
    }

    void RenderUpdateSegmentButton()
    {
        UpdateSegmentController.SetSegment(UserType);
        UpdateSegmentController.gameObject.GetComponent<CheckSegmentImprovementResources>().SetSegment(UserType);

        bool isInnovation = ProductUtils.IsWillInnovate(MyProductEntity, GameContext, UserType);
        UpdateSegmentController.gameObject.GetComponentInChildren<Text>().text = isInnovation ? "Make an innovation!" : "Improve app";
    }

    void RenderSegmentLevel(UserType userType, GameEntity company)
    {
        SegmentImprovements.UpdateValue(company.product.Concept);

        var demand = ProductUtils.GetSegmentMarketDemand(company, GameContext, userType);

        SegmentImprovements.minValue = demand - 5;
        SegmentImprovements.maxValue = demand;

        var apps = String.Join("\n", NicheUtils.GetCompetitorSegmentLevels(MyProductEntity, GameContext, userType));


        // for {EnumUtils.GetFormattedUserType(userType)}
        var hint = $"Best apps \n\n{apps}";

        LoyaltyHint.SetHint(hint);
    }

    void RenderAudience(UserType userType, GameEntity c)
    {
        AudienceSize.text = $"{Format.Minify(c.marketing.clients)}";

        var hint = MarketingUtils.GetAudienceHint(GameContext, userType, c);

        AudienceHint.SetHint(hint);
    }

    void RenderSegmentIncome(int companyId, UserType userType)
    {
        var income = CompanyEconomyUtils.GetIncomeBySegment(GameContext, companyId, userType);

        Income.text = $"+${Format.Minify(Convert.ToInt64(income))}";
    }

    void RenderLoyalty(int companyId, UserType userType)
    {
        //LoyaltyLabel.UpdateValue(MarketingUtils.GetClientLoyalty(GameContext, CompanyId, UserType));

        var hint = MarketingUtils.GetClientLoyaltyDescription(GameContext, companyId, userType);
        
        //hint += "\n" + String.Join("\n", NicheUtils.GetCompetitorSegmentLevels(MyProductEntity, GameContext, userType));

        //LoyaltyHint.SetHint(hint);
    }
}
