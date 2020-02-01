using Assets.Core;

public class MoveToDate : ButtonController
{
    int date;
    public override void Execute()
    {
        if (date > CurrentIntDate)
            ScheduleUtils.ResumeGame(Q, date);
    }

    public void SetEntity(int currentDate)
    {
        date = currentDate;
    }
}
