public class PlayerControlView : ParameterView
{
    public override string RenderValue()
    {
        var shares = 85f;

        Colorize((int)shares, 0, 100);

        return shares.ToString("0.0") + "%";
    }
}
