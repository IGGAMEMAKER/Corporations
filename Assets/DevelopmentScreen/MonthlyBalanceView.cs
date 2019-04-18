using Assets.Utils;

public class MonthlyBalanceView : View
{
    ColoredValuePositiveOrNegative Text;

    void Start()
    {
        Text = GetComponent<ColoredValuePositiveOrNegative>();
    }

    // Update is called once per frame
    void Update()
    {
        Text.value = CompanyEconomyUtils.GetBalanceChange(MyProductEntity, GameContext);
    }
}
