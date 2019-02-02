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
    public Text Loyalty;
    public Text Share;
    public Image Icon;

    void Start()
    {
        Name.text = "You";
        Motivation.text = "TOP1 Corporation";
        Goal.text = "+10 positions in Forbes";
        Loyalty.text = "100%";
        Share.text = "100%";
    }
}
