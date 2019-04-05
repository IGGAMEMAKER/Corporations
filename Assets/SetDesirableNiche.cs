public class SetDesirableNiche : View
{
    private void OnEnable()
    {
        GetComponent<LinkToNiche>().SetNiche(myProductEntity.product.Niche);
    }
}
