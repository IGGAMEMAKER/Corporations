public class WorkerView : View
{
    int humanId;

    public void SetEntity(int humanId)
    {
        this.humanId = humanId;

        GetComponent<LinkToHuman>().SetHumanId(humanId);
    }
}
