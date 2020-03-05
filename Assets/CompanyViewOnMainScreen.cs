using Assets.Core;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CompanyViewOnMainScreen : View
{
    public Text Name;
    public Hint CompanyHint;
    public Text Clients;
    public LinkToProjectView LinkToProjectView;

    public Text PositionOnMarket;

    public Image Image;
    public Image DarkImage;

    public Text Profitability;

    public Text Brand;
    public GameObject BrandIcon;

    public HireWorker HireWorker;


    public Text Expertise;

    bool EnableDarkTheme;

    GameEntity company;

    public void SetEntity(GameEntity c, bool darkImage)
    {
        company = c;
        EnableDarkTheme = darkImage;

        Render();
    }

    void Render()
    {
        if (company == null)
            return;

        var id = company.company.Id;

        var clients = Marketing.GetClients(company);
        var churn = Marketing.GetChurnRate(Q, company);
        var churnClients = Marketing.GetChurnClients(Q, id);

        var profit = Economy.GetProfit(Q, id);

        bool hasControl = Companies.GetControlInCompany(MyCompany, company, Q) > 0;

        var nameColor = hasControl ? Colors.COLOR_CONTROL : Colors.COLOR_NEUTRAL;
        var profitColor = profit >= 0 ? Colors.COLOR_POSITIVE : Colors.COLOR_NEGATIVE;

        var positionOnMarket = Markets.GetPositionOnMarket(Q, company) + 1;

        var brand = (int)company.branding.BrandPower;
        var brandChange = Marketing.GetBrandChange(company, Q);

        var effeciency = Products.GetTeamEffeciency(Q, company);



        SetEmblemColor();

        Clients.text = Format.Minify(clients);

        CompanyHint.SetHint(GetCompanyHint());

        Expertise.text = $"Team Speed: {effeciency}%";
        Expertise.color = Visuals.GetGradientColor(0, 100, effeciency);

        Name.text = company.company.Name;
        Name.color = Visuals.GetColorFromString(nameColor);

        Profitability.text = Format.Money(profit);
        Profitability.color = Visuals.GetColorFromString(profitColor);

        PositionOnMarket.text = $"#{positionOnMarket}";

        var change = brandChange.Sum();
        Brand.text = $"{brand} ({Format.Sign(change)})";
        Brand.color = Visuals.GetGradientColor(0, 100, brand);



        UpdateIfNecessary(BrandIcon,    company.isRelease);
        UpdateIfNecessary(Brand,        company.isRelease);


        // buttons

        // set
        LinkToProjectView.CompanyId = id;
        HireWorker.companyId = id;



        // render
        var max = Products.GetNecessaryAmountOfWorkers(company, Q);
        var workers = Teams.GetAmountOfWorkers(company, Q);


        HireWorker.GetComponentInChildren<TextMeshProUGUI>().text = $"Hire Worker ({workers}/{max})";
        HireWorker.GetComponentInChildren<Button>().interactable = workers < max;
        HireWorker.GetComponentInChildren<Hint>().SetHint(workers < max ?
            "Hiring workers will increase development speed"
            :
            Visuals.Negative("You reached max limit of workers") + "\n\nTo increase this limit, hire TOP managers"
            );
    }

    void UpdateIfNecessary(MonoBehaviour mb, bool condition) => UpdateIfNecessary(mb.gameObject, condition);
    void UpdateIfNecessary(GameObject go, bool condition)
    {
        if (go.activeSelf != condition)
            go.SetActive(condition);
    }

    public override void ViewRender()
    {
        base.ViewRender();

        var flagship = Companies.GetFlagship(Q, MyCompany);

        if (flagship == null)
            return;

        company = flagship;

        Render();
    }

    string GetProfitDescription()
    {
        var profit = Economy.GetProfit(Q, company.company.Id);

        return profit > 0 ?
            Visuals.Positive($"Profit: +{Format.Money(profit)}") :
            Visuals.Negative($"Loss: {Format.Money(-profit)}");
    }
    
    void SetEmblemColor()
    {
        Image.color = Companies.GetCompanyUniqueColor(company.company.Id);

        var col = DarkImage.color;
        var a = EnableDarkTheme ? 219f : 126f;

        DarkImage.color = new Color(col.r, col.g, col.b, a / 255f);
        //DarkImage.gameObject.SetActive(DisableDarkTheme);
    }

    string GetCompanyHint()
    {
        StringBuilder hint = new StringBuilder(company.company.Name);

        var position = Markets.GetPositionOnMarket(Q, company);

        // quality description
        var conceptStatus = Products.GetConceptStatus(company, Q);

        var concept = "???";

        switch (conceptStatus)
        {
            case ConceptStatus.Leader: concept = "Sets Trends!"; break;
            case ConceptStatus.Outdated: concept = "Outdated"; break;
            case ConceptStatus.Relevant: concept = "Relevant"; break;
        }

        //
        var level = Products.GetProductLevel(company);

        var clients = Marketing.GetClients(company);

        var brand = (int)company.branding.BrandPower;

        hint.AppendLine();
        hint.AppendLine();

        hint.AppendLine($"Clients: {Format.Minify(clients)} (#{position + 1})");
        hint.AppendLine($"Brand: {brand}");

        hint.AppendLine();

        hint.AppendLine($"Concept: {level}LVL ({concept})");

        hint.AppendLine();
        hint.AppendLine();

        hint.AppendLine(GetProfitDescription());

        return hint.ToString();
    }
}