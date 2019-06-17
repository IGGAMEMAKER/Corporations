using Assets.Utils;

public class HireWorker : ButtonController
{
    public override void Execute()
    {
        HumanUtils.Recruit(SelectedHuman, MyProductEntity);
    }
}
