using Assets.Core;
using UnityEngine;
using UnityEngine.UI;

public class ColorPositive : MonoBehaviour
{
    void Start()
    {
        GetComponent<Text>().color = Visuals.GetColorFromString(VisualConstants.COLOR_POSITIVE);
    }
}
