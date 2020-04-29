public class WorkerView : View
{
    public HumanPreview HumanPreview;

    public void SetEntity(int humanId, WorkerRole workerRole)
    {
        var link = GetComponent<LinkToHuman>();

        link.SetHumanId(humanId);
        //link.enabled = true;

        HumanPreview.SetEntity(humanId);
    }
}
