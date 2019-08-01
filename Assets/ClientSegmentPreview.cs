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
    }

    public override void ViewRender()
    {
        base.ViewRender();
    }
}
