using UnityEngine;
using UnityEngine.UI;

public class ColorNegative : MonoBehaviour
{
    void Start()
    {
        GetComponent<Text>().color = VisualUtils.Color(VisualConstants.COLOR_NEGATIVE);
    }
}
