
public class CloseMyModalWindow : ButtonController
{
    public string ModalTag;

    public override void Execute()
    {
        CloseModal(ModalTag);
    }
}
