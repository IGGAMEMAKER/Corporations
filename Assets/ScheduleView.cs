public class ScheduleView : View
{
    public ResourceView ScheduleResourceView;

    private void Update()
    {
        Render();
    }

    public void Render()
    {
        ScheduleResourceView.UpdateResourceValue("", CurrentIntDate);
    }
}
