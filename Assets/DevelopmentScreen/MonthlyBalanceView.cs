using Assets.Utils;
using UnityEngine.UI;

public class MonthlyBalanceView : View
{
    ColoredValuePositiveOrNegative Text;
    // Start is called before the first frame update
    void Start()
    {
        Text = GetComponent<ColoredValuePositiveOrNegative>();
    }

    // Update is called once per frame
    void Update()
    {
        Text.value = ProductEconomicsUtils.GetBalance(myProductEntity);
    }
}
