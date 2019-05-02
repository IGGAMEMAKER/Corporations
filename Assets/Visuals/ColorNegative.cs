using UnityEngine;
using UnityEngine.UI;

public class ColorNegative : MonoBehaviour
{
    void Start()
    {
        GetComponent<Text>().color = VisualFormattingUtils.Color(VisualConstants.COLOR_NEGATIVE);
    }
}
