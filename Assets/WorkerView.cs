public class WorkerView : View
{
    int humanId;

    public HumanPreview HumanPreview;

    public void SetEntity(int humanId, WorkerRole workerRole)
    {
        this.humanId = humanId;

        var link = GetComponent<LinkToHuman>();
        if (humanId != -1)
        {
            link.SetHumanId(humanId);
            link.enabled = true;

            HumanPreview.SetEntity(humanId);
        }
        else
        {
            link.enabled = false;
        }
    }
}
