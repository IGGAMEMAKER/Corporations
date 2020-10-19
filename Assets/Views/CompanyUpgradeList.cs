using Assets.Core;
using UnityEngine;

public abstract class CompanyUpgradeList : ListView
{
    public override void SetItem<T>(Transform t, T entity)
    {
        var u = (ProductUpgrade)(object)entity;

        var elementCount = GetUpgrades().Length;
    }

    void SetTransformByIndex(Transform t, float radius, int number, int count)
    {
        var offset = Rendering.GetPointPositionOnArc(number - 1, count, radius, 45, -90);

        t.Translate(offset);
    }

    public abstract ProductUpgrade[] GetUpgrades();

    public override void ViewRender()
    {
        base.ViewRender();

        var upgrades = GetUpgrades();

        SetItems(upgrades);
    }
}