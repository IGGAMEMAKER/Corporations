public class TargetingButtonColorController : View
{
    void Update()
    {
        if (myProductEntity.isTargeting)
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
