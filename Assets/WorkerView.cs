public class WorkerView : View
{
    int humanId;

    public HumanPreview HumanPreview;

    public void SetEntity(int humanId)
    {
        this.humanId = humanId;

        GetComponent<LinkToHuman>().SetHumanId(humanId);

        HumanPreview.SetEntity(humanId);
    }
}
