using Assets.Utils;
using UnityEngine;
using UnityEngine.UI;

public class ColorNegative : MonoBehaviour
{
    void Start()
    {
        GetComponent<Text>().color = Visuals.GetColorFromString(VisualConstants.COLOR_NEGATIVE);
    }
}
