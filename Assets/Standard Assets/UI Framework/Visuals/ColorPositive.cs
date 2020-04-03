using Assets.Core;
using UnityEngine;
using UnityEngine.UI;

public class ColorPositive : MonoBehaviour
{
    void Start()
    {
        GetComponent<Text>().color = Visuals.GetColorFromString(Colors.COLOR_POSITIVE);
    }
}
