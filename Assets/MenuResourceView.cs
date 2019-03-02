using Assets.Classes;
using System;
using UnityEngine;

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
        ScheduleView.UpdateResourceValue("Space: Pause/Unpause\n\n +/-: Faster/Slower", CurrentIntDate, "Day");
    }

    public void Render(TeamResource teamResource, TeamResource resourceMonthChanges, Audience audience, string currentDate)
    {
        Debug.Log("MenuResourceView RENDER RESOURCES");
        string hint;

        // resources
        hint = GetHintText(resourceMonthChanges.money);
        MoneyView.UpdateResourceValue("Money", teamResource.money, hint);

        hint = GetHintText(resourceMonthChanges.programmingPoints);
        ProgrammingView.UpdateResourceValue("Programming Points", teamResource.programmingPoints, hint);

        hint = GetHintText(resourceMonthChanges.managerPoints);
        ManagerView.UpdateResourceValue("Manager Points", teamResource.managerPoints, hint);

        hint = GetHintText(resourceMonthChanges.salesPoints);
        MarketingView.UpdateResourceValue("Marketing points", teamResource.salesPoints, hint);

        hint = GetHintText(resourceMonthChanges.ideaPoints);
        IdeaView.UpdateResourceValue("Ideas", teamResource.ideaPoints, hint);


        // audience
        hint = String.Format(
            "We will lose {0} clients this month due to:\n\n Churn rate: {1}%",
            audience.GetChurnClients(),
            (int) (audience.GetChurnRate() * 100)
        );

        ClientView.UpdateResourceValue("Clients", audience.clients, hint);

        // date
        ScheduleView.UpdateResourceValue("Space: Pause/Unpause\n\n +/-: Faster/Slower", currentDate, "Day");
    }
}
