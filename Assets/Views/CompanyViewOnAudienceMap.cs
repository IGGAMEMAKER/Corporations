using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CompanyViewOnAudienceMap : View/*, IPointerEnterHandler, IPointerExitHandler*/
{
    public Text Name;
    public Hint CompanyHint;
    public LinkToProjectView LinkToProjectView;

    public Image Image;

    public GameObject LoyaltyImage;
    public Text Loyalty;
    public Hint LoyaltyHint;

    public Text Growth;

    public SetAmountOfStars SetAmountOfStars;

    GameEntity company;

    public void SetEntity(GameEntity c, int segmentId)
    {
        company = c;

        bool hasControl = Companies.GetControlInCompany(MyCompany, c, Q) > 0;

        var shortName = c.company.Name.Substring(0, 1) + c.company.Name.FirstOrDefault(char.IsDigit);
        var loyalty = Marketing.GetSegmentLoyalty(Q, company, segmentId, true);

        SetEmblemColor();

        Name.text = shortName;
        Name.color = Visuals.GetColorFromString(hasControl ? Colors.COLOR_CONTROL : Colors.COLOR_NEUTRAL);

        LinkToProjectView.CompanyId = c.company.Id;

        Loyalty.text = loyalty.Sum().ToString("0");
        LoyaltyHint.SetHint(loyalty.SortByModule().HideZeroes().ToString());

        CompanyHint.SetHint(GetCompanyHint(hasControl));

        var growth = Marketing.GetAudienceChange(company, Q, true);
        Growth.text = Visuals.PositiveOrNegativeMinified(growth.Sum());
        Growth.GetComponent<Hint>().SetHint("Users will change by " + Visuals.Colorize(growth.Sum()) + " because " + growth.ToString());

        //Hide(SetAmountOfStars);
        //Hide(LoyaltyImage);

        var position = Markets.GetPositionOnMarket(Q, company);

        var max = 1.8f;
        var scale = Mathf.Clamp(max / (1 + position), 0.8f, max);

        transform.localScale = new Vector3(scale, scale, 1);
    }

    void SetEmblemColor()
    {
        Image.color = Companies.GetCompanyUniqueColor(company.company.Id);
    }

    public static int GetBudgetEstimation(GameEntity product, GameContext gameContext)
    {
        var competitors = Companies.GetCompetitorsOfCompany(product, gameContext, true);

        var position = competitors.OrderByDescending(c => Economy.GetProductCompanyMaintenance(c, Q)).ToList().FindIndex(s => s.company.Id == product.company.Id);

        return Mathf.Clamp(5 - position, 1, 5);
    }

    public static int GetTeamEstimation(GameEntity product, GameContext gameContext)
    {
        var competitors = Companies.GetCompetitorsOfCompany(product, gameContext, true);

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
        var clients = Marketing.GetClients(company);

        var change = Marketing.GetAudienceChange(company, Q);

        var changeFormatted = $"<b>{Format.SignOf(change) + Format.Minify(change)}</b> weekly";

        var budgetFormatted = Format.MinifyMoney(Economy.GetProductCompanyMaintenance(company, Q));

        var teamStars    = GetTeamEstimation(company, Q);
        var budgetStars  = GetBudgetEstimation(company, Q);
        var managerStars = GetManagerEstimation(company, Q);

        var productStrength = (budgetStars + managerStars + teamStars) / 3;
        SetAmountOfStars.SetStars(productStrength);

        StringBuilder hint = new StringBuilder($"<size=35>{Visuals.Colorize(company.company.Name, hasControl ? Colors.COLOR_CONTROL : Colors.COLOR_NEUTRAL)}</size>");

        hint.AppendLine($"\n\nUsers: <b>{Format.Minify(clients)}</b>");
        hint.AppendLine($"{Visuals.Colorize(changeFormatted, change >=0)}\n");
        //hint.AppendLine($"Users: <b>{Format.Minify(clients)}</b> {Visuals.Colorize(changeFormatted, change >=0)}\n");
        //hint.AppendLine(RenderStars("Product", techStars));

        //hint.AppendLine();
        hint.AppendLine(RenderStars("Company strength", productStrength));
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
