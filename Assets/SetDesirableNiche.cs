public class SetDesirableNiche : View
{
    private void OnEnable()
    {
        GetComponent<LinkToNiche>().SetNiche(MyProductEntity.product.Niche);
    }
}
