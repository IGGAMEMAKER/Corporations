using Assets.Core;

public class ShowWorkerUpgrades : View
{
    GameEntity worker;

    RenderCompanyWorkerListView WorkerListView;
    FlagshipRelayInCompanyView flagshipRelay;

    public void SetWorker(GameEntity worker)
    {
        this.worker = worker;

        WorkerListView = FindObjectOfType<RenderCompanyWorkerListView>();
        flagshipRelay = FindObjectOfType<FlagshipRelayInCompanyView>();

        BlinkIfNecessary();
    }

    public void ToggleState()
    {
        ////WorkerListView.ToggleRole(worker.worker.WorkerRole);

        //flagshipRelay.ToggleRole(worker.worker.WorkerRole);
        //WorkerListView.HighlightManagers();
    }

    public override void ViewRender()
    {
        base.ViewRender();

        if (worker != null)
            BlinkIfNecessary();
    }

    void BlinkIfNecessary()
    {
        var isNecessary = false;

        switch (worker.worker.WorkerRole)
        {
            case WorkerRole.CEO:
                isNecessary = HasNewCEOButtons();

                break;
            case WorkerRole.MarketingLead:
                isNecessary = HasNewMarketingLeadButtons();

                break;
            case WorkerRole.TeamLead:
                isNecessary = HasNewTeamLeadButtons();

                break;
            case WorkerRole.ProjectManager:
                isNecessary = HasNewProjectManagerButtons();

                break;
            case WorkerRole.ProductManager:
                isNecessary = HasNewProductManagerButtons();

                break;

            default:
                isNecessary = false;

                break;
        }

        GetComponent<Blinker>().enabled = isNecessary && true; // flagshipRelay.IsRoleChosen(worker.worker.WorkerRole);
    }

    bool HasNewCEOButtons()
    {
        return false;
    }

    bool HasNewMarketingLeadButtons()
    {
        bool hasNewChannels = NotificationUtils.HasGameEvent(Q, GameEventType.NewMarketingChannel);

        //var activeChannels = Flagship.companyMarketingActivities.Channels.Count == Markets.GetAvailableMarketingChannels(Q, Flagship).Length;

        return hasNewChannels;
    }

    bool HasNewTeamLeadButtons()
    {
        return false;
    }

    bool HasNewProjectManagerButtons()
    {
        return false;
    }

    bool HasNewProductManagerButtons()
    {
        return false;
    }
}
