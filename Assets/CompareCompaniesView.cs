using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class CompareCompaniesView : View
{
    public Text Name1;
    public Text Name2;

    public Text MaxRating1;
    public Text MaxRating2;

    public Text MarketingEfficiency1;
    public Text MarketingEfficiency2;

    public Text MarketingBudget1;
    public Text MarketingBudget2;

    public Text Funding1;
    public Text Funding2;

    public Text Employees1;
    public Text Employees2;

    int offset = 0;

    GameEntity[] competitors => Companies.GetCompetitorsOfCompany(SelectedCompany, Q, false)
        .Where(c => c.productPositioning.Positioning == SelectedCompany.productPositioning.Positioning).ToArray();

    public override void ViewRender()
    {
        base.ViewRender();

        var company1 = SelectedCompany;

        //var competitors = Companies.GetCompetitorsOfCompany(company1, Q, false).ToArray();

        var list = competitors;
        var company2 = list.Count() > 0 ? competitors[offset] : company1;

        Name1.text = RenderName(company1);
        Name2.text = RenderName(company2);

        CompareData("Max feature lvl", MaxRating1, MaxRating2, 
            Products.GetFeatureRatingCap(company1), Products.GetFeatureRatingCap(company2));

        //SetHints(MaxRating1, )

        CompareData("Marketing quality", MarketingEfficiency1, MarketingEfficiency2, 
            Teams.GetMarketingEfficiency(company1), Teams.GetMarketingEfficiency(company2));

        CompareData("Employees", Employees1, Employees2, 
            Teams.GetTotalEmployees(company1), Teams.GetTotalEmployees(company2));



        CompareData("Marketing Budget", MarketingBudget1, MarketingBudget2, 
            Economy.GetMarketingBudget(company1, Q), Economy.GetMarketingBudget(company2, Q));

        CompareData("Funding", Funding1, Funding2, 
            Economy.GetFundingBudget(company1, Q), Economy.GetFundingBudget(company2, Q));
    }

    public void NextCompetitor()
    {
        offset++;

        var maxCount = competitors.Count();
        if (offset >= maxCount)
        {
            offset = maxCount - 1;
        }

        PlaySound(Assets.Sound.StandardClick);

        ViewRender();
    }

    public void PreviousCompetitor()
    {
        offset--;

        if (offset < 0)
            offset = 0;

        PlaySound(Assets.Sound.StandardClick);

        ViewRender();
    }

    void SetHints(GameObject obj, string hint)
    {

    }

    void CompareData(string tag, Text Left, Text Right, float left, float right)
    {
        CompareData(tag, Left, Right, left.ToString("0.0"), right.ToString("0.0"), left > right, left == right);
    }

    void CompareData(string tag, Text Left, Text Right, long left, long right)
    {
        var formattedLeft = Format.Minify(left);
        var formattedRight = Format.Minify(right);

        CompareData(tag, Left, Right, formattedLeft, formattedRight, left > right, left == right);
    }

    void CompareData(string tag, Text Left, Text Right, string formattedLeft, string formattedRight, bool leftIsBigger, bool equal)
    {
        Color leftColor;
        Color rightColor;

        if (leftIsBigger)
        {
            leftColor = Visuals.GetColorFromString(Colors.COLOR_POSITIVE);
            rightColor = Visuals.GetColorFromString(Colors.COLOR_NEGATIVE);
        }
        else if (equal)
        {
            leftColor = Visuals.GetColorFromString(Colors.COLOR_NEUTRAL);
            rightColor = Visuals.GetColorFromString(Colors.COLOR_NEUTRAL);
        }
        else
        {
            leftColor = Visuals.GetColorFromString(Colors.COLOR_NEGATIVE);
            rightColor = Visuals.GetColorFromString(Colors.COLOR_POSITIVE);
        }

        Left.text = $"{tag}\n<size=30><b>{Visuals.Colorize(formattedLeft, leftColor)}</b></size>";
        Right.text = $"{tag}\n<size=30><b>{Visuals.Colorize(formattedRight, rightColor)}</b></size>";
    }

    private void OnEnable()
    {
        offset = 0;

        ViewRender();
    }
}
