using Assets.Utils;
using UnityEngine;
using UnityEngine.UI;

public class ColorPositive : MonoBehaviour
{
    void Start()
    {
        GetComponent<Text>().color = VisualUtils.Color(VisualConstants.COLOR_POSITIVE);
    }
}
