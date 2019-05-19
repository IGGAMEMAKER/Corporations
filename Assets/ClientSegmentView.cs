using Assets.Utils;
using Assets.Utils.Formatting;
using System;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class ClientSegmentView : View
{
    public Text LevelLabel;
    public ColoredValueGradient Churn;
    public Hint ChurnHint;

    public GameObject IdeaIcon;
    public GameObject BrandIcon;

    public SetTargetUserType SetTargetUserType;

    public Text SegmentBonus;

    UserType UserType;
    int CompanyId;

    public void SetEntity(UserType userType, int companyId)
    {
        UserType = userType;
        CompanyId = companyId;

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

        SetTargetUserType.SetUserType(UserType);

        LevelLabel.text = $"{c.product.Segments[UserType]}LVL";

        RenderChurn(UserType, c);
        
        IdeaIcon.SetActive(UserType != UserType.Regular);
        BrandIcon.SetActive(UserType == UserType.Regular);

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
}
