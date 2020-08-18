using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudiencesOnMainScreenListView : ListView
{
    [Header("Buttons")]
    public GameObject SetAsTargetAudience;

    public GameObject TeamPanel;

    public ProductUpgradeLinks MainAudienceInfo;
    public ProductUpgradeLinks AmountOfUsers;
    public ProductUpgradeLinks UserGrowth;
    public ProductUpgradeLinks Potential;
    public ProductUpgradeLinks FavouriteFeatures;
    public ProductUpgradeLinks HatedFeatures;
    public ProductUpgradeLinks MainInfo;

    public GameObject ButtonList;

    public override void SetItem<T>(Transform t, T entity, object data = null)
    {
        t.GetComponent<AudiencePreview>().SetEntity((AudienceInfo)(object)entity);
    }

    public override void ViewRender()
    {
        base.ViewRender();

        var audiences = Marketing.GetAudienceInfos();

        SetItems(audiences);
    }

    public override void OnItemSelected(int ind)
    {
        base.OnItemSelected(ind);

        if (ind == -1)
        {
            Hide(ButtonList);
            Show(TeamPanel);
        }
        else
        {
            Show(ButtonList);
            Hide(TeamPanel);

            // todo remove
            Hide(FavouriteFeatures);
            Hide(HatedFeatures);
            Hide(UserGrowth);

            bool isTargetAudience = Flagship.productTargetAudience.SegmentId == ind;
            bool hasClients = Flagship.marketing.ClientList.ContainsKey(ind);
            var clients = hasClients ? Flagship.marketing.ClientList[ind] : 0;

            Draw(SetAsTargetAudience, !isTargetAudience);
            Draw(MainAudienceInfo, isTargetAudience);
            Draw(AmountOfUsers, clients > 0);


            var audience = Marketing.GetAudienceInfos()[ind];

            var growing = Marketing.GetAudienceGrowthBySegment(Flagship, Q, ind);
            AmountOfUsers.Title.text = $"{Format.Minify(clients)} {audience.Name}\n" + Visuals.Colorize($"+{Format.Minify(growing)} weekly", growing >= 0);

            MainAudienceInfo.Title.text = Visuals.Colorize("<b>Our main audience</b>", Colors.COLOR_GOLD);

            var incomePerUser = 0.42f;
            var worth = (long)((double)audience.Size * incomePerUser);
            Potential.Title.text = $"<b>Potential: {Format.Minify(audience.Size)} users</b>\nworth {Format.MinifyMoney(worth)}";


            var income = Economy.GetIncomePerSegment(Q, Flagship, ind);

            MainInfo.Title.text = $"<b>{audience.Name}</b>\nIncome: {Visuals.Positive("+" + Format.MinifyMoney(income))}";
        }
    }

    private void OnEnable()
    {
        Hide(ButtonList);
        Show(TeamPanel);
    }
}
