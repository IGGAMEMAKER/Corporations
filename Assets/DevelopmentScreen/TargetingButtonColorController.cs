public class TargetingButtonColorController : View
    , ITargetingListener
{
    void Start()
    {
        MyProductEntity.AddTargetingListener(this);
    }

    void ITargetingListener.OnTargeting(GameEntity entity)
    {
        GetComponent<IsChosenComponent>().enabled = MyProductEntity.isTargeting;
    }
}
