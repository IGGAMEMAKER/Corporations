using Assets.Core;
using UnityEngine.UI;

public class CompanyPreviewView : View
{
    public Text CompanyNameLabel;
    public Text CompanyTypeLabel;
    public Image Panel;
    public Text CEOLabel;
    public Hint CEOHint;

    public Text ShareCostLabel;

    public GameEntity entity;

    public void SetEntity(GameEntity company)
    {
        entity = company;

        Render(company);
    }

    public override void ViewRender()
    {
        base.ViewRender();

        Render(entity);
    }

    void Render(GameEntity e)
    {
        if (e == null)
            return;

        RenderPanel();

        RenderDependencyStatus(e);

        RenderCompanyName(e);
        RenderCompanyType(e);

        RenderCompanyCost(e);

        UpdateLinkToCompany(e);
    }

    void RenderDependencyStatus(GameEntity e)
    {
        var isDaughter = Companies.IsDaughterOf(MyGroupEntity, entity);

        if (CEOLabel != null)
        {
            Draw(CEOLabel, entity.isControlledByPlayer || isDaughter);

            if (isDaughter)
            {
                CEOLabel.text = "SUB";
                CEOHint.SetHint("This company is subsidiary (daughter) of " + MyGroupEntity.company.Name);
            }
            else if (entity.isControlledByPlayer)
            {
                CEOLabel.text = "CEO";
                CEOHint.SetHint("You are CEO of this company!");
            }
        }
    }

    void RenderPanel()
    {
        var inGroupScreens = CurrentScreen == ScreenMode.GroupManagementScreen || CurrentScreen == ScreenMode.ManageCompaniesScreen;

        if (Panel != null)
            Panel.color = GetPanelColor(entity == SelectedCompany && inGroupScreens);
    }

    void RenderCompanyType(GameEntity entity)
    {
        if (CompanyTypeLabel != null)
            CompanyTypeLabel.text = Enums.GetFormattedCompanyType(entity.company.CompanyType);
    }

    void RenderCompanyName(GameEntity entity)
    {
        CompanyNameLabel.text = entity.company.Name;
    }

    void UpdateLinkToCompany(GameEntity e)
    {
        var link = GetComponent<LinkToProjectView>();

        if (link != null)
            link.CompanyId = e.company.Id;
    }

    private void RenderCompanyCost(GameEntity e)
    {
        //var cost = Economy.CostOf(e, Q);
        var profit = Economy.GetProfit(Q, e);

        if (ShareCostLabel != null)
        {
            ShareCostLabel.text = Format.Money(profit);
            ShareCostLabel.color = Visuals.GetColorPositiveOrNegative(profit);
        }
    }
}