using UnityEngine;

[ExecuteAlways]
public class RandomMapFiller : View
{

    [Range(0, 1500)]
    public int width;
    [Range(0, 700)]
    public int height;

    [Header("Default item size")]
    [Range(0, 1000)]
    public int averageSize;

    void OnTransformChildrenChanged()
    {
        Render2();
    }

    // Update is called once per frame
    void Update()
    {
        Render2();
    }

    void Render2()
    {
        var index = 0;
        var count = 0;

        foreach (Transform child in transform)
            count++;

        foreach (Transform child in transform)
        {
            var scale = child.localScale;

            var line = (int)Mathf.Pow(count, 0.5f);

            child.localPosition = new Vector3(count % (index + 1), line) * averageSize;

            index++;
        }
    }
}
