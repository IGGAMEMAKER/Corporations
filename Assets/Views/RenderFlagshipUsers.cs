using Assets.Core;

public class RenderFlagshipUsers : ParameterView
{
    public override string RenderValue()
    {
        var clients = Marketing.GetUsers(Flagship);

        return Format.Minify(clients) + " users";
    }
}
