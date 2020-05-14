using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcRender : MonoBehaviour
{
    float radius;
    float angleMin;
    float angleMax;

    public void Render(float radius = 120f, float angleMin = 45, float angleMax = -45)
    {
        this.radius = radius;
        this.angleMax = angleMax;
        this.angleMin = angleMin;

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
            //child.SetPositionAndRotation(position, Quaternion.identity);
            child.localPosition = position;

            index++;
        }
    }

    private void Start()
    {
        Render2();
    }

    private void OnEnable()
    {
        Render2();
    }
}
