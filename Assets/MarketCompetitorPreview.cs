using Assets.Utils;
using UnityEngine.UI;

public class MarketCompetitorPreview : View
{
    public Text CompanyNameLabel;
    public Image Panel;

    public Text Clients;

    public Text Concept;

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

        RenderCompanyName(e);

        RenderCompanyInfo(e);

        UpdateLinkToCompany(e);
    }

    void RenderPanel()
    {
        var daughter = Companies.IsDaughterOfCompany(MyCompany, entity);

        Panel.color = GetPanelColor(daughter);
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

    private void RenderCompanyInfo(GameEntity e)
    {
        var clients = MarketingUtils.GetClients(e);
        var concept = Products.GetProductLevel(e);

        var brand = (int) e.branding.BrandPower;

        var newClients = MarketingUtils.GetAudienceGrowth(e, GameContext);

        //Clients.text = Format.MinifyToInteger(clients) + " users";
        Clients.text = "+" + Format.Minify(newClients) + " users";

        //Concept.text = concept + "LVL";
        Concept.text = "Brand\n\n" + brand;
        //Concept.gameObject.GetComponent<Hint>().SetHint("");

        //Concept.text = Visuals.Colorize(brand.ToString(), Visuals.GetGradientColor(-100, 100, brand));

        //var change = MarketingUtils.GetMonthlyBrandPowerChange(e, GameContext)
        //    .RenderTitle();

        //var hint = change.ToString();
        //Concept.gameObject.GetComponent<Hint>().SetHint(hint);
    }
}
