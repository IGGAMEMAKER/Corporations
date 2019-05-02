using UnityEngine;
using UnityEngine.UI;

public class ColorPositive : MonoBehaviour
{
    void Start()
    {
        GetComponent<Text>().color = VisualFormattingUtils.Color(VisualConstants.COLOR_POSITIVE);
    }
}
