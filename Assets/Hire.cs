using Assets.Utils;

public class Hire : ButtonController
{
    public WorkerRole worker;

    public override void Execute()
    {
        TeamUtils.HireWorker(MyProductEntity, worker);
    }
}
