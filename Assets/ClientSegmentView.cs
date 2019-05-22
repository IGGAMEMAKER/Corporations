using Assets.Utils;
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
        SetTargetUserType.gameObject.GetComponent<RenderFocusSegmentButtonView>().ViewRender();

        LevelLabel.text = $"{c.product.Segments[UserType]}LVL";

        RenderChurn(UserType, c);
        
        IdeaIcon.SetActive(UserType != UserType.Regular);
        BrandIcon.SetActive(UserType == UserType.Regular);

        SegmentBonus.text = $"+25";
    }

    private void RenderChurn(UserType userType, GameEntity c)
    {
        var bonus = MarketingUtils.GetChurnBonus(GameContext, c.company.Id, userType);

        int baseValue = MarketingUtils.GetUserTypeBaseValue(userType);

        Churn.minValue = baseValue;
        Churn.maxValue = baseValue + 10;
        Churn.UpdateValue(bonus.Sum());

        ChurnHint.SetHint(bonus.ToString(true));
    }
}
