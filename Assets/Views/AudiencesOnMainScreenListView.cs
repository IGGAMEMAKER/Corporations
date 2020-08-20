using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class AudiencesOnMainScreenListView : ListView
{
    [Header("Buttons")]
    public GameObject SetAsTargetAudience;

    public ProductUpgradeLinks MainAudienceInfo;
    public ProductUpgradeLinks AmountOfUsers;
    public ProductUpgradeLinks Potential;
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

        var clients = Flagship.marketing.ClientList;

        bool hasNoUsers = Marketing.GetClients(Flagship) == 0;

        if (hasNoUsers)
        {
            // show test audience only
            SetItems(audiences.Take(1));
        }
        else
        {
            bool hasEnoughTestUsers = clients[0] > 1000;

            if (!hasEnoughTestUsers)
            {
                SetItems(audiences.Take(1));
            }
            else
            {
                SetItems(audiences);
            }
        }
    }

    public override void OnDeselect()
    {
        base.OnDeselect();

        HideButtons();
        FindObjectOfType<MainPanelRelay>().ShowAudiencesAndInvestors();
    }

    public void HideButtons()
    {
        Hide(ButtonList);
    }

    public override void OnItemSelected(int ind)
    {
        base.OnItemSelected(ind);

        FindObjectOfType<MainPanelRelay>().ExpandAudiences();

        Show(ButtonList);

        bool isTargetAudience   = Flagship.productTargetAudience.SegmentId == ind;
        bool hasClients         = Flagship.marketing.ClientList.ContainsKey(ind);
        var clients             = hasClients ? Flagship.marketing.ClientList[ind] : 0;

        Draw(SetAsTargetAudience, !isTargetAudience);
        Draw(MainAudienceInfo, isTargetAudience);
        Draw(AmountOfUsers, clients > 0);

        RenderAudienceData(ind, clients);
    }

    void RenderAudienceData(int ind, long clients)
    {
        var audience = Marketing.GetAudienceInfos()[ind];

        var growing = Marketing.GetAudienceGrowthBySegment(Flagship, Q, ind);

        var incomePerUser = 0.42f;
        var worth = (long)((double)audience.Size * incomePerUser);

        var income = Economy.GetIncomePerSegment(Q, Flagship, ind);

        var potentialPhrase = Format.Minify(audience.Size);
        var marketWorth = Format.MinifyMoney(worth);

        var growthPhrase = $"+{Format.Minify(growing)} weekly";


        MainInfo.Title.text          = $"<b>{audience.Name}</b>\nIncome: {Visuals.Positive(Format.MinifyMoney(income))}";
        AmountOfUsers.Title.text     = $"{Format.Minify(clients)} {audience.Name}\n" + Visuals.Colorize(growthPhrase, growing >= 0);

        MainAudienceInfo.Title.text  = "<b>Our main audience</b>";
        MainAudienceInfo.Title.color = Visuals.GetColorFromString(Colors.COLOR_GOLD);

        Potential.Title.text         = $"<b>Potential: {potentialPhrase} users</b>\nworth {marketWorth}";
    }
}
