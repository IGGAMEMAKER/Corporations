using System;
using System.Collections.Generic;
using Assets;
using Assets.Classes;

public class Application
{
    public World world;
    public ViewManager ViewManager;
    AudioManager audioManager;
    Notifier Notifier;

    public int myProjectId = 0;
    public int myHumanId = 0;

    public Application(World world, ViewManager ViewManager, AudioManager audioManager)
    {
        this.world = world;
        this.ViewManager = ViewManager;
        this.audioManager = audioManager;

        Notifier = new Notifier();
    }



    public bool PeriodTick(int count)
    {
        bool isMonthTick = world.PeriodTick(count);

        if (isMonthTick)
        {
            audioManager.PlayCoinSound();
            ViewManager.HighlightMonthTick();
        }

        return isMonthTick;
    }

    internal void ExchangeShare(int sellerId, int buyerId, int share)
    {
        audioManager.PlayToggleScreenSound();
        world.ExchangeShare(sellerId, buyerId, share);
        RedrawCompanies();
    }

    public void PrepareAd(int projectId, int channelId, int duration)
    {
        audioManager.PlayPrepareAdSound();
        world.PrepareAd(projectId, channelId, duration);
        RedrawResources();
        RedrawAds();
    }

    public void StartAdCampaign(int projectId, int channelId)
    {
        audioManager.PlayStartAdSound();
        world.StartAdCampaign(projectId, channelId);
        RedrawResources();
        RedrawAds();
    }

    internal void Notify(string message)
    {
        Notifier.Notify(message);
        audioManager.PlayNotificationSound();
    }

    public void ExploreFeature(int projectId, int featureId)
    {
        audioManager.PlayClickSound();
        world.ExploreFeature(projectId, featureId);
        RedrawFeatures();
    }

    public void Fire(int projectId, int workerId)
    {
        audioManager.PlayWaterSplashSound();
        world.Fire(projectId, workerId);
        RedrawTeam();
    }

    public void Hire(int projectId, int workerId)
    {
        audioManager.PlayWaterSplashSound();
        world.Hire(projectId, workerId);
        RedrawTeam();
    }

    public void UpgradeFeature(int projectId, int featureId)
    {
        audioManager.PlayClickSound();
        world.UpgradeFeature(projectId, featureId);
        RedrawFeatures();
    }


    // rendering
    public void RedrawResources()
    {
        TeamResource teamResource = world.GetProjectById(myProjectId).resources;
        TeamResource resourceMonthChanges = world.GetProjectById(myProjectId).resourceMonthChanges;

        Audience audience = world.GetProjectById(myProjectId).audience;

        string formattedDate = world.GetFormattedDate();

        ViewManager.RedrawResources(teamResource, resourceMonthChanges, audience, formattedDate);
    }

    public void RedrawTeam()
    {
        Project p = world.GetProjectById(myProjectId);
        ViewManager.RedrawTeam(p);
    }

    internal void RedrawCompanies()
    {
        ViewManager.RedrawCompanies(world.projects, myProjectId);
    }

    public void RedrawAds()
    {
        Project p = world.GetProjectById(myProjectId);
        ViewManager.RedrawAds(p.GetAds());
    }

    public void RedrawFeatures()
    {
        Project p = world.GetProjectById(myProjectId);
        ViewManager.RedrawFeatures(p.Features);
    }
}
