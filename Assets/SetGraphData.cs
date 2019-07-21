using Assets.Utils;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;


public class GraphData
{
    public List<long> values;
    public Color Color;
    public string Name;
}

public class SetGraphData : MonoBehaviour
{
    public GameObject DotPrefab;
    public GameObject DotContainer;
    public GameObject NotEnoughDataBanner;

    List<GameObject> Dots = new List<GameObject>();

    //List<int> XList;
    //List<long> Values;

    public int graphWidth = 550;
    public int graphHeight = 200;

    public int baseY = 0;
    public int baseX = 25;

    bool isEnoughData(List<int> xs, List<long> values)
    {
        //return XList.Count == 0 || Values.Count != XList.Count;
        return xs.Count > 0 && values.Count > 0;
    }

    GameObject GetDot(int i)
    {
        if (Dots.Count <= i)
            Dots.Add(Instantiate(DotPrefab, DotContainer.transform));

        return Dots[i];
    }

    int counter;
    void RenderGraphs(List<int> XList, GraphData graphData)
    {
        List<long> Values = graphData.values;
        
        RenderNoDataBanner(XList, Values);

        if (!isEnoughData(XList, Values))
            return;

        var len = XList.Count;

        var max = Values.Max();
        var min = Values.Min();

        long value = 0;

        var i = 0;
        for (i = 0; i < len; i++)
        {
            value = Values[i];

            RenderDot(value, len, max, min, i);

            counter++;
        }

        HideUselessDots(i);
    }

    void RenderDot(long value, int len, long max, long min, int i)
    {
        var dot = GetDot(counter);

        dot.transform.localPosition = new Vector3(baseX + i * graphWidth / len, baseY + value * graphHeight / max);
        dot.SetActive(true);

        var txt = dot.GetComponentInChildren<Text>();

        if (len <= 12 || value == max || value == min)
        {
            txt.text = Format.MinifyToInteger(value);
        }
        else
        {
            txt.text = "";
        }
    }

    void RenderNoDataBanner(List<int> xs, List<long> ys)
    {
        if (!isEnoughData(xs, ys))
        {
            NotEnoughDataBanner.SetActive(true);
            DotContainer.SetActive(false);
        }
        else
        {
            if (!DotContainer.activeSelf)
                DotContainer.SetActive(true);
            NotEnoughDataBanner.SetActive(false);
        }
    }

    void HideUselessDots(int i)
    {
        for (; i < Dots.Count; i++)
            Dots[i].SetActive(false);
    }

    public void SetData(List<int> xs, GraphData[] ys)
    {
        counter = 0;

        for (var i = 0; i < ys.Length; i++)
            RenderGraphs(xs, ys[i]);
    }

    public void SetData(List<int> xs, GraphData ys)
    {
        //XList = xs;
        //Values = ys;

        counter = 0;
        RenderGraphs(xs, ys);
    }
}
