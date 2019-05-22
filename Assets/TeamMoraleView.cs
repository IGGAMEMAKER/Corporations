public class TeamMoraleView : View
{
    public override void ViewRender()
    {
        base.ViewRender();

        var morale = MyProductEntity.team.Morale;

        GetComponent<ColoredValueGradient>().UpdateValue(morale);
        //GetComponent<Hint>().SetHint("Morale ")
    }
}
