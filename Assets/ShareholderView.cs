using UnityEngine;
using UnityEngine.UI;

public enum MotivationType
{
    Cost,
    Dividends,
    Capture,
    FriendlyCapture,
}

public class Shareholder
{
    public string Name;
    public MotivationType Motivation;
}

public class ShareholderView : MonoBehaviour
{
    public Text Name;
    public Text Motivation;
    public Text Goal;
    public ColoredValuePositiveOrNegative Loyalty;
    public ColoredValuePositiveOrNegative LoyaltyChange;
    public Text Share;
    public Image Icon;

    void Start()
    {
        Name.text = "Venture investor";
        Goal.text = "Company Cost 50.000.000$";
        Motivation.text = "20% growth";
        Share.text = "100%";

        LoyaltyChange.UpdateValue(Random.Range(-10f, 10f));
        Loyalty.UpdateValue(Random.Range(0, 80f));
    }
}
