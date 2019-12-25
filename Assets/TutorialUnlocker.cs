using Assets.Utils;

public class TutorialUnlocker : ButtonController
{
    string Text;
    public override void Execute()
    {
        TutorialUtils.Unlock(GameContext, Text);
    }

    public void SetEvent(string text)
    {
        Text = text;
    }
}
