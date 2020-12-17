using System.Linq;

public class TeamSpeedView : UpgradedParameterView
{
    public override string RenderHint() => "";

    public override string RenderValue()
    {
        var bonus = new Bonus<long>("Team Speed");

        var averageSpeed = Flagship.team.Teams.Average(t => t.Organisation);



        var tasks = Flagship.team.Teams[0].Tasks.Count;
        var capacity = 4;

        var speed = tasks * averageSpeed / capacity;

        return speed.ToString("0%");
    }
}
