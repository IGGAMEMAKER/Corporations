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
    public GameObject NotEnoughDataBanner;

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
        {
            NotEnoughDataBanner.SetActive(true);
            return;
        } else
        {
            NotEnoughDataBanner.SetActive(false);
        }


        while (XList.Count > Dots.Count)
        {
            Dots.Add(Instantiate(DotPrefab, DotContainer.transform));
        }

        var len = XList.Count;

        var max = Values.Max();
        var min = Values.Min();

        long value = 0;

        for (var i = 0; i < len; i++)
        {
            value = Values[i];
            Dots[i].transform.localPosition = new Vector3(baseX + i * graphWidth / len, baseY + value * graphHeight / max);

            var txt = Dots[i].GetComponentInChildren<Text>();

            if (len < 10 || value == max || value == min)
            {
                txt.text = Format.MinifyToInteger(value);
            }
            else
            {
                txt.text = "";
            }
        }
    }

    bool IsSquaredValue (long value)
    {
        var sqrt = (int)Mathf.Sqrt(value);

        return sqrt * sqrt == value;
    }
}
