using Assets;
using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class FlagshipInterruptsView : View
{
    public Image NeedsServersImage;
    public Text ServerLoad;

    public Image NeedsSupportImage;
    public Image NeedsManagersImage;

    public Image DDOSImage;
    public ProgressBar DDOSProgress;

    //
    public Image HasDisloyalManagersImage;
    public Image AcquisitionOffer;

    public Image AudienceMapLink;
    public Text MarketShare;

    int previousCounter = 0;
    int problemCounter = 0;

    public void FightServerAttack()
    {
        Flagship.serverAttack.CurrentResistance -= 2;

        if (Flagship.serverAttack.CurrentResistance <= 0)
        {
            Flagship.RemoveServerAttack();
        }

        ViewRender();
    }

    public override void ViewRender()
    {
        base.ViewRender();

        var load = Products.GetServerLoad(Flagship) * 100;
        var cap = Products.GetServerCapacity(Flagship);

        var product = Flagship;

        if (CurrentIntDate > 0 && CurrentIntDate % 91 == 0 && Random.Range(0, 2) < 1 && !product.hasServerAttack)
        {
            //// ddos stops when you have normal load
            //// or resistance (progressbar) < 0

            ////var load = Products.GetServerLoad(Flagship);
            //var resistance = Random.Range(10, 25);
            //var strength = Random.Range(2, 20);

            //product.AddServerAttack(cap * strength, resistance, resistance); // load x2... x20 (in pupils), resistance how easy is it to kill attack (clicks per second 1...5),
        }


        bool needsMoreServers = Products.IsNeedsMoreServers(product);
        bool needsMoreSupport = Products.IsNeedsMoreMarketingSupport(product);
        bool needsMoreManagers = product.team.Managers.Count < Teams.GetRolesTheoreticallyPossibleForThisCompanyType(product).Count;
        bool underAttack = product.hasServerAttack;

        if (underAttack)
        {
            product.serverAttack.CurrentResistance = Mathf.Clamp(product.serverAttack.CurrentResistance + 1, 1, product.serverAttack.Resistance);

            var res = product.serverAttack.CurrentResistance;
            var max = product.serverAttack.Resistance;

            DDOSProgress.SetValue(max - res, max);
        }
        // 
        bool workerDisloyal = false;
        bool hasAcquisitionOffers = Companies.GetAcquisitionOffersToPlayer(Q).Count() > 0;

        problemCounter = 0;

        SpecialDraw(AudienceMapLink, product.isRelease);
        SpecialDraw(NeedsManagersImage, false);
        SpecialDraw(NeedsServersImage, true);
        SpecialDraw(NeedsSupportImage, false);
        SpecialDraw(DDOSImage, underAttack);

        NeedsSupportImage.color = Visuals.GetColorFromString(needsMoreSupport ? Colors.COLOR_NEGATIVE : Colors.COLOR_NEUTRAL);
        NeedsSupportImage.GetComponent<Blinker>().enabled = needsMoreSupport;

        NeedsServersImage.color = Visuals.GetColorFromString(needsMoreServers ? Colors.COLOR_NEGATIVE : Colors.COLOR_NEUTRAL);
        NeedsServersImage.GetComponent<Blinker>().enabled = needsMoreServers;

        MarketShare.text = (Companies.GetMarketShareOfCompanyMultipliedByHundred(Flagship, Q) * 1f).ToString("0.0") + "%";




        var perc = cap != 0 ? load / cap : 100;
        ServerLoad.text = perc + "%";
        ServerLoad.color = Visuals.GetGradientColor(0, 100, perc, true);

        SpecialDraw(HasDisloyalManagersImage, workerDisloyal);

        SpecialDraw(AcquisitionOffer, hasAcquisitionOffers);

        if (problemCounter > previousCounter)
        {
            // play interrupt sound
            SoundManager.Play(Sound.Notification);
            //gameObject.AddComponent<EnlargeOnAppearance>().
        }

        previousCounter = problemCounter;
    }

    void SpecialDraw(Image obj, bool draw)
    {
        Draw(obj, draw);

        if (draw)
            problemCounter++;
    }
}
