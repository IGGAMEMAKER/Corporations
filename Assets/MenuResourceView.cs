using Assets.Classes;
using System;

public class MenuResourceView : View {
    public ResourceView MoneyView;
    public ResourceView ProgrammingView;
    public ResourceView MarketingView;
    public ResourceView ManagerView;
    public ResourceView IdeaView;
    public ResourceView ClientView;
    public ResourceView ScheduleView;
	
    string GetHintText<T> (T value)
    {
        string valueSigned = "";

        if (long.Parse(value.ToString()) > 0)
            valueSigned = "+" + value.ToString();
        else
            valueSigned = value.ToString();

        return String.Format("Monthly change \n\n {0}", valueSigned);
    }

    private void Update()
    {
        Render(myProductEntity.product.Resources, new TeamResource(), new Audience(1000, 0), CurrentIntDate);
    }

    public void Render(TeamResource teamResource, TeamResource resourceMonthChanges, Audience audience, int currentDate)
    {
        string hint;

        // resources
        hint = GetHintText(resourceMonthChanges.money);
        MoneyView.UpdateResourceValue(hint, teamResource.money);

        hint = GetHintText(resourceMonthChanges.programmingPoints);
        ProgrammingView.UpdateResourceValue(hint, teamResource.programmingPoints);

        hint = GetHintText(resourceMonthChanges.managerPoints);
        ManagerView.UpdateResourceValue(hint, teamResource.managerPoints);

        hint = GetHintText(resourceMonthChanges.salesPoints);
        MarketingView.UpdateResourceValue(hint, teamResource.salesPoints);

        hint = GetHintText(resourceMonthChanges.ideaPoints);
        IdeaView.UpdateResourceValue(hint, teamResource.ideaPoints);


        // audience
        hint = String.Format(
            "We will lose {0} clients this month due to:\n\n Churn rate: {1}%",
            audience.GetChurnClients(),
            (int) (audience.GetChurnRate() * 100)
        );

        //ClientView.UpdateResourceValue("Clients", audience.clients, hint);

        // date
        ScheduleView.UpdateResourceValue("", currentDate);
        //ScheduleView.UpdateResourceValue("Space: Pause/Unpause\n\n +/-: Faster/Slower", currentDate, "Day");
    }
}
