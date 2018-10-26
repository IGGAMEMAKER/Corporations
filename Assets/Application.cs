using Assets;
using Assets.Classes;

public class Application
{
    public World world;
    public ViewManager ViewManager;
    AudioManager audioManager;


    public int projectId = 0;



    public Application(World world, ViewManager ViewManager, AudioManager audioManager)
    {
        this.world = world;
        this.ViewManager = ViewManager;
        this.audioManager = audioManager;
    }

    public void ExploreFeature(int projectId, int featureId)
    {
        world.ExploreFeature(projectId, featureId);
        RedrawFeatures();
    }

    public bool PeriodTick(int count)
    {
        bool isMonthTick = world.PeriodTick(count);

        if (isMonthTick)
            audioManager.PlayCoinSound();

        return isMonthTick;
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

    public void UpgradeFeature(int projectId, int featureId)
    {
        world.UpgradeFeature(projectId, featureId);
        RedrawFeatures();
    }


    // rendering
    public void RedrawResources()
    {
        TeamResource teamResource = world.GetProjectById(projectId).resources;

        Audience audience = world.GetProjectById(projectId).audience;

        string formattedDate = world.GetFormattedDate();

        ViewManager.RedrawResources(teamResource, audience, formattedDate);
    }

    public void RedrawAds()
    {
        Project p = world.GetProjectById(projectId);
        ViewManager.RedrawAds(p.GetAds());
    }

    public void RedrawFeatures()
    {
        Project p = world.GetProjectById(projectId);
        ViewManager.RedrawFeatures(p.Features);
    }
}
