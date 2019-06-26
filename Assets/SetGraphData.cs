using Assets.Utils;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class SetGraphData : MonoBehaviour
{
    public GameObject DotPrefab;
    public GameObject DotContainer;

    List<GameObject> Dots = new List<GameObject>();

    List<int> XList;
    List<long> Values;

    public int graphWidth = 550;
    public int graphHeight = 200;

    public int baseY = 0;
    public int baseX = 25;

    public void SetData(List<int> xs, List<long> ys)
    {
        XList = xs;
        Values = ys;

        if (XList.Count == 0 || Values.Count != XList.Count)
            return;


        while (XList.Count > Dots.Count)
        {
            Dots.Add(Instantiate(DotPrefab, DotContainer.transform));
        }

        var max = ys.Max();

        var len = XList.Count;
        for (var i = 0; i < len; i++)
        {
            Dots[i].transform.localPosition = new Vector3(baseX + i * graphWidth / len, baseY + Values[i] * graphHeight / max);
            Dots[i].GetComponentInChildren<Text>().text = Format.MinifyToInteger(Values[i]);
        }
    }
}
