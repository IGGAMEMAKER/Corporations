using Assets.Classes;

public class Application
{
    public World world;
    public ViewManager ViewManager;

    public int projectId = 0;

    public Application(World world, ViewManager ViewManager)
    {
        this.world = world;
        this.ViewManager = ViewManager;
    }

    public void ExploreFeature(int projectId, int featureId)
    {
        world.ExploreFeature(projectId, featureId);
    }

    public bool PeriodTick(int count)
    {
        return world.PeriodTick(count);
    }

    public void PrepareAd(int projectId, int channelId, int duration)
    {
        world.PrepareAd(projectId, channelId, duration);
        RedrawResources();
        RedrawAds();
    }

    public void StartAdCampaign(int projectId, int channelId)
    {
        world.StartAdCampaign(projectId, channelId);
        RedrawResources();
        RedrawAds();
    }

    public void UpgradeFeature(int projectId, int featureId)
    {
        world.UpgradeFeature(projectId, featureId);
    }


    // rendering
    public void RedrawResources()
    {
        TeamResource teamResource = world.GetProjectById(projectId).resources;

        Audience audience = world.GetProjectById(projectId).audience;

        ViewManager.RedrawResources(teamResource, audience);
    }

    public void RedrawAds()
    {
        Project p = world.GetProjectById(projectId);
        ViewManager.RedrawAds(p.GetAds());
    }
}
