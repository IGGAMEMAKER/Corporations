using Assets.Core;
using UnityEngine;

[ExecuteAlways]
public class ArcRender : View
{
    public float radius;
    public float angleMin;
    public float angleMax;

    public int AmountOfItems;

    public void Render(float radius = 120f, float angleMin = 45, float angleMax = -45)
    {
        this.radius = radius;
        this.angleMax = angleMax;
        this.angleMin = angleMin;

        Render2();
    }

    //private void Start()
    //{
    //    Render2();
    //}

    //private void OnEnable()
    //{
    //    Render2();
    //}

    //public override void ViewRender()
    //{
    //    base.ViewRender();

    //    Render2();
    //}

    void OnTransformChildrenChanged()
    {
        Render2();
    }

    private void Update()
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
            var position = Rendering.GetPointPositionOnArc(index, count, radius, angleMin, angleMax);

            child.localPosition = position;

            index++;
        }
    }
}
