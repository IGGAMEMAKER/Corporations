using Assets.Core.Tutorial;

public class TutorialUnlocker : ButtonController
{
    string Text;
    public override void Execute()
    {
        TutorialUtils.Unlock(Q, Text);
    }

    public void SetEvent(string text)
    {
        Text = text;
    }
}
