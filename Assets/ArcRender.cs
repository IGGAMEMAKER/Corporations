using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcRender : MonoBehaviour
{
    float radius;
    public void Render(float radius = 120f)
    {
        this.radius = radius;

        var index = 0;
        var count = 0;

        foreach (Transform child in transform)
            count++;

        foreach (Transform child in transform)
        {
            var position = Rendering.GetPointPositionOnArc(index, count, radius, 1, 0, 360);
            //child.SetPositionAndRotation(position, Quaternion.identity);
            child.localPosition = position;

            index++;
        }
    }

    private void Start()
    {
        Render();
    }

    private void OnEnable()
    {
        Render();
    }
}
