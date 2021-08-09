using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class FlagshipCompetitorsInDevelopmentScreenListView : ListView
{
    public override void SetItem<T>(Transform t, T entity)
    {
        var product = entity as GameEntity;

        var users = Marketing.GetUsers(product);

        var header = $"<b>#{index + 1} {product.company.Name}</b>";

        if (product.isFlagship)
        {
            header = Visuals.Colorize(header, Colors.COLOR_COMPANY_WHERE_I_AM_CEO);
        }

        var workers = product.team.Teams.Select(t1 => t1.Managers.Count).Sum();
        var marketers = product.team.Teams.Select(t1 => t1.Roles.Values.Count(r => r == WorkerRole.Marketer)).Sum();
        var coders = product.team.Teams.Select(t1 => t1.Roles.Values.Count(r => r == WorkerRole.Programmer)).Sum();


        var text = $"{header} {Format.Minify(users)} users & ({coders}/{marketers}) #{product.creationIndex}";

        text += $"\nMONEY: " + Format.Money(Economy.BalanceOf(product));
        var profit = Economy.GetProfit(Q, product, true).Minify().MinifyValues();
        text += "\n" + profit.ToString();

        text += $"\n{GetGoals(product)}\n";

        text = GetMarketingActivity(text, product);
        text = GetKeyFeatures(text, product);

        text += "\n\n---------------------------------------";

        t.GetComponent<Text>().text = text;
        //AddIfAbsent<Hint>(t.gameObject).SetHint(profit.ToString());
    }

    string GetMarketingActivity(string text, GameEntity product)
    {
        var churn = Marketing.GetChurnRate(product, Q);

        var joinedChannels = product.companyMarketingActivities.Channels.Keys
            .Select(k => Marketing.GetChannelClientGain(product, k))
            .Sum();
    //.OrderByDescending(k => k)
    //.Select(k => Format.Minify(k));

        text += $"\n{(int)churn}% channels: " + Marketing.GetActiveChannelsCount(product) + $"  +{Format.Minify(joinedChannels)}\n";
        //text += $"\n{(int)churn}% channels: " + Marketing.GetActiveChannelsCount(product) + $"  {string.Join(", ", joinedChannels)}\n";

        return text;
    }

    string GetKeyFeatures(string text, GameEntity product)
    {
        foreach (var f in Products.GetAllFeaturesForProduct())
        {
            var rating = (int)Products.GetFeatureRating(product, f.Name);
            bool isBest = Products.IsLeadingInFeature(product, f, Q);

            if (isBest && rating > 0)
                text += $"\n\t{f.Name} ({rating}) " + Visuals.Positive("+10% audience growth");
        }

        return text;
    }

    string GetGoals(GameEntity product)
    {
        if (product.companyGoal.Goals.Any())
        {
            var goal = product.companyGoal.Goals.First();
            
            return "<b>" + goal + "</b>\n" + string.Join(", ", Investments.GetProductActions(product, goal));
        }

        var released = product.isRelease ? "RELEASED" : "prototype";

        return $"<b>NO Goals</b> {released}\n" + string.Join(", ", product.completedGoals.Goals);
    }

    public override void ViewRender()
    {
        var competitors = Companies.GetDirectCompetitors(Flagship, Q, true)
            .Where(c => c.companyGoal.Goals.Any())
            .OrderByDescending(Marketing.GetUsers)
            ;

        SetItems(competitors);
    }
}
