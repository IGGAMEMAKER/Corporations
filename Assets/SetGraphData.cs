using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SetGraphData : MonoBehaviour
{
    public GameObject DotPrefab;
    public GameObject DotContainer;

    List<GameObject> Dots = new List<GameObject>();

    List<int> XList;
    List<long> Values;


    public void SetData(List<int> xs, List<long> ys)
    {
        Debug.Log("Set data to graph " + xs.Count + " " + ys.Count);

        XList = xs;
        Values = ys;

        if (XList.Count == 0 || Values.Count != XList.Count)
            return;


        while (XList.Count > Dots.Count)
        {
            Dots.Add(Instantiate(DotPrefab, DotContainer.transform));
        }

        var max = ys.Max();

        var baseX = 0;
        var baseY = 0;

        var graphWidth = 550;
        var graphHeight = 300;

        var len = XList.Count;
        for (var i = 0; i < len; i++)
        {
            Dots[i].transform.localPosition = new Vector3(baseX + i * graphWidth / len, baseY + Values[i] * graphHeight / max);
        }
    }
}
