
public class OpenMyModalWindow : ButtonController
{
    public string ModalTag;

    public override void Execute()
    {
        OpenModal(ModalTag);
    }
}
