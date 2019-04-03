public class IndustryNextButtonController : IndustryNavigator
{
    public override int Tweak(int index)
    {
        return index + 1;
    }
}
