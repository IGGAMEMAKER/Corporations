public abstract class DailyUpdateableView : View
    , IAnyDateListener
{
    void Start()
    {
        ListenDateChanges(this);
    }

    public abstract void Render();

    void IAnyDateListener.OnAnyDate(GameEntity entity, int date)
    {
        Render();
    }
}
