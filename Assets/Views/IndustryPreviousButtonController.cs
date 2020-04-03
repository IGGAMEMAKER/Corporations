public class IndustryPreviousButtonController : IndustryNavigator
{
    public override int Tweak(int index)
    {
        return index - 1;
    }
}
