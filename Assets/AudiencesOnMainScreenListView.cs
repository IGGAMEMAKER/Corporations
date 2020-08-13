using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudiencesOnMainScreenListView : ListView
{
    [Header("Buttons")]
    public GameObject SetAsTargetAudience;

    public ProductUpgradeLinks MainAudienceInfo;
    public ProductUpgradeLinks AmountOfUsers;
    public ProductUpgradeLinks UserGrowth;
    public ProductUpgradeLinks Potential;
    public ProductUpgradeLinks FavouriteFeatures;
    public ProductUpgradeLinks HatedFeatures;

    public GameObject ButtonList;

    public override void SetItem<T>(Transform t, T entity, object data = null)
    {
        t.GetComponent<AudiencePreview>().SetEntity((AudienceInfo)(object)entity);
    }

    public override void ViewRender()
    {
        base.ViewRender();

        SetItems(Marketing.GetAudienceInfos());
    }

    public override void OnItemSelected(int ind)
    {
        base.OnItemSelected(ind);

        if (ind == -1)
            Hide(ButtonList);
        else
        {
            Show(ButtonList);

            var audience = Marketing.GetAudienceInfos()[ind];

            bool hasClients = Flagship.marketing.ClientList.ContainsKey(ind);
            var clients = hasClients ? Flagship.marketing.ClientList[ind] : 0;
            var growing = Marketing.GetAudienceGrowthBySegment(Flagship, Q, ind);

            Draw(SetAsTargetAudience, Flagship.productTargetAudience.SegmentId != ind);
            Draw(MainAudienceInfo, Flagship.productTargetAudience.SegmentId == ind);
            MainAudienceInfo.Title.text = Visuals.Colorize("<b>Our main audience</b>", Colors.COLOR_GOLD);

            Draw(AmountOfUsers, clients > 0);
            Draw(UserGrowth, clients > 0);
            AmountOfUsers.Title.text = $"<b>We have</b>\n{Format.Minify(clients)} {audience.Name}";
            UserGrowth.Title.text = $"<b>Growing by</b>\n+{Format.Minify(growing)} weekly";

            var incomePerUser = 0.42f;
            var worth = audience.Size * incomePerUser;
            Potential.Title.text = $"<b>Potential: {Format.Minify(audience.Size)} users</b>\nworth {Format.MinifyMoney(worth)}";

            FavouriteFeatures.Title.text = $"<b>Favourite features</b>\nChats, emojis, videocalls, audiocalls";
            HatedFeatures.Title.text = $"<b>Favourite features</b>\nMonetisation";
        }
    }
}
