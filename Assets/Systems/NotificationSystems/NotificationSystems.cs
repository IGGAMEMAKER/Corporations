public class NotificationSystems : Feature
{
    public NotificationSystems(Contexts contexts) : base("Notification Systems")
    {
        Add(new NotificationInitializerSystem(contexts));

        Add(new NotificationCheckNewCompaniesSystem(contexts));

        //Add(new NotificationCheckNicheTrendsSystem(contexts));
    }
}
