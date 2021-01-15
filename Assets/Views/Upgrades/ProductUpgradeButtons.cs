using Assets.Core;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProductUpgradeButtons : RoleRelatedButtons
{
    public ReleaseApp ReleaseApp;
    public GameObject ChangePositioning;

    void Render()
    {
        var company = Flagship;
        var id = company.company.Id;
        
        ReleaseApp.SetCompanyId(id);

        Draw(ReleaseApp, Companies.IsReleaseableApp(company));
        Draw(ChangePositioning, false);
    }

    void RenderInvestmentsButton()
    {
        bool hasReleasedProducts = Companies.IsHasReleasedProducts(Q, MyCompany);

        bool bankruptcyLooming = TutorialUtils.IsOpenedFunctionality(Q, TutorialFunctionality.BankruptcyWarning);
    }

    public override void ViewRender()
    {
        base.ViewRender();

        Render();
    }
}

// Project manager
class CustomImage
{
    Image Image;

    // or

    string ImageUrl;
}

class CustomVideo
{
    string Url; // both web and desktop
}

class InterestInfo
{
    CustomImage Logo;
    CustomImage MainScreen;
    string Sentence;
    List<CustomImage> Screenshots;
    string Description; // abz
    string FullDescription;

    CustomVideo Trailer;
    CustomVideo LetsPlay;
}

class RawTextData
{
    public List<string> Texts;
}

class ProjectInfo
{
    AudienceInfo AudienceInfo;
    InterestInfo InterestInfo;
    string WhatAreYouDoing;

    string WhyWillItWork;
    List<string> ClientSources;

    List<string> DefaultQuestions = new List<string> {
        "Holding Attention", "Getting Attention",
        "Why wanna play", "Why wanna buy", "Why wanna feedback", "Feedback type",

        "Why will they like it", "Who will be a superfan", "How to work w them",
        "What's the challenge", "What's the game essence"
    };

    // default state
    // empty pages
    // 0 fans
    List<RawTextData> Infos;
}
