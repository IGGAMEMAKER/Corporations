using Assets.Utils;
using UnityEngine.UI;

public class DevelopmentCostView : View
{
    Text Text;
    Hint Hint;

    // Start is called before the first frame update
    void Start()
    {
        Text = GetComponent<Text>();
        Hint = GetComponent<Hint>();
    }

    // Update is called once per frame
    void Update()
    {
        Text.text = ProductDevelopmentUtils.GetDevelopmentCost(myProductEntity).programmingPoints + "";

        string hint = "Base Cost: " + ProductDevelopmentUtils.BaseDevCost(myProductEntity);

        if (ProductDevelopmentUtils.IsInnovating(myProductEntity))
            hint += "\n Is Innovating: +" + Constants.DEVELOPMENT_INNOVATION_PENALTY + "%";

        Hint.SetHint(hint);
    }
}
