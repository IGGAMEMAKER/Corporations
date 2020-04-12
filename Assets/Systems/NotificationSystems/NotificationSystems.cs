public class NotificationSystems : Feature
{
    public NotificationSystems(Contexts contexts) : base("Notification Systems")
    {

        Add(new NotificationCheckNewCompaniesSystem(contexts));

        //Add(new NotificationCheckNicheTrendsSystem(contexts));
        Add(new WarnAboutBankruptcySystem(contexts));
    }
}
