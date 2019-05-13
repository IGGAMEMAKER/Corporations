public class ScheduleView : View
    , IAnyDateListener
{
    public ResourceView ScheduleResourceView;

    private void OnEnable()
    {
        ListenDateChanges(this);

        Render();
    }

    public void Render()
    {
        ScheduleResourceView.UpdateResourceValue("Date", CurrentIntDate);
    }

    void IAnyDateListener.OnAnyDate(GameEntity entity, int date)
    {
        Render();
    }
}
