public class LinkToHuman : ButtonController
{
    int humanId;

    public override void Execute()
    {
        NavigateToHuman(humanId);
    }

    public void SetHumanId(int id)
    {
        humanId = id;
    }
}
