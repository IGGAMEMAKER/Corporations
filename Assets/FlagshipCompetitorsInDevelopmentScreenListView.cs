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

        var text = $"{header} {Format.Minify(users)} users & ({coders}/{marketers})\n";

        foreach (var f in Products.GetAllFeaturesForProduct())
        {
            var rating = (int)Products.GetFeatureRating(product, f.Name);
            bool isBest = Products.IsLeadingInFeature(product, f, Q);

            if (isBest && rating > 0)
                text += $"\n\t{f.Name} ({rating}) " + Visuals.Positive("+10% audience growth");
        }

        t.GetComponent<Text>().text = text;
        //AddIfAbsent<Button>(t.gameObject);
        //AddIfAbsent<LinkToProjectView>(t.gameObject).CompanyId = product.company.Id;
        AddIfAbsent<Hint>().SetHint(Economy.GetProfit(Q, product, true).ToString());
    }

    public override void ViewRender()
    {
        var competitors = Companies.GetDirectCompetitors(Flagship, Q, true).OrderByDescending(Marketing.GetUsers);

        SetItems(competitors);
    }
}
