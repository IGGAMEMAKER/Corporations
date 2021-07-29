using Assets.Core;

public class RenderFlagshipUsers : UpgradedParameterView2<long>
{
    public override long GetValue()
    {
        return Marketing.GetUsers(Flagship);
    }

    public override string RenderHint()
    {
        return "";
    }

    public override string RenderValue()
    {
        var clients = Marketing.GetUsers(Flagship);

        return Format.Minify(clients); // + " users";
    }
}
