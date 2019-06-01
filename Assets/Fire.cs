using Assets.Utils;

public class Fire : ButtonController
{
    public override void Execute()
    {
        TeamUtils.FireWorker(MyProductEntity, SelectedHuman);
    }
}
