using Assets.Core;
using UnityEngine;

public abstract class CompanyUpgradeList : ListView
{
    public override void SetItem<T>(Transform t, T entity, object data = null)
    {
        System.Type t1 = typeof(TargetingToggleButton);

        var u = (ProductUpgrade)(object)entity;

        var elementCount = GetUpgrades().Length;

        var radius = 160f;

        switch (u)
        {
            // marketing lead
            case ProductUpgrade.TargetingCampaign:
                t.gameObject.AddComponent<TargetingToggleButton>();
                SetTransformByIndex(t, radius, 1, elementCount);

                break;

            case ProductUpgrade.TargetingCampaign2:
                t.gameObject.AddComponent<TargetingToggleButton2>();
                SetTransformByIndex(t, radius, 2, elementCount);

                break;

            case ProductUpgrade.TargetingCampaign3:
                t.gameObject.AddComponent<TargetingToggleButton3>();
                SetTransformByIndex(t, radius, 3, elementCount);

                break;

            case ProductUpgrade.BrandCampaign:
                t.gameObject.AddComponent<BrandingToggleButton>();
                SetTransformByIndex(t, radius, 4, elementCount);

                break;

            case ProductUpgrade.BrandCampaign2:
                t.gameObject.AddComponent<BrandingToggleButton2>();
                SetTransformByIndex(t, radius, 5, elementCount);

                break;

            case ProductUpgrade.BrandCampaign3:
                t.gameObject.AddComponent<BrandingToggleButton3>();
                SetTransformByIndex(t, radius, 6, elementCount);

                break;

            // product manager
            case ProductUpgrade.QA:
                t.gameObject.AddComponent<QAToggleButton>();
                SetTransformByIndex(t, radius, 1, elementCount);

                break;

            case ProductUpgrade.QA2:
                t.gameObject.AddComponent<QAToggleButton2>();
                SetTransformByIndex(t, radius, 2, elementCount);

                break;

            case ProductUpgrade.QA3:
                t.gameObject.AddComponent<QAToggleButton3>();
                SetTransformByIndex(t, radius, 3, elementCount);

                break;

            // CEO
            case ProductUpgrade.TestCampaign:
                t.gameObject.AddComponent<TestCampaignButton>();
                SetTransformByIndex(t, radius, 1, elementCount);

                break;
            case ProductUpgrade.PlatformDesktop:
                var c = t.gameObject.AddComponent<OneTimeToggleButton>();
                c.OneTimeUpgrade = u;
                SetTransformByIndex(t, radius, 1, elementCount);

                break;
        }
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

        SetItems(upgrades, upgrades.Length);
    }
}