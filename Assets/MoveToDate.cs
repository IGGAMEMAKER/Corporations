using Assets.Utils;

public class MoveToDate : ButtonController
{
    int date;
    public override void Execute()
    {
        if (date > CurrentIntDate)
            ScheduleUtils.ResumeGame(GameContext, date);
    }

    public void SetEntity(int currentDate)
    {
        date = currentDate;
    }
}
