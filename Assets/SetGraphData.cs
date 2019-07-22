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
    public GameObject MarkPrefab;
    public GameObject DotContainer;
    public GameObject NotEnoughDataBanner;

    List<GameObject> Marks = new List<GameObject>();

    List<GameObject> Dots = new List<GameObject>();

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

    GameObject GetMark(int i)
    {
        if (Marks.Count <= i)
            Marks.Add(Instantiate(MarkPrefab, DotContainer.transform));

        return Marks[i];
    }

    int counter;

    long maxx = 0;
    long minn = 0;
    void RenderGraphs(List<int> XList, GraphData graphData, int amountOfGraphs)
    {
        List<long> Values = graphData.values;
        
        RenderNoDataBanner(XList, Values);

        if (!isEnoughData(XList, Values))
            return;

        var len = XList.Count;

        maxx = System.Math.Max(maxx, Values.Max());
        minn = System.Math.Min(minn, Values.Min());

        long value = 0;

        var i = 0;
        for (i = 0; i < len; i++)
        {
            value = Values[i];

            RenderDot(value, len, i, amountOfGraphs, graphData.Color, graphData.Name);

            counter++;
        }

        HideUselessDots(i);
    }

    Vector3 GetPointPosition(long value)
    {
        return GetPointPosition(0, value, 1);
    }

    Vector3 GetPointPosition(int i, long value, int len)
    {
        return new Vector3(baseX + i * graphWidth / len, baseY + value * graphHeight / maxx);
    }

    void RenderDot(long value, int len, int i, int amountOfGraphs, Color color, string hint)
    {
        var dot = GetDot(counter);

        dot.transform.localPosition = GetPointPosition(i, value, len);
        // new Vector3(baseX + i * graphWidth / len, baseY + value * graphHeight / maxx);
        dot.SetActive(true);


        string txt = "";

        bool isSmallAmountOfData = len <= 12;

        bool isRenderMinMaxOnly = amountOfGraphs > 1 && value == maxx || value == minn;

        bool isRenderAllValues = amountOfGraphs == 1 && isSmallAmountOfData;

        string val = Format.Minify(value);
        if (isRenderAllValues)
            txt = val;

        dot.GetComponent<GraphDot>().Render(color, txt, val, hint);
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
        for (i = counter; i < Dots.Count; i++)
            Dots[i].SetActive(false);
    }

    void ResetCounter()
    {
        counter = 0;

        maxx = 1;
        minn = 0;
    }

    void RenderYAxis()
    {
        for (var i = 0; i < 6; i++)
        {
            var m = GetMark(i);

            var value = minn + (maxx - minn) * i / 5;

            m.GetComponent<GraphMark>().Render(value, minn, maxx, graphHeight);
        }
    }

    public void SetData(List<int> xs, GraphData[] ys)
    {
        ResetCounter();

        for (var i = 0; i < ys.Length; i++)
            RenderGraphs(xs, ys[i], ys.Length);

        RenderYAxis();
    }

    public void SetData(List<int> xs, GraphData ys)
    {
        ResetCounter();

        RenderGraphs(xs, ys, 1);

        RenderYAxis();
    }
}
