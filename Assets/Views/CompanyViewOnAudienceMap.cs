using Assets.Core;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class CompanyViewOnAudienceMap : View/*, IPointerEnterHandler, IPointerExitHandler*/
{
    public Text Name;
    public Hint CompanyHint;
    public LinkToProjectView LinkToProjectView;

    public Image BorderImage;

    public Image Image;

    public GameObject LoyaltyImage;
    public Text Loyalty;
    public Hint LoyaltyHint;

    [FormerlySerializedAs("Growth1")] [FormerlySerializedAs("Growth")] public Text Users;
    [FormerlySerializedAs("Users")] public Text Growth;

    public AnimationSpawner AnimationSpawner;

    private int loyaltyClamped = 0;
    private long previousUsers = 0;
    
    GameEntity company;

    public void SetEntity(GameEntity c)
    {
        company = c;

        ViewRender();
    }

    public override void ViewRender()
    {
        base.ViewRender();
        
        bool hasControl = Companies.GetControlInCompany(MyCompany, company, Q) > 0;

        var shortName = Companies.GetShortName(company);

        // SetEmblemColor();


        Hide(Name);
        RenderBorderImage(hasControl);
        EmphasizeCompanySize();

        RenderLoyalty();

        
        RenderUsers();
        // RenderGrowth();

        // BlinkDaughterCompanyIfThereAreTroublesWithLoyalty();

        LinkToProjectView.CompanyId = company.company.Id;

        CompanyHint.SetHint(GetCompanyHint(hasControl));
    }

    void BlinkDaughterCompanyIfThereAreTroublesWithLoyalty()
    {
        GetComponent<Blinker>().enabled = company.isFlagship && Marketing.GetAppQuality(company) < 0;
    }

    void RenderLoyalty()
    {
        var loyalty = Marketing.GetPositioningQuality(company);
        var loyaltySum = loyalty.Sum();
        var loyaltyString = loyaltySum.ToString("0.00");

        Loyalty.text = ((int)loyaltySum).ToString();// loyalty.Sum().ToString("0");
        LoyaltyHint.SetHint($"<size=30>App quality={loyaltyString}</size>\n" + loyalty.SortByModule().HideZeroes().ToString());

        if ((int) loyaltySum > loyaltyClamped)
        {
            AnimationSpawner.SpawnJumpingAnimation(transform);

            loyaltyClamped = (int) loyaltySum;
        }
    }

    void RenderBorderImage(bool hasControl)
    {
        BorderImage.color = Visuals.GetColorFromString(hasControl ? Colors.COLOR_CONTROL : Colors.COLOR_VIOLET);
    }

    void RenderUsers()
    {
        var users = Marketing.GetUsers(company);


        var growth = Marketing.GetAudienceChange(company, Q, true);
        var growthSum = growth.Sum();
        var growthSign = Format.SignOf(growthSum);

        var growthPhrase = $"<b>{growthSign}{Format.MinifyToInteger(growthSum)}</b>";
        var growthDescription = Visuals.Colorize(growthPhrase, growthSum > 0); 

        Growth.text = $"<b>{Format.MinifyToInteger(users)}</b> users";
        if (growthSum != 0)
        {
            Growth.text += $" ({growthDescription})";
        }
        
        Users.text = $"<b>{company.company.Name}</b>";

        if (users != previousUsers)
        {
            AnimationSpawner.Spawn(Visuals.Colorize(growthPhrase + " users", growthSum > 0), null);
            // PlaySound();
            previousUsers = users;
        }
    }

    void RenderGrowth()
    {
        var growth = Marketing.GetAudienceChange(company, Q, true);
        var growthSum = growth.Sum();

        // Growth.text = Visuals.PositiveOrNegativeMinified(growthSum) + " users";
        Users.GetComponent<Hint>().SetHint("Users will change by " + Visuals.Colorize(growth.Sum()) + " because " + growth.ToString());
    }

    void EmphasizeCompanySize()
    {
        var position = Companies.GetDirectCompetitors(company, Q, true)
            .OrderByDescending(Marketing.GetUsers)
            .Select((c, i) => new { c, i, share = Marketing.GetUsers(c) })
            .First(cc => cc.c.company.Id == company.company.Id)
            .i;
        // Markets.GetPositionOnMarket(Q, company);

        var max = 1f;
        var min = 0.5f;

        var scale = max / (1 + position);

        if (position == 0)
            scale = max;
        else if (position == 1)
            scale = max * 0.8f;
        else if (position == 2 || position == 3)
            scale = max * 0.7f;
        else
            scale = min;

        scale = Mathf.Clamp(scale, min, max);

        transform.localScale = new Vector3(scale, scale, 1);
    }

    void SetEmblemColor()
    {
        Image.color = Companies.GetCompanyUniqueColor(company.company.Id);
    }

    public static int GetBudgetEstimation(GameEntity product, GameContext gameContext)
    {
        var competitors = Companies.GetCompetitorsOf(product, gameContext, true);

        var position = competitors.OrderByDescending(c => Economy.GetProductMaintenance(c, Q)).ToList().FindIndex(s => s.company.Id == product.company.Id);

        return Mathf.Clamp(5 - position, 1, 5);
    }

    public static int GetTeamEstimation(GameEntity product, GameContext gameContext)
    {
        var competitors = Companies.GetCompetitorsOf(product, gameContext, true);

        var position = competitors.OrderByDescending(c => c.team.Teams.Count).ToList().FindIndex(s => s.company.Id == product.company.Id);

        return Mathf.Clamp(5 - position, 1, 5);
    }

    public static int GetManagerEstimation(GameEntity product, GameContext gameContext)
    {
        //var competitors = Companies.GetCompetitorsOfCompany(product, gameContext, true);

        var avgStrength = Teams.GetTeamAverageStrength(product, gameContext);

        if (avgStrength < 50)
            return 1;

        if (avgStrength < 60)
            return 2;

        if (avgStrength < 70)
            return 3;

        if (avgStrength < 80)
            return 4;

        return 5;

        //return competitors.OrderByDescending(c => c.team.Teams.Count).ToList().FindIndex(s => s.company.Id == product.company.Id);

        return Random.Range(1, 5);
    }

    string GetCompanyHint(bool hasControl)
    {
        var position = Markets.GetPositionOnMarket(Q, company);

        // #{position + 1}
        var clients = Marketing.GetUsers(company);

        var change = Marketing.GetAudienceChange(company, Q);

        var changeFormatted = $"<b>{Format.SignOf(change) + Format.Minify(change)}</b> weekly";

        var budgetFormatted = Format.Money(Economy.GetProductMaintenance(company, Q), true);

        var teamStars    = GetTeamEstimation(company, Q);
        var budgetStars  = GetBudgetEstimation(company, Q);
        var managerStars = GetManagerEstimation(company, Q);

        var productStrength = (budgetStars + managerStars + teamStars) / 3;
        //SetAmountOfStars.SetStars(productStrength);

        StringBuilder hint = new StringBuilder($"<size=35>{Visuals.Colorize(company.company.Name, hasControl ? Colors.COLOR_CONTROL : Colors.COLOR_NEUTRAL)}</size>");

        if (hasControl)
            hint.AppendLine(Visuals.Colorize("\n\nYour company", Colors.COLOR_CONTROL));
        else
        {
            hint.AppendLine("\n<b>Goals</b>: " + string.Join("\n", company.companyGoal.Goals.Select(g => g.GetFormattedName())));
        }

        hint.AppendLine($"\n\nUsers: <b>{Format.Minify(clients)}</b>");
        hint.AppendLine($"{Visuals.Colorize(changeFormatted, change >=0)}\n");
        //hint.AppendLine($"Users: <b>{Format.Minify(clients)}</b> {Visuals.Colorize(changeFormatted, change >=0)}\n");
        //hint.AppendLine(RenderStars("Product", techStars));

        //hint.AppendLine();

        hint.AppendLine($"\n\nApp quality: <size=25><b>{Visuals.Colorize((long)Marketing.GetAppQuality(company))}</b></size>");
        // STRENGTH
        //hint.AppendLine(RenderStars("Company strength", productStrength));
        
        //hint.AppendLine(RenderStars("Budget", budgetStars));

        //hint.AppendLine();
        //hint.AppendLine(RenderStars("Managers", managerStars));
        //hint.AppendLine(RenderStars("Teams", teamStars));


        //hint.AppendLine($"\n<b>{company.team.Teams.Count}</b> teams");
        //hint.AppendLine($"Managers: <b>{Teams.GetTeamAverageStrength(company, Q)}LVL</b>");

        //hint.AppendLine($"\nBudget: <b>{Visuals.Positive(budgetFormatted)}</b>");

        //hint.AppendLine(GetProfitDescription());

        //if (hasControl)
        //    hint.AppendLine(Visuals.Colorize("\nYour company", Colors.COLOR_CONTROL));

        return hint.ToString();
    }

    string RenderStars(string title, int stars)
    {
        string str = $"<b>{title}</b>\n<size=40>";

        for (var i = 0; i < 5; i++)
        {
            str += Visuals.Colorize("<b>*</b>", i < stars ? Colors.COLOR_GOLD : Colors.COLOR_NEUTRAL);
        }

        return str + "</size>";
    }

    //public void OnPointerEnter(PointerEventData eventData)
    //{
    //    Show(SetAmountOfStars);
    //    Show(LoyaltyImage);
    //}

    //public void OnPointerExit(PointerEventData eventData)
    //{
    //    Hide(SetAmountOfStars);
    //    Hide(LoyaltyImage);
    //}
}
