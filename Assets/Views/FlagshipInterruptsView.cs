﻿using Assets;
using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class FlagshipInterruptsView : View
{
    public Image NeedsServersImage;
    public Image NeedsSupportImage;
    public Image NeedsManagersImage;
    public Image DDOSImage;

    //
    public Image HasDisloyalManagersImage;
    public Image AcquisitionOffer;

    int previousCounter = 0;
    int problemCounter = 0;

    public override void ViewRender()
    {
        base.ViewRender();


        var product = Flagship;

        bool needsMoreServers = Products.IsNeedsMoreServers(product);
        bool needsMoreSupport = Products.IsNeedsMoreMarketingSupport(product);
        bool needsMoreManagers = product.team.Managers.Count < Teams.GetRolesTheoreticallyPossibleForThisCompanyType(product).Count;
        bool underAttack = false;

        // 
        bool workerDisloyal = false;
        bool hasAcquisitionOffers = Companies.GetAcquisitionOffersToPlayer(Q).Count() > 0;

        problemCounter = 0;

        SpecialDraw(NeedsManagersImage, false);
        SpecialDraw(NeedsServersImage, true);
        SpecialDraw(NeedsSupportImage, false);
        SpecialDraw(DDOSImage, underAttack);

        NeedsSupportImage.color = Visuals.GetColorFromString(needsMoreSupport ? Colors.COLOR_NEGATIVE : Colors.COLOR_NEUTRAL);
        NeedsSupportImage.GetComponent<Blinker>().enabled = needsMoreSupport;

        NeedsServersImage.color = Visuals.GetColorFromString(needsMoreServers ? Colors.COLOR_NEGATIVE : Colors.COLOR_NEUTRAL);
        NeedsServersImage.GetComponent<Blinker>().enabled = needsMoreServers;

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
