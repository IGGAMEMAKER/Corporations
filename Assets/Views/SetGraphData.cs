using Assets.Core;
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
    public GameObject ConnectionPrefab;
    public GameObject MarkPrefab;
    public GameObject DotContainer;
    public GameObject NotEnoughDataBanner;

    List<GameObject> Marks = new List<GameObject>();

    List<GameObject> Dots = new List<GameObject>();

    List<GameObject> Connections = new List<GameObject>();

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
        return GetOrCreateObject(Dots, i, DotPrefab);
    }

    GameObject GetMark(int i)
    {
        return GetOrCreateObject(Marks, i, MarkPrefab);
    }

    GameObject GetConnection(int i)
    {
        return GetOrCreateObject(Connections, i, ConnectionPrefab);
    }

    GameObject GetOrCreateObject(List<GameObject> list, int i, GameObject prefab)
    {
        while (list.Count <= i)
            list.Add(Instantiate(prefab, DotContainer.transform));

        return list[i];
    }

    int counter;

    long maxx = 0;
    long minn = 0;
    void RenderGraph(List<int> XList, GraphData graphData, int amountOfGraphs)
    {
        List<long> Values = graphData.values;
        
        RenderNoDataBanner(XList, Values);

        if (!isEnoughData(XList, Values))
            return;

        var len = XList.Count;

        maxx = System.Math.Max(maxx, Values.Max());
        minn = System.Math.Min(minn, Values.Min());

        long value = 0;

        GameObject prevDot = null;

        var i = 0;
        for (i = 0; i < len; i++)
        {
            value = Values[i];

            var dot = RenderDot(value, len, i, amountOfGraphs, graphData.Color, graphData.Name);

            if (prevDot != null)
                ConnectDots(prevDot.transform, dot.transform, counter - 1, graphData.Color);

            prevDot = dot;
            counter++;
        }

        HideUselessDots(i);
    }

    void ConnectDots(Transform dot1, Transform dot2, int i, Color color)
    {
        var connection = GetConnection(i);

        var diff = dot2.localPosition - dot1.localPosition;


        connection.transform.localPosition = dot1.localPosition + diff / 2;

        var angle = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        connection.transform.eulerAngles = new Vector3(0, 0, angle);

        connection.GetComponent<Image>().color = color;

        connection.GetComponent<RectTransform>().sizeDelta = new Vector2(diff.magnitude, 2f);

        connection.SetActive(true);
    }

    Vector3 GetPointPosition(long value)
    {
        return GetPointPosition(0, value, 1);
    }

    Vector3 GetPointPosition(int i, long value, int len)
    {
        return new Vector3(baseX + i * graphWidth / len, baseY + value * graphHeight / maxx);
    }

    GameObject RenderDot(long value, int len, int i, int amountOfGraphs, Color color, string hint)
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

        return dot;
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

        for (i = counter - 1; i < Connections.Count; i++)
            Connections[i].SetActive(false);
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
            RenderGraph(xs, ys[i], ys.Length);

        RenderYAxis();
    }

    public void SetData(List<int> xs, GraphData ys)
    {
        ResetCounter();

        RenderGraph(xs, ys, 1);

        RenderYAxis();
    }
}
