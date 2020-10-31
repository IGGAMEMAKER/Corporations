using Assets.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public partial class ProductDevelopmentSystem : OnPeriodChange
{
    void ManageSupport(GameEntity product)
    {
        int tries = 2;
        if (Products.IsNeedsMoreServers(product) && tries > 0)
        {
            tries--;
            AddServer(product);
        }

        var load = Products.GetServerLoad(product);
        var capacity = Products.GetServerCapacity(product);

        var growth = Marketing.GetAudienceChange(product, gameContext);

        if (load + growth >= capacity)
        {
            AddServer(product);
        }
    }


    void AddServer(GameEntity product)
    {
        var supportFeatures = Products.GetHighloadFeatures(product);
        var feature = supportFeatures[3];

        TryAddTask(product, new TeamTaskSupportFeature(feature));
    }
}
