using Assets.Core;
using Assets.Visuals;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class NewsListView : ListView
{
    List<string> Messages = new List<string>();

    List<float> LastFeatures;
    List<GameEntity> LastClients;


    public override void SetItem<T>(Transform t, T entity)
    {
        var message = entity as string;

        t.GetComponent<MockText>().SetEntity(message);

        /*if (index == 0)
            AddIfAbsent<EnlargeOnAppearance>(t.gameObject);*/
    }

    private void OnEnable()
    {
        UpdateFeatures();
        UpdateClients();
    }

    void UpdateFeatures()
    {
        var features = Markets.GetCalculatedMarketRequirements(Markets.Get(Q, Flagship), Q);
        LastFeatures = Markets.CopyMarketRequirements(features);
    }

    void UpdateClients()
    {
        LastClients = GetClients();
    }

    List<GameEntity> GetClients()
    {
        var competitors = Companies.GetDirectCompetitors(Flagship, Q, true);

        return competitors.OrderByDescending(c => Marketing.GetUsers(c)).ToList();
    }

    void NotifyAboutNewLeader(IEnumerable<GameEntity> competitors, string feature)
    {
        var leader = competitors.OrderByDescending(c => Products.GetFeatureRating(c, feature)).First();

        if (leader.isFlagship)
        {
            var monetisation = feature.ToLower().Contains("ad");

            if (!monetisation)
                NotificationUtils.AddSimplePopup(Q, "We are leading in feature " + feature, "This we get more users on each campaign");

            PrintMessage(Visuals.Colorize("We are best in " + feature + "!!!", Colors.COLOR_BEST));
        }
        else
        {
            var max = (int)Markets.GetMaxFeatureLVL(competitors, feature);

            PrintMessage(leader.company.Name + " LEADS IN " + feature + $"  <b>{max}LV</b>");
        }
    }

    void NotifyAboutFeatureDiff2(NewProductFeature[] allfeatures, IEnumerable<GameEntity> competitors)
    {
        bool redraw = false;

        var reqs = Markets.GetCalculatedMarketRequirements(Markets.Get(Q, Flagship), Q);


        for (var i = 0; i < LastFeatures.Count; i++)
        {
            var feature = allfeatures[i].Name;

            var was = (int)LastFeatures[i];
            var now = (int)reqs[i];

            if (now <= was)
                continue;

            NotifyAboutNewLeader(competitors, feature);

            redraw = true;
        }

        if (redraw)
        {
            UpdateFeatures();
            Redraw();
        }
    }

    void Redraw()
    {
        SetItems(Messages.Take(3));
    }

    void PrintMessage(string message)
    {
        Messages.Insert(0, message);
    }

    public override void ViewRender()
    {
        base.ViewRender();

        var product = Flagship;

        var allfeatures = Products.GetAllFeaturesForProduct();
        var competitors = Companies.GetDirectCompetitors(product, Q, true);


        NotifyAboutFeatureDiff2(allfeatures, competitors);

        var was = LastClients.FindIndex(c => c.isFlagship);
        UpdateClients();

        var now = LastClients.FindIndex(c => c.isFlagship);

        if (now > was)
        {
            var competitor = LastClients[now - 1];
            PrintMessage(Visuals.Negative($"{competitor.company.Name} has more users than us"));

            //NotificationUtils.AddSimplePopup(Q, "You were outcompeted by " + competitor.company.Name, Visuals.Negative("They have more users, than us"));
        }

        if (now < was)
        {
            var competitorID = now + 1;

            if (LastClients.Count > competitorID)
            {
                var competitor = LastClients[competitorID];
                PrintMessage(Visuals.Positive($"We have more users than {competitor.company.Name}"));

                //NotificationUtils.AddSimplePopup(Q, "You are outrunning " + competitor.company.Name, Visuals.Positive($"We have more users, than them"));
            }
        }
    }
}
