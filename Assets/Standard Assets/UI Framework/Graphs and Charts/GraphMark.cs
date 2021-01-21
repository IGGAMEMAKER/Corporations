using Assets.Core;
using UnityEngine;
using UnityEngine.UI;

public class GraphMark : MonoBehaviour
{
    public Text Text;

    void UpdatePosition(long value, long min, long max, long graphHeight)
    {
        var pos = transform.localPosition;

        var percent = (value - min) / (float)(max - min);

        transform.localPosition = new Vector3(pos.x, percent * graphHeight, 0);
    }

    public void Render(long value, long min, long max, int graphHeight)
    {
        Text.text = Format.Minify(value);

        UpdatePosition(value, min, max, graphHeight);
    }
}
