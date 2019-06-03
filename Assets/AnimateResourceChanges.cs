using Assets.Classes;
using Assets.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimateResourceChanges : View
{
    TeamResource Previous;
    long prevClients;
    long prevBrand;

    public GameObject ResourceTextPrefab;

    public GameObject BrandIcon;
    public GameObject MoneyIcon;
    public GameObject IdeasIcon;
    public GameObject ClientsIcon;


    public override void ViewRender()
    {
        base.ViewRender();

        var resources = MyProductEntity.companyResource.Resources;
        var clients = MarketingUtils.GetClients(MyProductEntity);
        var brand = MyProductEntity.marketing.BrandPower;


        var diff = TeamResource.Difference(resources, Previous);
        var clientsDiff = clients - prevClients;
        var brandDiff = brand - prevBrand;

        SpawnResource(diff.money, MoneyIcon, 0);
        SpawnResource(brandDiff, BrandIcon, 1);
        SpawnResource(clientsDiff, ClientsIcon, 2);
        SpawnResource(diff.ideaPoints, IdeasIcon, 3);

        // update values
        Previous = resources;
        prevClients = clients;
        prevBrand = brand;
    }

    void SpawnResource(long change, GameObject icon, int position)
    {
        if (change != 0)
        {
            var offset = position * 100;

            // render icon
            var obj = Instantiate(icon, transform);
            obj.transform.localPosition = new Vector3(-27.5f + offset, 0, 0);

            obj.AddComponent<AnimateResourceChange>();

            // render value
            var val = Instantiate(ResourceTextPrefab, transform);
            val.transform.localPosition = new Vector3(25.77f + offset, 0, 0);

            val.AddComponent<AnimateResourceChange>();
            val.GetComponent<Text>().text = Visuals.PositiveOrNegative(change);
        }
    }
}
