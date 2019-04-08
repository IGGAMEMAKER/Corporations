public class TargetingButtonColorController : View
{
    void Update()
    {
        if (MyProductEntity.isTargeting)
        {
            if (gameObject.GetComponent<IsChosenComponent>() == null)
                gameObject.AddComponent<IsChosenComponent>();
        }
        else
        {
            var c = gameObject.GetComponent<IsChosenComponent>();

            if (c != null)
                Destroy(c);
        }
    }
}
