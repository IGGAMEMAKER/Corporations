using Assets.Core;
using UnityEngine;

public class MyGroupCompaniesCultureChangesListView : ListView
{
    public override void SetItem<T>(Transform t, T entity)
    {
        var bar = t.GetComponent<ProgressBar>();

        if (Cooldowns.HasCorporateCultureUpgradeCooldown(Q, MyCompany))
        {
            var culture = Cooldowns.GetCorporateCultureCooldown(MyCompany, Q);

            var date = CurrentIntDate;

            bar.SetValue(date - culture.StartTime, culture.EndTime - culture.StartTime);
            bar.SetCustomText("Upgrading Culture".ToUpper());
        }
    }
}
