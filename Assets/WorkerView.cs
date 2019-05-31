public class WorkerView : View
{
    int humanId;

    public HumanPreview HumanPreview;

    public void SetEntity(int humanId, WorkerRole workerRole)
    {
        this.humanId = humanId;

        GetComponent<LinkToHuman>().SetHumanId(humanId);

        HumanPreview.SetEntity(humanId);
    }
}
