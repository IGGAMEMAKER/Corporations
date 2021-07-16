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
        var text = $"{header} {Format.Minify(users)} users\n";

        var features = Products.GetAllFeaturesForProduct(product);

        foreach (var f in features)
        {
            var rating = (int)Products.GetFeatureRating(product, f.Name);
            if (Products.IsLeadingInFeature(product, f, Q, Competitors) && rating > 0)
                text += $"\n\t{f.Name} ({rating}) " + Visuals.Positive("+5% audience growth");
        }

        t.GetComponent<Text>().text = text;
    }

    IEnumerable<GameEntity> Competitors;

    public override void ViewRender()
    {
        Competitors = Companies.GetDirectCompetitors(Flagship, Q, true);

        SetItems(Competitors.OrderByDescending(c => Marketing.GetUsers(c)));
    }
}
