using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteAlways]
public class RenderCommunicationEffeciencyLine : View
{
    public Image Line;
    public Transform CEOTransform;

    public int Value = 150;

    public void SetEntity(GameEntity company, WorkerRole role, int maxRoles, Transform Center)
    {
        Line.color = Visuals.GetGradientColor(0, 100, Random.Range(0, 100));

        CEOTransform = Center;
    }

    private void Update()
    {
        LookAtCenter();
    }

    void LookAtCenter()
    {
        Line.color = Visuals.GetGradientColor(0, 100, Random.Range(0, 100));
        var p1 = transform.position;
        //var p1 = Line.transform.position;
        var p2 = CEOTransform.position;

        var diff = p2 - p1;

        var rotation = Mathf.Acos(diff.normalized.x);

        Debug.Log($"Look at: from {p1.x} {p1.y} {p1.z} to {p2.x} {p2.y} {p2.z}");
        Debug.Log($"Diff: {diff.x} {diff.y} {diff.z}");
        Debug.Log($"Rotation: {rotation}");

        Line.transform.rotation = Quaternion.Euler(0, 0, rotation * Mathf.Rad2Deg);
        Line.transform.position = p1 + diff / 2;
        //Line.transform.rotation.SetFromToRotation(Line.transform.position, CEOTransform.position);
    }
}
