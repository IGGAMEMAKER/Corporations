using Assets.Utils;
using UnityEngine;
using UnityEngine.UI;

public class ColorNegative : MonoBehaviour
{
    void Start()
    {
        GetComponent<Text>().color = Visuals.Color(VisualConstants.COLOR_NEGATIVE);
    }
}
